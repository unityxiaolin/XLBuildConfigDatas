using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LogMgr
{
    private static StringBuilder m_textLog = new StringBuilder("");
    public static void InitLog()
    {
        AppDomain currentDomain = default(AppDomain);
        currentDomain = AppDomain.CurrentDomain;
        // Handler for unhandled exceptions.
        currentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
        // Handler for exceptions in threads behind forms.
        //System.Application.ThreadException += GlobalThreadExceptionHandler;
    }

    private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = default(Exception);
        ex = (Exception)e.ExceptionObject;
        AddTextLog(ex.Message + "\n" + ex.StackTrace);
        SaveTextLog();
    }

    private static void GlobalThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        Exception ex = default(Exception);
        ex = e.Exception;
        AddTextLog(ex.Message + "\n" + ex.StackTrace);
        SaveTextLog();
    }

    public static void AddTextLog(string str)
    {
        m_textLog.Append("\n" + str);
    }

    public static void SaveTextLog()
    {
        FileStream fs = new FileStream("TranslationLog.txt", FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(m_textLog.ToString());
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
    }
}