using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Reflection;
using System.Text;

public static class CSCompilerTool
{
    public static void CompilerCSFiles(List<string> csNameList)
    {
        Console.WriteLine("Start Compiler C# file");
        List<string> usingList=new List<string>();
        StringBuilder sourceCodeTextBuilder=new StringBuilder();
        foreach (string csName in csNameList)
        {
            string path = pathMgr.csPath + csName + ".cs";
            foreach (var line in File.ReadAllLines(path))
            {
                if(line.StartsWith("using ")&&line.EndsWith(";"))
                {
                    if(!usingList.Contains(line))
                    {
                        usingList.Add(line);
                    }
                }
                else
                {
                    sourceCodeTextBuilder.AppendLine(line);
                }
            }
        }
        StringBuilder usingStrBuilder=new StringBuilder();
        foreach(string line in usingList)
        {
            usingStrBuilder.AppendLine(line);
        }
        var sourceCodeText = $"{usingStrBuilder}{Environment.NewLine}{sourceCodeTextBuilder}";
        MetadataReference[] systemReference = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.Load("Google.Protobuf").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Collections, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location),
        };
        SyntaxTree syntaxtree = CSharpSyntaxTree.ParseText(sourceCodeText, new CSharpParseOptions(LanguageVersion.Latest));
        string assemblyName = Utils.CompilerCSDLL;
        //创建编译任务
        var compilation = CSharpCompilation.Create(assemblyName)//指定程序集名称
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))//输出为 dll 程序集
            .AddReferences(systemReference)//添加程序集引用
            .AddSyntaxTrees(syntaxtree);// 添加上面代码分析得到的语法树
        string assemblyPath = $"{assemblyName}.dll";
        var compilationResult = compilation.Emit(assemblyPath);
        if(compilationResult.Success)
        {
            // 编译成功，获取编译后的程序集并从中获取数据库表信息以及字段信息
            Console.WriteLine("Compiler C# file success.");
        }
        else
        {
            // 如果编译失败，则输出诊断消息
            Console.Error.WriteLine("!!! Compiler C# file error, please check.");
            foreach (var diagnostic in compilationResult.Diagnostics)
            {
                Console.WriteLine(diagnostic.ToString());
            }
        }
        //Console.WriteLine("编译完成");
    }
    
}
