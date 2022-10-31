@echo off

::proto文件夹路径
set protoFilePath=.\Datas\Proto
 
::protoc.exe
set protoc=.\XLBuildConfigDatas\Tools\ProtoTools\protoc.exe
::C#文件生成路径
set CSGenPath=.\Datas\CS
 
::删除之前创建的文件
del %CSGenPath%\*.* /f /s /q
 
::遍历所有文件
for /f "delims=" %%i in ('dir /b "%protoFilePath%\*.proto"') do (
	::echo %protoc% -i:%protoFilePath%\%%i -o:%CSGenPath%\%%~ni.cs
	%protoc% %protoFilePath%\%%i --csharp_out=%CSGenPath%
)
echo Generate CS File Success!
pause