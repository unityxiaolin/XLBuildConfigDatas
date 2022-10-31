using System;

public class CommonDataMgr
{
    private CommonDataMgr? instance;
    public CommonDataMgr Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new CommonDataMgr();
            }
            return instance;
        }
    }
    /// <summary>
    /// 表格中的前面几列用来描述的，之后的才是数据列
    /// </summary>
    public static int ConfigFrontLineCount = 6;
}