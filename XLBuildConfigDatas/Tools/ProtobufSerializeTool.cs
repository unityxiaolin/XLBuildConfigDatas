using Google.Protobuf;
using System;

public class ProtobufSerializeTool
{
    /// <summary>
    /// 序列化后保存为bytes文件
    /// </summary>
    /// <param name="configDatasClass">类结构的数组类</param>
    /// <param name="csName">cs文件名</param>
    public static void SerializeCSToFile(IMessage configDatasClass, string csName)
    {
        //MessageExtensions为序列化的静态类
        byte[] data = configDatasClass.ToByteArray();
        string bytesFilePath = pathMgr.bytesPath + csName + ".bytes";
        if (File.Exists(bytesFilePath))
        {
            File.Delete(bytesFilePath);
        }
        FileStream fileStream = new FileStream(bytesFilePath, FileMode.Create, FileAccess.Write);
        fileStream.Write(data, 0, data.Length);
        fileStream.Flush();
        fileStream.Close();
        Console.WriteLine("Serialize success, Generate bytes file success,fileName:"+ csName + ".bytes");
    }
}