using System;
using Google.Protobuf;
using Spire.Xls;

namespace Program
{
    class Program
    {
        static Dictionary<string, Worksheet> m_sheetDataDic=new Dictionary<string, Worksheet>();
        static Dictionary<string, string> m_md5Dic = new Dictionary<string, string>
        {
            ["======SheetName======"] ="======MD5======"
        };
        static void Main(string[] args)
        {
            pathMgr.Init();
            
            //GenerateSameCongigs(10);
            //TestReadConfigs();
            BuildConfigs();

            Console.ReadKey();
        }

        #region 复制多份表格，用于测试打表内存情况
        /// <summary>
        /// 复制多份表格，用于测试打表内存情况
        /// </summary>
        /// <param name="configCount">复制表格的数量</param>
        static void GenerateSameCongigs(int configCount)
        {
            #region 暂时注释 以这种方式生成的xlsx文件中的数字以科学计数法来保存，数字比较大的时候，读出来的数据变成了1E+7之类的了，暂未有好的处理办法。
            //Workbook workbook = new Workbook();
            //workbook.LoadFromFile(pathMgr.configsPath + "羽翼系统.xlsx");
            //for (int i = 0; i < configCount; i++)
            //{
            //    Console.WriteLine($"正在复制第{(i + 1)}个表格...");
            //    Workbook newWorkbook = new Workbook();
            //    for (int j = 0; j < workbook.Worksheets.Count; j++)
            //    {
            //        Worksheet sheet = workbook.Worksheets[j];
            //        if (sheet.Name.StartsWith("#") || sheet.Name.StartsWith("Sheet"))//#或者Sheet开头的页签为备注页签
            //        {
            //            continue;
            //        }
            //        Worksheet newSheet = newWorkbook.Worksheets.AddCopy(sheet);
            //        newSheet.Name = sheet.Name + (i + 1);
            //    }
            //    newWorkbook.SaveToFile($"{pathMgr.configsPath}羽翼系统{(i + 1)}.xlsx");
            //}
            #endregion

            string newFilePath;
            for (int i = 0; i < configCount; i++)
            {
                Console.WriteLine($"正在复制第{(i + 1)}个表格...");
                newFilePath = pathMgr.configsPath + $"羽翼系统{(i + 1)}.xlsx";
                if(File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }
                File.Copy(pathMgr.configsPath + "羽翼系统.xlsx", newFilePath);
                Workbook newWorkbook = new Workbook();
                newWorkbook.LoadFromFile(newFilePath);
                for (int j = 0; j < newWorkbook.Worksheets.Count; j++)
                {
                    Worksheet sheet = newWorkbook.Worksheets[j];
                    if (sheet.Name.StartsWith("#") || sheet.Name.StartsWith("Sheet"))//#或者Sheet开头的页签为备注页签
                    {
                        continue;
                    }
                    sheet.Name = sheet.Name + (i + 1);
                }
                newWorkbook.Save();
                newWorkbook.Dispose();
            }

            Console.WriteLine($"成功复制{configCount}个表格文件, done.");
        }
        #endregion

        #region 读表测试  已注释，需要时打开
        static void TestReadConfigs()
        {
            ////需要测试的，可以把TempCopyCSDir的cs文件拷贝到Program.cs的同级目录进行测试，
            ////或者将path.txt中的CopyCSToDir的路径改到Program.cs的同级目录下重新生成cs文件后测试
            //string bytesFilePath = pathMgr.bytesPath + "WingBaseConfig.bytes";
            //FileStream fileStream = File.OpenRead(bytesFilePath);
            //WingBaseConfigDatas wingBaseConfigDatas = new WingBaseConfigDatas();
            //wingBaseConfigDatas.MergeFrom(fileStream);
            //List<WingBaseConfig> wingBaseConfigs = new List<WingBaseConfig>();
            //for (int i = 0; i < wingBaseConfigDatas.WingBase.Count; i++)
            //{
            //    wingBaseConfigs.Add(wingBaseConfigDatas.WingBase[i]);
            //}
            //Console.WriteLine(wingBaseConfigs[1].WingId);
        }
        #endregion

