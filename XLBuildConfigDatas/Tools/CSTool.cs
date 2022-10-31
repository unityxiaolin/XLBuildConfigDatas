using System;
using Spire.Xls;
using Google.Protobuf;
using System.Reflection;
using System.Collections;

public static class CSTool
{
    public static Assembly? CSFileAssembly;
    public static void Init()
    {
        CSFileAssembly = Assembly.LoadFrom(Utils.CompilerCSDLL);
    }
    public static void GenerateBytesFile(string csName, Worksheet sheet)
    {
        if (sheet.Rows.Count() <= 0 || sheet.Columns.Count() <= 0)
        {
            Console.WriteLine($"{sheet.Name}中的数据为空，请检查.");
            return;
        }
        
        Type? classType = CSFileAssembly?.GetType(csName);
        if(classType == null)
        {
            Console.Error.WriteLine("!!! classType is null,please check, classType:"+csName);
            return;
        }
        //类的数组
        Type? classesType = CSFileAssembly?.GetType(sheet.Name+Utils.ConfigClassesSuffix);
        if(classesType == null)
        {
            Console.Error.WriteLine("!!! classesType is null, please check, class name:" + sheet.Name + Utils.ConfigClassesSuffix);
            return;
        }
        IMessage? classesInstance = Activator.CreateInstance(classesType) as IMessage;
        PropertyInfo? classesPropertyInfo = classesType.GetProperty(sheet.Name);
        IList classesRepeatedFiled = (IList)classesPropertyInfo.GetValue(classesInstance);

        int classCount = sheet.Columns[0].CellList.Count - CommonDataMgr.ConfigFrontLineCount;//类的个数
        if(classCount<=0)
        {
            Console.Error.WriteLine("!!!classCount error,please check, classCount:" + classCount);
            return;
        }
        List<IMessage> classList = new List<IMessage>();
        List<Dictionary<string, IList>> allSmallClassRepeatedDicList = new List<Dictionary<string, IList>>();//小类
        List<List<IMessage>> smallConfigClassList = new List<List<IMessage>>();//表格结构体中的每一组
        for (int i = 0; i < classCount; i++)
        {
            IMessage? configClass = Activator.CreateInstance(classType) as IMessage;//创建类的实例对象
            if (configClass == null)
            {
                Console.Error.WriteLine("!!! configClass is null,please check, classType:" + classType.Name);
                return;
            }
            classList.Add(configClass);
            classesRepeatedFiled?.Add(configClass);
            allSmallClassRepeatedDicList.Add(new Dictionary<string, IList>());
            smallConfigClassList.Add(new List<IMessage>());
        }
        
        int excelColumn = 0;//表格中每行数据的列
        List<CellRange> columnList;
        List<CellRange> oneRowCellRanges = sheet.Rows[0].CellList;
        IMessage currentClass;

        int smallClassArrayCount = 0;//小类中的组数,同时也是小类的类个数
        int smallClassMemberCount = 0;//小类中每组的个数
        int curSmallClassArrayIndex = 0;//小类中的组数
        int curSmallClassMemberIndex = 0;//当前执行的个数，会变
        int arrayColumnCount = 0;//表格结构体占有的列数还剩多少列

        string smallClassName = "";

        while (excelColumn < oneRowCellRanges.Count && !string.IsNullOrEmpty(oneRowCellRanges[excelColumn].DisplayedText))
        {
            if (!oneRowCellRanges[excelColumn].DisplayedText.ToLower().Contains("c"))//客户端字段才会读取
            {
                excelColumn++;
                continue;
            }
            List<CellRange> twoRowCellRanges = sheet.Rows[1].CellList;
            List<CellRange> threeRowCellRanges = sheet.Rows[2].CellList;
            List<CellRange> fourRowCellRanges = sheet.Rows[3].CellList;
            string structTypeStr = twoRowCellRanges[excelColumn].DisplayedText;
            string memberTypeStr = threeRowCellRanges[excelColumn].DisplayedText;
            string memberStr = fourRowCellRanges[excelColumn].DisplayedText;

            columnList = sheet.Columns[excelColumn].CellList;//某一列的数据
            for (int i = CommonDataMgr.ConfigFrontLineCount; i < columnList.Count; i++)//遍历
            {
                int classIndex = i - CommonDataMgr.ConfigFrontLineCount;
                if (arrayColumnCount <= 0 || structTypeStr.ToLower().StartsWith("struct"))//表格中的正常类型
                {
                    currentClass = classList[classIndex];
                    //Console.WriteLine("ggggggggggggg:currentClass.GetType().Name:" + currentClass.GetType().Name+ "        arrayColumnCount:"+ arrayColumnCount);
                    PropertyInfo? propertyInfo = currentClass.GetType().GetProperty(memberStr);
                    if (propertyInfo == null)
                    {
                        Console.Error.WriteLine($"!!!!!!!!!!!!!!!!! propertyInfo == null,please check, class={csName} memberStr={memberStr}");
                        return;
                    }
                    if (structTypeStr == Utils.optional)//普通类型
                    {
                        PropertyInfoSetValue(propertyInfo, currentClass, memberTypeStr, memberStr, columnList[i].DisplayedText);
                    }
                    else if (structTypeStr == Utils.repeated)//数组类型
                    {
                        string[] arrayValue = columnList[i].DisplayedText.Split(";");
                        Type? type = propertyInfo.PropertyType.GetGenericArguments()[0];//该数组的类型
                        //Console.WriteLine("type name:" + type.Name + "           propertyInfo.PropertyType:" + propertyInfo.PropertyType);
                        IList repeatedFiled = (IList)propertyInfo.GetValue(currentClass);
                        if (type.Name.ToLower() == Utils.stringType)//字符串类型
                        {
                            for (int j = 0; j < arrayValue.Length; j++)
                            {
                                repeatedFiled.Add(arrayValue[j]);
                            }
                        }
                        else//其他类型
                        {
                            if (type == null)
                            {
                                Console.Error.WriteLine($"array type error,please check,config:{sheet.Name} memberTypeStr:{memberTypeStr}");
                                return;
                            }
                            Type[] types = new Type[1];
                            types[0] = typeof(string);
                            MethodInfo? memberTypeInfo = type.GetMethod("Parse", types);
                            if (memberTypeInfo == null)
                            {
                                Console.Error.WriteLine($"MethodInfo is null, {memberTypeStr} do not have Parse property，please check，config:{sheet.Name}");
                                return;
                            }
                            object[] arrayTypeValues = new object[arrayValue.Length];//对应的类型的数组值
                            for (int j = 0; j < arrayValue.Length; j++)
                            {
                                string[] strings = new string[1];
                                strings[0] = arrayValue[j];
                                arrayTypeValues[j] = memberTypeInfo?.Invoke(null, strings);
                            }
                            //Console.WriteLine("数组字段的类型:" + propertyInfo.PropertyType.Name);
                            for (int j = 0; j < arrayTypeValues.Length; j++)
                            {
                                repeatedFiled.Add(arrayTypeValues[j]);
                            }
                        }
                    }
                    else if (structTypeStr.ToLower().StartsWith("struct"))//表格结构体类型
                    {
                        smallClassName = sheet.Name + memberStr;//表格中的结构体类型
                        Type? smallClassType = CSFileAssembly?.GetType(smallClassName);
                        if (smallClassType == null)
                        {
                            Console.Error.WriteLine("!!! smallClassType is null,please check, classType:" + smallClassName);
                            return;
                        }
                        string[] structStr = structTypeStr.Split("_");
                        smallClassArrayCount = int.Parse(structStr[1]);
                        smallClassMemberCount = int.Parse(structStr[2]);
                        curSmallClassArrayIndex = 0;
                        curSmallClassMemberIndex = 0;

                        Dictionary<string, IList> smallClassRepeatedDic = allSmallClassRepeatedDicList[classIndex];
                        IList? repeatedField;
                        if (!smallClassRepeatedDic.TryGetValue(smallClassName, out repeatedField))
                        {
                            repeatedField = (IList?)currentClass.GetType().GetProperty(memberStr)?.GetValue(currentClass);
                            if (repeatedField == null)
                            {
                                Console.Error.WriteLine($"!!!!! class  get repeatedField value err,please check, currentClass={currentClass.GetType().Name} memberStr={memberStr}");
                                return;
                            }
                            smallClassRepeatedDic.Add(smallClassName, repeatedField);
                        }

                        arrayColumnCount = smallClassArrayCount * smallClassMemberCount+1;
                        curSmallClassMemberIndex -= 1;
                    }
                }
                if(arrayColumnCount > 0 && !structTypeStr.ToLower().StartsWith("struct"))//表格中的结构体类型
                {
                    if (curSmallClassArrayIndex < smallClassArrayCount)//给表格中的小类赋值
                    {
                        if (curSmallClassMemberIndex == 0)//小类开始实例化
                        {
                            Type? smallClassType = CSFileAssembly?.GetType(smallClassName);
                            if (smallClassType == null)
                            {
                                Console.Error.WriteLine("!!!!!!!!! smallClassType is error,please check, smallClassType:" + smallClassType);
                                return;
                            }
                            IMessage? smallClass = Activator.CreateInstance(smallClassType) as IMessage;
                            smallConfigClassList[classIndex].Add(smallClass);
                        }

                        //给小类中的字段赋值
                        IMessage? curSmallClass = smallConfigClassList[classIndex][curSmallClassArrayIndex];
                        PropertyInfo? smallPropertyInfo = curSmallClass.GetType().GetProperty(memberStr);
                        if (smallPropertyInfo == null)
                        {
                            Console.Error.WriteLine($"!!!!!!!!!!!!!!!!! smallClassType do not have {memberStr} attr,please check, class={csName}");
                            return;
                        }
                        if (structTypeStr == Utils.optional)//普通类型
                        {
                            PropertyInfoSetValue(smallPropertyInfo, curSmallClass, memberTypeStr, memberStr, columnList[i].DisplayedText);
                        }
                        else if (structTypeStr == Utils.repeated)//数组类型
                        {
                            string[] arrayValue = columnList[i].DisplayedText.Split(";");
                            PropertyInfoArraySetValue(smallPropertyInfo, curSmallClass, sheet, arrayValue, memberTypeStr);
                        }

                        if (curSmallClassMemberIndex == smallClassMemberCount - 1)//小类赋值完成
                        {
                            Dictionary<string, IList> smallClassRepeatedDic = allSmallClassRepeatedDicList[classIndex];
                            IList? repeatedField;
                            if (!smallClassRepeatedDic.TryGetValue(smallClassName, out repeatedField))
                            {
                                Console.Error.WriteLine($"!!!!! struct get repeatedField value err,please check, currentClass={smallClassName} memberStr={memberStr}");
                                return;
                            }
                            repeatedField.Add(curSmallClass);
                            if (curSmallClassArrayIndex == smallClassArrayCount-1)
                            {
                                smallConfigClassList[classIndex].Clear();//清除一下，留给下一个表格结构体赋值
                            }
                        }
                    }
                }
            }
            excelColumn++;
            if (arrayColumnCount>0)
            {
                curSmallClassMemberIndex += 1;
                if (curSmallClassMemberIndex >= smallClassMemberCount)
                {
                    curSmallClassArrayIndex += 1;
                    curSmallClassMemberIndex = 0;
                }
                arrayColumnCount--;
            }
        }
        sheet.Dispose();
        //赋值完成，开始序列化
        ProtobufSerializeTool.SerializeCSToFile(classesInstance, csName);
    }

