using Spire.Xls;
using System;

public static class ProtoTool
{
    /// <summary>
    /// 根据excel来生成proto文件
    /// </summary>
    public static void GenerateProtoByExcel(Worksheet sheet)
    {
        if(sheet.Rows.Count()<=0 || sheet.Columns.Count()<=0)
        {
            //Console.WriteLine($"{sheet.Name}中的数据为空，不生成表格数据.");
            return;
        }
        //Console.WriteLine($"即将生成proto文件的页签名称:{sheet.Name}");
        string filePath = pathMgr.protoPath + Utils.GetClassName(sheet.Name) + ".proto";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        FileStream fileStream = File.Create(filePath);
        //Console.WriteLine("创建proto文件成功：" + sheet.Name + Utils.ConfigClassSuffix + ".proto");
        StreamWriter writer = new StreamWriter(fileStream);
        writer.WriteLine("syntax = \"proto3\";");
        writer.WriteLine();
        //该类的数组
        writer.WriteLine("message " + Utils.GetConfigClassesName(sheet.Name));
        writer.WriteLine("{");
        string desc = $"//{Utils.ConfigBaseName}===>{Utils.GetClassName(sheet.Name)}类的数组列表类";
        writer.WriteLine($"\t{Utils.repeated} {Utils.GetClassName(sheet.Name)} {Utils.ConfigBaseName}=1;{desc}");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("message " + Utils.GetClassName(sheet.Name));
        writer.WriteLine("{");
        int excelColumn = 0;//表格中每行数据的列
        int clientColumn = 1;//proto文件中的下标从1开始往下递增
        int jumpColumnCount = 0;//假如是结构体，需要跳过结构体对应的那么多列
        List<CellRange> oneRowCellRanges = sheet.Rows[0].CellList;
        List<ConfigStruct> configStructList = new List<ConfigStruct>();//存储表格中的结构体数据
        while (excelColumn < oneRowCellRanges.Count && !string.IsNullOrEmpty(oneRowCellRanges[excelColumn].DisplayedText))
        {
            if(!oneRowCellRanges[excelColumn].DisplayedText.ToLower().Contains("c"))//客户端字段才会读取
            {
                excelColumn++;
                continue;
            }
            if(jumpColumnCount>0)
            {
                jumpColumnCount--;
                excelColumn++;
                continue;
            }
            List<CellRange> twoRowCellRanges = sheet.Rows[1].CellList;
            List<CellRange> threeRowCellRanges = sheet.Rows[2].CellList;
            List<CellRange> fourRowCellRanges = sheet.Rows[3].CellList;
            List<CellRange> fiveRowCellRanges = sheet.Rows[4].CellList;
            string structTypeStr = twoRowCellRanges[excelColumn].DisplayedText;
            string memberTypeStr = threeRowCellRanges[excelColumn].DisplayedText;
            string memberStr = Utils.ConvertToProtoName(fourRowCellRanges[excelColumn].DisplayedText);
            string memberDesc = fiveRowCellRanges[excelColumn].DisplayedText;
            desc = $"//{memberStr}===>{memberDesc}";
            if (structTypeStr == Utils.optional)//普通类型
            {
                writer.WriteLine($"\t{memberTypeStr} {memberStr}={clientColumn};{desc}");
                clientColumn++;
            }
            else if (structTypeStr == Utils.repeated)//数组类型
            {
                writer.WriteLine($"\t{structTypeStr} {memberTypeStr} {memberStr}={clientColumn};{desc}");
                clientColumn++;
            }
            else if(structTypeStr.ToLower().StartsWith("struct"))//结构体类型
            {
                writer.WriteLine($"\t{Utils.repeated} {sheet.Name+memberStr} {memberStr}={clientColumn};{desc}");
                clientColumn++;

                string[] structStr = structTypeStr.Split("_");
                int arrayCount = int.Parse(structStr[1]);
                int memberCount = int.Parse(structStr[2]);
                jumpColumnCount = arrayCount * memberCount;
                List<string> contentStrList = new List<string>();
                for (int i = 1; i <= memberCount; i++)
                {
                    string structTypeStrOfStruct = twoRowCellRanges[excelColumn + i].DisplayedText;
                    string memberTypeStrOfStruct = threeRowCellRanges[excelColumn + i].DisplayedText;
                    string memberStrOfStruct = fourRowCellRanges[excelColumn + i].DisplayedText;
                    string memberDescOfStruct = fiveRowCellRanges[excelColumn + i].DisplayedText;
                    string contentStr = "";
                    desc = $"//{memberStrOfStruct}===>{memberDescOfStruct}";
                    if (structTypeStrOfStruct == Utils.optional)
                    {
                        contentStr = $"\t {memberTypeStrOfStruct} {memberStrOfStruct}={i};{desc}";
                    }
                    else if (structTypeStrOfStruct == Utils.repeated)
                    {
                        contentStr = $"\t{structTypeStrOfStruct} {memberTypeStrOfStruct} {memberStrOfStruct}={i};{desc}";
                    }
                    
                    contentStrList.Add(contentStr);
                }
                if(contentStrList.Count > 0)
                {
                    configStructList.Add(new ConfigStruct(sheet.Name + memberStr, contentStrList));
                }
            }
            excelColumn++;
        }
        writer.WriteLine("}");
        //根据上面存储的结构体数据生成结构体的proto内容
        for (int i = 0; i < configStructList.Count; i++)
        {
            writer.WriteLine();
            ConfigStruct configStruct = configStructList[i];
            writer.WriteLine($"message {configStruct.StructName}");
            writer.WriteLine("{");
            for (int j = 0; j < configStruct.ContentStrList.Count; j++)
            {
                string contentStr=configStruct.ContentStrList[j];
                writer.WriteLine(contentStr);
            }
            writer.WriteLine("}");
        }
        writer.Flush();
        writer.Close();
        writer.Dispose();
        fileStream.Close();
        fileStream.Dispose();
        //Console.WriteLine("proto文件数据写入成功 --------- proto generate done.");
    }

    /// <summary>
    /// 配置表中的结构体类型的结构体
    /// </summary>
    struct ConfigStruct
    {
        private string m_structName;
        public string StructName { get => m_structName; set => m_structName = value; }

        private List<string> m_contentStrList;
        public List<string> ContentStrList { get => m_contentStrList; set => m_contentStrList = value; }

        public ConfigStruct(string structName, List<string> contentStrList)
        {
            m_structName = structName;
            m_contentStrList = contentStrList;
        }
    }

}