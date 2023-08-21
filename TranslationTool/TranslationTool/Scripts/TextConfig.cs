using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TextConfig
{
    public uint Id;
    public string Chinese;//中文
    public string English;//英文
}

class TextCompareConfig
{
    public uint Id;
    public string NewChinese;//线上文本表的中文
    public string OldChinese;//翻译表中的老中文
    public string CompareAdd;//对比后发现是新增的==>新中文不为空&&新中文不等于旧中文
    public string OldEnglish;//线上文本表中的英文
    public string NewEnglish;//翻译表中的英文
    public string CompareFinalEnglish;//对比后取的英文==》新英文不为空，取新英文。新英文为空，取旧英文

    public void InitCompareAddStr()
    {
        if(!string.IsNullOrEmpty(NewChinese)&&!NewChinese.Equals(OldChinese))
        {
            CompareAdd = NewChinese;
        }
        else
        {
            CompareAdd = "";
        }
    }

    public void InitFinalEnglish()
    {
        if(!string.IsNullOrEmpty(NewEnglish))
        {
            CompareFinalEnglish = NewEnglish;
        }
        else
        {
            CompareFinalEnglish = OldEnglish ?? "";
        }
    }

}