    /// <summary>
    /// 给字段赋值，会自动根据该字段的类型来赋予对应的值
    /// </summary>
    /// <param name="propertyInfo">字段的属性信息</param>
    /// <param name="currentClass">当前操作的类</param>
    /// <param name="memberTypeStr">字段的类型，是个字符串来的</param>
    /// <param name="memberStr">字段名，也是字符串</param>
    /// <param name="displayedText">该字段的具体值内容</param>
    static void PropertyInfoSetValue(PropertyInfo propertyInfo, IMessage currentClass, string memberTypeStr, string memberStr, string displayedText)
    {
        if(string.IsNullOrEmpty(displayedText))
        {
            return;
        }
        if (memberTypeStr == Utils.stringType)
        {
            propertyInfo.SetValue(currentClass, displayedText);
        }
        else
        {
            if (propertyInfo.PropertyType == null)
            {
                Console.Error.WriteLine($"propertyInfo.PropertyType is null, please check, class:{currentClass.GetType().Name} memberStr={memberStr}");
                return;
            }
            Type[] types = new Type[1];
            types[0] = typeof(string);
            MethodInfo? memberTypeInfo = propertyInfo.PropertyType.GetMethod("Parse", types);
            if (memberTypeInfo == null)
            {
                Console.Error.WriteLine($"MethodInfo is null, {memberTypeStr} do not have Parse property，please check，class:{currentClass.GetType().Name}");
                return;
            }
            string[] strings = new string[1];
            strings[0] = displayedText;
            propertyInfo.SetValue(currentClass, memberTypeInfo.Invoke(propertyInfo.PropertyType, strings));
        }
    }
    /// <summary>
    /// 给数组赋值，会自动根据该数组的类型来赋予对应的值
    /// </summary>
    /// <param name="propertyInfo">数组的类定义的属性信息</param>
    /// <param name="currentClass">表格结构体的小类</param>
    /// <param name="sheet">表格</param>
    /// <param name="arrayValue">需要保存的数组值</param>
    /// <param name="memberTypeStr">该数组对应的类型</param>
    static void PropertyInfoArraySetValue(PropertyInfo propertyInfo, IMessage currentClass, Worksheet sheet, string[] arrayValue, string memberTypeStr)
    {
        Type? type = propertyInfo.PropertyType.GetGenericArguments()[0];//该数组的类型
        //Console.WriteLine("type name:" + type.Name + "           propertyInfo.PropertyType:" + propertyInfo.PropertyType);
        IList repeatedFiled = (IList)propertyInfo.GetValue(currentClass);
        if (type.Name.ToLower() == Utils.stringType)//字符串类型
        {
            for (int j = 0; j < arrayValue.Length; j++)
            {
                repeatedFiled.Add(arrayValue[j]);
            }
        }
        else//其他类型
        {
            if (type == null)
            {
                Console.Error.WriteLine($"array type error,please check,config:{sheet.Name} memberTypeStr:{memberTypeStr}");
                return;
            }
            Type[] types = new Type[1];
            types[0] = typeof(string);
            MethodInfo? memberTypeInfo = type.GetMethod("Parse", types);
            if (memberTypeInfo == null)
            {
                Console.Error.WriteLine($"MethodInfo is null, {memberTypeStr} do not have Parse property，please check，config:{sheet.Name}");
                return;
            }
            object[] arrayTypeValues = new object[arrayValue.Length];//对应的类型的数组值
            for (int j = 0; j < arrayValue.Length; j++)
            {
                string[] strings = new string[1];
                strings[0] = arrayValue[j];
                arrayTypeValues[j] = memberTypeInfo?.Invoke(null, strings);
            }
            //Console.WriteLine("数组字段的类型:" + propertyInfo.PropertyType.Name);
            for (int j = 0; j < arrayTypeValues.Length; j++)
            {
                repeatedFiled.Add(arrayTypeValues[j]);
            }
        }
    }
}