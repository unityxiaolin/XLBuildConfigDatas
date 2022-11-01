# XLBuildConfigDatas
打表工具 Unity C#

在path.txt中配置生成的文件的路径，路径包括配置表、CS文件、Proto文件、二进制文件、MD5文件、复制到的CS路径、复制到的bytes路径。

表格文件在configs里，修改表格或者新增表格后，双击打表.bat就可以执行打表操作了，生成的文件在Datas目录下，
首先生成表格所对应的proto文件，再生成我们用到的C#文件，然后根据表格中配的数据生成对应的bytes文件，bytes文件为表格数据序列化后的二进制数据，
然后会为表格生成唯一的md5码来识别这个表格文件是否已经打表了。最后会将CS文件和bytes文件复制到指定目录，供反序列化使用，默认为Datas\TempCopyCSDir路径和Datas\TempCopyBytesDir路径，可修改，一般这两个路径是填到Unity的Assets路径里面的。


打出来的二进制文件非常小，序列化和反序列化也很快。