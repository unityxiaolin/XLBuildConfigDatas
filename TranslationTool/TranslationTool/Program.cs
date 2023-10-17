// See https://aka.ms/new-console-template for more information
using System;
using Spire.Xls;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===开始处理===");
            LogMgr.InitLog();
            ConfigDataMgr.Init();
            if(args.Length<=0)
            {
                args = new string[] { "4" };
            }
            
            switch (args[0])
            {
                case "1"://导出新增文本
                    InitLastCompareInfo();
                    InitTextConfigInfo();
                    CompareWriteToExcel();
                    ExportNewAddTextConfig();
                    break;
                case "2"://将翻译文本对比后写入线上文本表
                    InitTranslationInfo();
                    InitTextConfigInfo();
                    CompareWriteToExcel();
                    WriteTranslationToOrcTextConfigs();
                    break;
                case "3"://导出新增文本,并将翻译文本对比后写入线上文本表
                    InitTranslationInfo();
                    InitLastCompareInfo();
                    InitTextConfigInfo();
                    CompareWriteToExcel();
                    ExportNewAddTextConfig();
                    WriteTranslationToOrcTextConfigs();
                    break;
                case "4":
                    InitTextConfigInfo();
                    break;
                default:
                    Console.WriteLine("参数错误，无法处理表格");
                    break;
            }

            Console.WriteLine("===处理完成===");
            Console.ReadLine();
        }

        static Dictionary<uint, TextConfig> m_textConfigDic = new Dictionary<uint, TextConfig>(10000);//线上文本表的配置信息
        static Dictionary<uint, TextConfig> m_translationDic = new Dictionary<uint, TextConfig>(5000);//翻译给回来的表
        static Dictionary<uint, TextConfig> m_lastCompareDic = new Dictionary<uint, TextConfig>(10000);//上次生成的对比表
        static Dictionary<uint, TextCompareConfig> m_textCompareConfigDic;//对比表的数据信息

        /// <summary>
        /// 对比后写入excel
        /// </summary>
        static void CompareWriteToExcel()
        {
            Workbook compareBook = new Workbook();
            Worksheet compareSheet = compareBook.Worksheets[0];
            compareSheet.Name = "对比表";
            compareSheet.SetCellValue(1, 1, "ID");
            compareSheet.SetCellValue(1, 2, "中文");
            compareSheet.SetCellValue(1, 3, "旧中文");
            compareSheet.SetCellValue(1, 4, "对比");
            compareSheet.SetCellValue(1, 5, "旧英文");
            compareSheet.SetCellValue(1, 6, "新英文");
            compareSheet.SetCellValue(1, 7, "对比合并");

            Console.WriteLine("线上文本数量：" + m_textConfigDic.Count);
            m_textCompareConfigDic = new Dictionary<uint, TextCompareConfig>(m_textConfigDic.Count);
            int row = 2;
            foreach (TextConfig textConfig in m_textConfigDic.Values)
            {
                TextCompareConfig compareConfig = new TextCompareConfig();
                compareConfig.Id = textConfig.Id;
                compareConfig.NewChinese = textConfig.Chinese ?? "";
                compareConfig.OldEnglish = textConfig.English ?? "";
                TextConfig transConfig;
                if(m_lastCompareDic.TryGetValue(textConfig.Id, out transConfig))
                {
                    compareConfig.OldChinese = transConfig.Chinese;
                }
                else
                {
                    compareConfig.OldChinese = "";
                }
                if (m_translationDic.TryGetValue(textConfig.Id, out transConfig))
                {
                    compareConfig.NewEnglish = transConfig.English;
                }
                else
                {
                    compareConfig.NewEnglish = "";
                }
                compareConfig.InitCompareAddStr();
                compareConfig.InitFinalEnglish();
                m_textCompareConfigDic.Add(compareConfig.Id, compareConfig);

                compareSheet.SetCellValue(row, 1, compareConfig.Id.ToString());
                compareSheet.SetCellValue(row, 2, compareConfig.NewChinese);
                compareSheet.SetCellValue(row, 3, compareConfig.OldChinese);
                compareSheet.SetCellValue(row, 4, compareConfig.CompareAdd);
                compareSheet.SetCellValue(row, 5, compareConfig.OldEnglish);
                compareSheet.SetCellValue(row, 6, compareConfig.NewEnglish);
                compareSheet.SetCellValue(row, 7, compareConfig.CompareFinalEnglish);
                row += 1;
            }
            compareBook.SaveToFile(ConfigDataMgr.SaveRootPath + "/" + ConfigDataMgr.SecondDateTime + "对比表.xlsx");
        }

        /// <summary>
        /// 将翻译对比后的内容写入线上文本表
        /// </summary>
        static void WriteTranslationToOrcTextConfigs()
        {
            Console.WriteLine("开始将翻译内容写入线上的文本表。");
            for (int i = 0; i < ConfigDataMgr.TextConfigPathList.Count; i++)
            {
                if (!File.Exists(ConfigDataMgr.TextConfigPathList[i]))
                {
                    Console.WriteLine("线上文本表找不到：" + ConfigDataMgr.TextConfigPathList[i]);
                    continue;
                }
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(ConfigDataMgr.TextConfigPathList[i]);
                for (int j = 0; j < workbook.Worksheets.Count; j++)
                {
                    Worksheet sheet = workbook.Worksheets[j];
                    if (sheet.Name.StartsWith("#") || sheet.Name.StartsWith("Sheet")
                        || sheet.Name == "Evaluation Warning" || ConfigDataMgr.ContainsNoDoSheet(sheet.Name))
                    {
                        sheet.Dispose();
                        continue;
                    }
                    WriteTranslationToOrcTextConfig(sheet);
                }
                workbook.Save();
                workbook.Dispose();
            }
        }
        /// <summary>
        /// 将翻译对比后的内容写入线上文本表
        /// </summary>
        static void WriteTranslationToOrcTextConfig(Worksheet sheet)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "正在写入：" + sheet.Name);
            CellRange firstCell;
            for (int row = 1; row < sheet.Rows.Length; row++)
            {
                uint id = 0;
                int englishIndex = 3;
                for (int column = 0; column < sheet.Columns.Length; column++)
                {
                    CellRange rowData = sheet.Rows[row].Columns[column];
                    firstCell = sheet.Rows[0].Columns[column];
                    if (string.IsNullOrEmpty(firstCell.DisplayedText))
                    {
                        break;
                    }
                    if (firstCell.DisplayedText.Equals("索引ID".Trim()))
                    {
                        if (string.IsNullOrEmpty(rowData.DisplayedText))//这个页签结束了
                        {
                            return;
                        }
                        if (TryParseUintValue(rowData, out uint tempId))
                        {
                            id = tempId;
                        }
                        else
                        {
                            Console.WriteLine("解析id失败：id:" + rowData.DisplayedText);
                        }
                    }
                    else if (firstCell.DisplayedText.Equals("内容_2".Trim()))
                    {
                        englishIndex = column;
                    }
                }
                if(m_textCompareConfigDic.TryGetValue(id, out TextCompareConfig compareConfig))
                {
                    sheet.SetCellValue(row + 1, englishIndex + 1, compareConfig.CompareFinalEnglish);
                }
            }
        }

        /// <summary>
        /// 导出新增文本
        /// </summary>
        static void ExportNewAddTextConfig()
        {
            Workbook compareBook = new Workbook();
            Worksheet compareSheet = compareBook.Worksheets[0];
            compareSheet.Name = "新增文本";
            compareSheet.SetCellValue(1, 1, "ID");
            compareSheet.SetCellValue(1, 2, "中文");
            compareSheet.SetCellValue(1, 3, "英文");
            int row = 2;
            foreach (var compareConfig in m_textCompareConfigDic.Values)
            {
                if(!string.IsNullOrEmpty(compareConfig.CompareAdd))
                {
                    compareSheet.SetCellValue(row, 1, compareConfig.Id.ToString());
                    compareSheet.SetCellValue(row, 2, compareConfig.CompareAdd);
                    row += 1;
                }
            }
            compareBook.SaveToFile(ConfigDataMgr.SaveRootPath + "/" + ConfigDataMgr.SecondDateTime + "新增文本表.xlsx");
        }

        /// <summary>
        /// 初始化上次的对比表信息，将上次对比表信息存入字典
        /// </summary>
        static void InitLastCompareInfo()
        {
            if (!File.Exists(ConfigDataMgr.LastCompareConfigPath))
            {
                Console.WriteLine("上次的对比表不存在,无法读取到新翻译信息");
                return;
            }
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(ConfigDataMgr.LastCompareConfigPath);
            Worksheet sheet = workbook.Worksheets[0];
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "正在读取上次的对比表信息：" + workbook.FileName);
            CellRange firstCell;
            for (int row = 1; row < sheet.Rows.Length; row++)
            {
                TextConfig textConfig = new TextConfig();
                if (string.IsNullOrEmpty(sheet.Rows[row].Columns[0].Value))
                {
                    break;
                }
                if(TryParseUintValue(sheet.Rows[row].Columns[0], out uint id))
                {
                    textConfig.Id = id;
                    textConfig.Chinese = sheet.Rows[row].Columns[1].DisplayedText.Trim();
                    m_lastCompareDic.Add(textConfig.Id, textConfig);
                }
                else
                {
                    Console.WriteLine("解析失败的id:" + sheet.Rows[row].Columns[0].Value.Trim()+"        中文为："+ sheet.Rows[row].Columns[1].DisplayedText.Trim());
                }
            }
        }

        /// <summary>
        /// 初始化翻译信息，将翻译信息存入字典
        /// </summary>
        static void InitTranslationInfo()
        {
            if(!File.Exists(ConfigDataMgr.TranslationConfigPath))
            {
                Console.WriteLine("翻译表不存在,无法读取到新翻译信息");
                return;
            }
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(ConfigDataMgr.TranslationConfigPath);
            Worksheet sheet = workbook.Worksheets[0];
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "正在读取翻译给回来的表格信息：" + workbook.FileName);
            CellRange firstCell;
            for (int row = 1; row < sheet.Rows.Length; row++)
            {
                TextConfig textConfig = new TextConfig();
                if(string.IsNullOrEmpty(sheet.Rows[row].Columns[0].DisplayedText))
                {
                    break;
                }
                if(TryParseUintValue(sheet.Rows[row].Columns[0], out uint tempId))
                {
                    textConfig.Id = tempId;
                    textConfig.Chinese = sheet.Rows[row].Columns[1].DisplayedText.Trim();
                    textConfig.English = sheet.Rows[row].Columns[2].DisplayedText.Trim();
                    m_translationDic.Add(textConfig.Id, textConfig);
                }
            }
        }

        static bool TryParseUintValue(CellRange cellRange, out uint finalValue)
        {
            if(TryParseUintValue(cellRange.Value, out finalValue))
            {
                return true;
            }
            else if(TryParseUintValue(cellRange.DisplayedText, out finalValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool TryParseUintValue(string str, out uint finalValue)
        {
            uint tempValue;
            if(uint.TryParse(str, out tempValue))
            {
                finalValue = tempValue;
                return true;
            }
            decimal tempV;
            if(decimal.TryParse(str, out tempV))
            {
                finalValue = (uint)Math.Round(tempV);
                return true;
            }
            finalValue = 0;
            return false;
        }

        /// <summary>
        /// 初始化线上文本表信息，将文本信息存入字典
        /// </summary>
        static void InitTextConfigInfo()
        {
            for (int i = 0; i < ConfigDataMgr.TextConfigPathList.Count; i++)
            {
                if (!File.Exists(ConfigDataMgr.TextConfigPathList[i]))
                {
                    Console.WriteLine("线上文本表找不到：" + ConfigDataMgr.TextConfigPathList[i]);
                    continue;
                }
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(ConfigDataMgr.TextConfigPathList[i]);
                for (int j = 0; j < workbook.Worksheets.Count; j++)
                {
                    Worksheet sheet = workbook.Worksheets[j];
                    if (sheet.Name.StartsWith("#") || sheet.Name.StartsWith("Sheet")
                        || sheet.Name == "Evaluation Warning" || ConfigDataMgr.ContainsNoDoSheet(sheet.Name))
                    {
                        sheet.Dispose();
                        continue;
                    }
                    GenerateTextConfigInfo(sheet);
                    sheet.Dispose();
                }
                workbook.Dispose();
            }
        }

        /// <summary>
        /// 线上文本表的信息存入字典中
        /// </summary>
        /// <param name="sheet"></param>
        static void GenerateTextConfigInfo(Worksheet sheet)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "正在读取线上文本表信息：" + sheet.Name);
            CellRange firstCell;
            for (int row = 1; row < sheet.Rows.Length; row++)
            {
                TextConfig textConfig = new TextConfig();
                for (int column = 0; column < sheet.Columns.Length; column++)
                {
                    CellRange rowData = sheet.Rows[row].Columns[column];
                    firstCell = sheet.Rows[0].Columns[column];
                    if(string.IsNullOrEmpty(firstCell.DisplayedText))
                    {
                        break;
                    }
                    if(firstCell.DisplayedText.Equals("索引ID".Trim()))
                    {
                        if(string.IsNullOrEmpty(rowData.DisplayedText))//这个页签结束了
                        {
                            return;
                        }
                        if(TryParseUintValue(rowData, out uint id))
                        {
                            textConfig.Id = id;
                        }
                        else
                        {
                            Console.WriteLine("解析id失败，id:" + rowData.DisplayedText);
                        }
                    }
                    else if (firstCell.DisplayedText.Equals("内容_1".Trim()))
                    {
                        textConfig.Chinese = rowData.DisplayedText;
                    }
                    else if (firstCell.DisplayedText.Equals("内容_2".Trim()))
                    {
                        textConfig.English = rowData.DisplayedText;
                    }
                }
                if(textConfig.Id != 0)
                {
                    m_textConfigDic.Add(textConfig.Id, textConfig);
                }
            }
        }
    }
}
        

