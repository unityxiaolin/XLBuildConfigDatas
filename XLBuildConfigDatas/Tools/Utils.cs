using System;
using System.Diagnostics;

public static class Utils
{
    //proto结构类型
    public static string optional = "optional";
    public static string repeated = "repeated";
    //proto生成类的后缀
    public static string ConfigClassSuffix = "Config";
    public static string ConfigClassesSuffix = "ConfigDatas";
    //数据类型
    public static string stringType = "string";
    //编译CS生成的dll名
    public static string CompilerCSDLL = "CompilerCSDLL";
    //MD5校验文件的文件名
    public static string MD5CSVName = "MD5File";
    public static string MD5CSVFile = MD5CSVName + ".txt";

    #region 通过proto代码生成C#代码

    /// <summary>
    /// 通过proto代码生成C#代码
    /// </summary>
    public static void GenerateCS(EventHandler cmdExitedHandler)
    {
        List<string> cmds = new List<string>();
        DirectoryInfo folder=new DirectoryInfo(pathMgr.protoPath);
        FileInfo[] files = folder.GetFiles("*.proto");
        foreach(FileInfo file in files)
        {
            //string cmd = $"{pathMgr.protocPath} --csharp_out={pathMgr.csDir} --proto_path={pathMgr.protoPath}{file.Name}";
            string cmd = $"{pathMgr.protocPath} -I={pathMgr.protoPath} --csharp_out={pathMgr.csPath} {pathMgr.protoPath}{file.Name}";
            cmds.Add(cmd);
        }
        Cmd(cmds, cmdExitedHandler);
    }
    /// <summary>
    /// 执行cmd命令
    /// </summary>
    /// <param name="cmds"></param>
    /// <param name="cmdExitedHandler"></param>
    public static void Cmd(List<string> cmds, EventHandler cmdExitedHandler)
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        //process.StartInfo.WorkingDirectory = pathMgr.rootPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = true;
        process.Exited += cmdExitedHandler;
        //process.OutputDataReceived += ProcessOutputDataReceived;
        process.ErrorDataReceived += ProcessErrorDataReceived;
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        for (int i = 0; i < cmds.Count; i++)
        {
            process.StandardInput.WriteLine(cmds[i]);
        }
        process.StandardInput.WriteLine("exit");
        process.WaitForExit();
    }
    
    private static void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            Console.WriteLine("cmd命令行执行后的输出信息：" + e.Data);
        }
    }
    private static void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if(!string.IsNullOrEmpty(e.Data))
        {
            Console.WriteLine("cmd命令行执行后的报错信息："+e.Data);
        }
    }

    #endregion

    #region 复制生成的C#文件到指定目录

    public static void CopyCSFilesToDir(List<string> csNameList)
    {
        if(pathMgr.CopyCSToDir==pathMgr.rootPath)
        {
            return;
        }
        for (int i = 0; i < csNameList.Count; i++)
        {
            FileInfo file = new FileInfo(pathMgr.csPath + csNameList[i]+".cs");
            file.CopyTo(pathMgr.CopyCSToPath + file.Name, true);
        }
        if(csNameList.Count>0)
        {
            Console.WriteLine("all cs files copy success.");
        }
    }

    #endregion

    #region 复制生成的Bytes文件到指定目录

    public static void CopyBytesFilesToDir(List<string> bytesNameList)
    {
        if (pathMgr.CopyBytesToDir == pathMgr.rootPath)
        {
            return;
        }
        for (int i = 0; i < bytesNameList.Count; i++)
        {
            FileInfo file = new FileInfo(pathMgr.bytesPath + bytesNameList[i] + ".bytes");
            file.CopyTo(pathMgr.CopyBytesToPath + file.Name, true);
        }
        if (bytesNameList.Count > 0)
        {
            Console.WriteLine("all bytes files copy success.");
        }
    }

    public static void CopyBytesFilesToDir(string bytesName)
    {
        if (pathMgr.CopyBytesToDir == pathMgr.rootPath)
        {
            return;
        }
        FileInfo file = new FileInfo(pathMgr.bytesPath + bytesName + ".bytes");
        file.CopyTo(pathMgr.CopyBytesToPath + file.Name, true);
    }

    #endregion

}