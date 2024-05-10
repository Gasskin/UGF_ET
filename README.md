# 仓库

- Client

所有客户端代码

- Config

所有Excel配置，以及生成C/S对应代码的导表工具（LuBan-Next）

- Server

所有服务器代码

- Server/Config

生成的服务器配置数据，还有Log配置

- Server/Protos

协议定义以及生成

- Server/Share

一些工具的源码，比如代码分析器，协议生成工具等等

- Server/Server

服务器逻辑代码

# 运行

1.打开Server/Share工程，编译Analyzer和Share.SourceGenerator

2.打开Server/Server工程，整体编译

3.回到Server/Share工程，编译Tool

如果报错找不到引用工程，命令行运行一下dotnet test

4.Server/Protos，生成所有协议

5.Config，导一下服务器配置表

6.运行