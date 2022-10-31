using Spire.Xls;
using System;
using System.Security.Cryptography;
using System.Text;

public class MD5Tool
{
    private static MD5 md5 = MD5.Create();

    /// <summary>
    /// 通过文件来获取md5值
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetMD5ByFile(string filePath)
    {
        string fileMD5 = "";
        try
        {
            using(FileStream fileStream=File.OpenRead(filePath))
            {
                byte[] fileMD5Bytes = md5.ComputeHash(fileStream);
                fileMD5 = BitConverter.ToString(fileMD5Bytes);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"MD5 get fail,please check,filePath={filePath} Exception:{ex.Message}");
        }
        return fileMD5;
    }

    /// <summary>
    /// 通过字符串来计算MD5值
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetMD5ByString(string str)
    {
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 根据sheet来获取md5值
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    public static string GetMD5BySheet(Worksheet sheet)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(sheet.Name);
        foreach (var row in sheet.Rows)
        {
            foreach (CellRange item in row.Columns)
            {
                stringBuilder.Append(item.DisplayedText);
            }
        }
        string str = stringBuilder.ToString();
        stringBuilder.Clear();
        return GetMD5ByString(str);
    }

}