        /// <summary>
        /// 开始打表
        /// </summary>
        static void BuildConfigs()
        {
            if (File.Exists(pathMgr.md5Path + Utils.MD5CSVFile))
            {
                StreamReader sr = new StreamReader(pathMgr.md5Path + Utils.MD5CSVFile);
                string? lineStr = sr.ReadLine();
                lineStr = sr.ReadLine();
                while (lineStr != null)
                {
                    string configNameColumn = lineStr.Split(",")[0];
                    string md5Column = lineStr.Split(",")[1];
                    m_md5Dic.Add(configNameColumn, md5Column);
                    lineStr = sr.ReadLine();
                }
                sr.Dispose();
                sr.Close();
            }
            bool isChange = false;
            DirectoryInfo dirInfo = new DirectoryInfo(pathMgr.configsPath);
            FileInfo[] allConfigFileInfo = dirInfo.GetFiles("*.xlsx");
            foreach (FileInfo fileInfo in allConfigFileInfo)
            {
                if (fileInfo.Name.StartsWith("~$"))//表格打开后会有这个~$开头的表格文件，剔除掉
                {
                    continue;
                }
                Console.WriteLine($"开始检查表格，表格的名称:{fileInfo.Name}");
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(pathMgr.configsPath + fileInfo.Name);
                //Console.WriteLine($"有{workbook.Worksheets.Count}个页签的表格数据");
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    Worksheet sheet = workbook.Worksheets[i];
                    if (sheet.Name.StartsWith("#") || sheet.Name.StartsWith("Sheet") || sheet.Name == "Evaluation Warning")//#或者Sheet开头的页签为备注页签，不打表
                    {
                        sheet.Dispose();
                        continue;
                    }
                    string configKey = sheet.Name + "Config";
                    string sheetMD5 = MD5Tool.GetMD5BySheet(sheet);
                    //Console.WriteLine($"sheetName:{sheet.Name}  md5:{sheetMD5}");
                    if(m_md5Dic.ContainsKey(configKey))
                    {
                        if(m_md5Dic[configKey] == sheetMD5)//MD5值没有变化，不打表
                        {
                            continue;
                        }
                        m_md5Dic[configKey] = sheetMD5;//sheet有修改
                    }
                    else//新增的sheet
                    {
                        m_md5Dic.Add(configKey, sheetMD5);
                    }
                    isChange = true;
                    Console.WriteLine($"======================>>>开始处理表格：sheetName={sheet.Name}");
                    m_sheetDataDic.Add(configKey, sheet);
                    ProtoTool.GenerateProtoByExcel(sheet);
                }
            }
            if (isChange == false)
            {
                Console.WriteLine("所有表格无变化，无需打表!!!");
                return;
            }
            Console.WriteLine("开始执行cmd命令生成c#代码");
            EventHandler cmdExitHandler = (sender, name) => CmdExitedHandler();
            Utils.GenerateCS(cmdExitHandler);
        }

        /// <summary>
        /// 生成完c#代码后执行
        /// </summary>
        static void CmdExitedHandler()
        {
            List<string> csNameList = m_sheetDataDic.Keys.ToList();
            Utils.CopyCSFilesToDir(csNameList);
            CSCompilerTool.CompilerCSFiles(csNameList);
            CSTool.Init();
            //根据cs文件生成bytes文件
            foreach (string csName in csNameList)
            {
                Worksheet? sheet;
                if (m_sheetDataDic.TryGetValue(csName, out sheet))
                {
                    CSTool.GenerateBytesFile(csName, sheet);
                }
                else
                {
                    Console.Error.WriteLine("!!! donot find sheet data, sheet name:" + csName);
                }
            }
            Console.WriteLine("All bytes file generate success!");
            Utils.CopyBytesFilesToDir(csNameList);//csName is the same of the bytesName
            //生成md5文件
            File.Delete(pathMgr.md5Path + Utils.MD5CSVFile);
            StreamWriter streamWriter = new StreamWriter(pathMgr.md5Path + Utils.MD5CSVFile);
            foreach (var md5Item in m_md5Dic)
            {
                streamWriter.WriteLine($"{md5Item.Key},{md5Item.Value}");
            }
            streamWriter.Flush();
            streamWriter.Dispose();
            Console.WriteLine("md5 file write success.");
            Console.WriteLine("=======================打表结束=======================");
        }
    }
}