using System;

public static class pathMgr
{
    public static string rootPath = "..\\..\\..\\..\\";  //"..\\";  //"..\\..\\..\\..\\";
    public static string configsPath = "";
    public static string csPath = "";
    public static string csDir = "";
    public static string protoPath = "";
    public static string protoDir = "";
    public static string protocPath = rootPath + "XLBuildConfigDatas\\Tools\\ProtoTools\\protoc.exe";
    public static string bytesPath = "";
    public static string md5Path = "";
    public static string CopyCSToPath = "";
    public static string CopyCSToDir = "";

    public static void InitPath()
    {
        StreamReader streamReader = new StreamReader(rootPath + "path.txt");
        string? lineStr;
        while ((lineStr = streamReader.ReadLine()) != null)
        {
            string[] lineArray = lineStr.Split("=");
            for (int i = 0; i < lineArray.Length; i++)
            {
                lineArray[i] = lineArray[i].Trim();
            }
            if (lineArray[0] == "Configs")
            {
                configsPath = rootPath + lineArray[1] + "\\";
            }
            else if (lineArray[0] == "CS")
            {
                csPath = rootPath + lineArray[1] + "\\";
                csDir = rootPath + lineArray[1];
            }
            else if (lineArray[0] == "Proto")
            {
                protoPath = rootPath + lineArray[1] + "\\";
                protoDir = rootPath + lineArray[1];
            }
            else if (lineArray[0] == "Bytes")
            {
                bytesPath = rootPath + lineArray[1] + "\\";
            }
            else if (lineArray[0] == "MD5")
            {
                md5Path = rootPath + lineArray[1] + "\\";
            }
            else if (lineArray[0] == "CopyCSToDir")
            {
                CopyCSToPath = rootPath + lineArray[1] + "\\";
                CopyCSToDir = rootPath + lineArray[1];
            }
        }
    }
}