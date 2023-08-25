using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ConfigDataMgr
{
    public static string RootPath= "../../../../";
    private const string ConfigPath = "../../../../Config/";
    public static List<string> TextConfigPathList;//线上文本表路径
    public static string TranslationConfigPath;//翻译给回来的翻译表路径
    public static string LastCompareConfigPath;//上次的对比表路径
    public static List<string> NoDoSheetList;//不处理的文本表中的页签名

    public static string DayDateTime;
    public static string SecondDateTime;
    public static bool ContainsNoDoSheet(string sheetName)
    {
        return NoDoSheetList.Contains(sheetName);
    }

    public static string SaveRootPath
    {
        get
        {
            return ConfigPath + DayDateTime;
        }
    }

    public static void Init()
    {
        
        SecondDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        DayDateTime= DateTime.Now.ToString("yyyyMMdd");
        if(!Directory.Exists(ConfigPath + DayDateTime))
        {
            Directory.CreateDirectory(ConfigPath + DayDateTime);
        }

        string textConfigPath="";//线上文本表路径
        List<string> textConfigNameList=new List<string>();//线上文本表名
        TextConfigPathList =new List<string>();
        StreamReader streamReader = new StreamReader(ConfigPath + "config.txt");
        string? lineStr;
        while ((lineStr = streamReader.ReadLine()) != null)
        {
            string[] lineArray = lineStr.Split("=");
            for (int i = 0; i < lineArray.Length; i++)
            {
                lineArray[i] = lineArray[i].Trim();
            }
            if (lineArray[0].Equals("文本表名"))
            {
                string[] configNameArr = lineArray[1].Split(',', '，');
                for (int i = 0; i < configNameArr.Length; i++)
                {
                    configNameArr[i] = configNameArr[i].Trim();
                    if(!string.IsNullOrEmpty(configNameArr[i]))
                    {
                        textConfigNameList.Add(configNameArr[i]);
                    }
                }
            }
            else if (lineArray[0].Equals("文本表路径"))
            {
                textConfigPath = ConfigPath + lineArray[1].Trim();
            }
            else if (lineArray[0].Equals("翻译给回来的翻译表名"))
            {
                TranslationConfigPath = ConfigPath + lineArray[1].Trim();
            }
            else if (lineArray[0].Equals("上次的对比表"))
            {
                LastCompareConfigPath = ConfigPath + lineArray[1].Trim();
            }
            else if (lineArray[0].Equals("不处理的文本表中的页签名"))
            {
                NoDoSheetList = new List<string>();
                string[] sheetNameArr = lineArray[1].Split(',', '，');
                for (int i = 0; i < sheetNameArr.Length; i++)
                {
                    sheetNameArr[i] = sheetNameArr[i].Trim();
                    if (!string.IsNullOrEmpty(sheetNameArr[i]))
                    {
                        NoDoSheetList.Add(sheetNameArr[i]);
                    }
                }
            }
        }
        //文本表路径赋值
        if (!string.IsNullOrEmpty(textConfigPath))
        {
            for (int i = 0; i < textConfigNameList.Count; i++)
            {
                TextConfigPathList.Add(textConfigPath + textConfigNameList[i]);
            }
        }
    }
}