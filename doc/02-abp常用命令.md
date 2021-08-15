# abp 常用命令整理



# [Powershell介绍以及常用命令](https://juejin.cn/post/6854573219232350221)

#### Powershell介绍

在不同的操作系统中，会有不同的命令提示符。在Mac中，默认使用的就是Bash，也有好多人通过oh my zsh使用zsh。而在Windows系统，命令行提示符有CMD.exe和Powershell两种。

Powershell是cmd的超集，换句话说，cmd能做的事情，Powershell都能做，可以直接在Powershell中执行CMD的命令，而且Powershell还能额外做许多cmd不能做的事情。

#### PowerShell里的变量使用

变量可以临时保存数据，使用的时候可以把把数据存在变量中，以便进一步操作。

##### 变量名格式

> 在powershell中变量名均是以美元符””开始，剩余字符可以是数字、字母、下划线的任意字符，并且powershell变量名大小写不敏感（”开始，剩余字符可以是数字、字母、下划线的任意字符，并且*p**o**w**e**r**s**h**e**l**l*变量名大小写不敏感（a和$A 是同一个变量)。

某些特殊的字符在powershell中有特殊的用途，一般不推荐使用这些字符作为变量名。当然你硬要使用，请把整个变量名后缀用花括号括起来。

```
PS C:\test> ${"I"like $}="test"
PS C:\test> ${"I"like $}
test
复制代码
```

##### 查找正在使用的变量

```
PS C:\Users> ls variable:

Name                           Value
----                           -----
"I"like $                      test
...
复制代码
```

##### 设置变量

```
PS D:\> $a = 5
PS D:\> $a * 4
20
PS D:\> "sdf".substring(2)
f
PS D:\> $str = "powershell"
PS D:\> $str.substring($str.indexOf("s"))
shell
复制代码
```

powershell支持.net的方法：截取字符串、if语句判断等这些都没有问题。可以根据自己需要来去使用。

常用的变量知识大概这么多，其它更多变量知识可以参考[Powershell 定义变量](https://link.juejin.cn/?target=https%3A%2F%2Fwww.pstips.net%2Fpowershell-define-variable.html)。

#### PowerShell常用命令

##### Start-Process，别名：start + 路径， 打开当前文件夹

```
# 把路径存储为变量，打开路径地址的文件
PS D:\> $variable = "D:\"
PS D:\> start $variable

# 以管理员身份启动Powershell
PS D:\> start -FilePath "powershell" -Verb RunAs
复制代码
```

##### Get-ChildItem，别名：ls、dir, 列出文件夹下所有文件

```
PS D:\> ls


    目录: D:\


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----        2020/1/20     11:58                Downloads
...
复制代码
```

##### Get-History，别名：history、h，列出之前的操作命令

```
PS D:\> history

  Id CommandLine
  -- -----------
   1 Start D:
   2 Start D
   3 ${"I"like $}="test"
   ...
复制代码
```

##### Get-Process， 别名：ps，查找进程, 可以通过进程名称或者进程ID来获取特定进程

```
 ➜  ~  ps wechat

Handles  NPM(K)    PM(K)      WS(K)     CPU(s)     Id  SI ProcessName
-------  ------    -----      -----     ------     --  -- -----------
   1255      81   153952     183020     220.53   5832   2 WeChat
复制代码
```

##### new-item，别名：ni <filename.txt>, 创建一个新的文本文件

```
PS D:\> ni test.txt
    目录: D:\
Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----        2020/7/25     16:29              0 test.txt
复制代码
```

##### remove-item，别名： rm、del, 删除或删除文件

```
PS D:\> rm test.txt
复制代码
```

##### copy-item，别名：cp,\copy, 复制文件

```
PS D:\> cp test.txt test1.txt
复制代码
```

##### get-location，别名：pwd, 当前目录位置

```
PS D:\> pwd
Path
----
D:\
复制代码
```

##### GET-HELP, 缩写help，查看命令的帮助

```
get-help get-process
复制代码
```

##### 其他命令

- get-date, 别名：date，获取系统当前时间
- Get-Command，别名：gcm， 查找所有命令，可以通过通配符查找，如：get-command *process
- write-output, 别名：echo、 write, 把东西输出发送到管道，从那里它可以通过管道传输到另一个cmdlet或者变量
- write-host 直接输出到控制台
- get-content，别名：cat, 输出文件内容到控制台
- Install-Module module-name -Scope CurrentUser，从在线库中找到包并安装包到本地
- Import-Module module-name，导入包
- chcp, 修改当前的编码方式，默认936(GB2312)，可以通过`chcp 65001`设置为UTF-8格式

更多可以查看[PowerShell官方文档](https://link.juejin.cn/?target=https%3A%2F%2Fdocs.microsoft.com%2Fen-us%2Fpowershell%2Fmodule%2FPowerShellGet%2FInstall-Module%3Fview%3Dpowershell-7)。

###### write-output 与 write-host 区别

```
$a = 'Testing Write-OutPut'  | Write-Output
$b = 'Testing Write-Host' | Write-Host

Get-Variable a,b
复制代码
```

##### 编辑器

- [Windows Terminal](https://juejin.cn/post/6850418122147659783)，微软新出的命令行工具，界面很好用，可以直接在Store中下载安装，如果想配置好看的Terminal主题和git配置，可以点击链接查看
- [CMDER](https://link.juejin.cn/?target=https%3A%2F%2Fcmder.net%2F), 默认安装了很多好用的配置，比如git，也附带了好看的主题，喜欢的可以直接官网下载安装。







# EF migration 常用命令

# [EF常用命令](https://www.cnblogs.com/liuhuimh/p/12693014.html)

1.Add-Migration 添加一个新的迁移（名词）,string是迁移的名称
2.remove-Migration 删除上一次的迁移
3.Update-Database 更新最近一次的迁移到数据库（有时候可能与原来的数据库产生冲突，所以可能并不能迁移到数据库）
4.Update-Database -Migration 0 表示清空数据库，回到空数据库状态
5.Script-Migration 查看迁移脚本



## [NUGET命令](https://www.cnblogs.com/xcsn/p/6259853.html)

CMD将nuget升级到最新版本：**nuget update -self**



### 一、安装

1.安装指定版本类库
install-package <程序包名> -version *<版本号>*

2.安装到指定的项目
install-package <程序包名> -project XXXProjectName -version *<版本号>*



### 二、更新

Update-Package <程序包名>

 

### 三、重新安装

1.重新安装所有Nuget包(整个解决方案都会重新安装)
update-package -reinstall

2.重新安装指定项目所有Nuget包
update-package -project *<项目名称>* -reinstall



### 四、卸载

1.正常卸载：uninstall-package <程序包名>

2.强制卸载：Uninstall-Package <程序包名> -Force

 



### 五、查询

Get-Package 默认列出本地已经安装了的包 可以加参数 -remote -filter entityframework 来在包源中查找自己想要的包





# ABP CLI

ABP CLI (命令行接口) 是一个命令行工具,用来执行基于ABP解决方案的一些常见操作.

## Installation

ABP CLI 是一个 [dotnet global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools). 使用命令行窗口安装:

```bash
dotnet tool install -g Volo.Abp.Cli
```

Bash

Copy

更新最新版本:

```bash
dotnet tool update -g Volo.Abp.Cli
```

Bash

Copy

## 全局选项

虽然每个命令可能都有一组选项,但有些全局选项可以与任何命令一起使用:

- `--skip-cli-version-check`: 跳过检查最新版本的ABP CLI. 如果没有指定,它会检查最新版本,如果检查到ABP CLI的新版本,会显示一条警告消息.

## Commands

这里是所有可用的命令列表:

- **`help`**: 展示ABP CLI的用法帮助信息.
- **`new`**：生成基于ABP的[启动模板](https://docs.abp.io/zh-Hans/abp/latest/Startup-Templates/Index).
- **`update`**：自动更新的ABP解决方案ABP相关的NuGet和NPM包.
- **`add-package`**: 添加ABP包到项目.
- **`add-module`**: 添加[应用模块](https://docs.abp.io/en/abp/latest/Modules/Index)到解决方案.
- **`generate-proxy`**: 生成客户端代理以使用HTTP API端点.
- **`remove-proxy`**: 移除以前生成的客户端代理.
- **`switch-to-preview`**: 切换到ABP框架的最新预览版本。
- **`switch-to-nightly`**: 切换解决方案所有ABP相关包为[夜间构建](https://docs.abp.io/zh-Hans/abp/latest/Nightly-Builds)版本.
- **`switch-to-stable`**: 切换解决方案所有ABP相关包为最新的稳定版本.
- **`translate`**: 当源代码控制存储库中有多个JSON[本地化]（Localization.md文件时,可简化翻译本地化文件的过程.
- **`login`**: 使用你在[abp.io](https://abp.io/)的用户名和密码在你的计算机上认证.
- **`logout`**: 在你的计算机注销认证.
- **`install-libs`**: 为 MVC / Razor Pages 和 Blazor Server UI 类型安装NPM包.

### help

展示ABP CLI的基本用法:

用法:

```bash
abp help [command-name]
```

Bash

Copy

示例:

```bash
abp help        # Shows a general help.
abp help new    # Shows help about the "new" command.
```

Bash

Copy

### new

生成基于ABP[启动模板](https://docs.abp.io/zh-Hans/abp/latest/Startup-Templates/Index)的新解决方案.

用法:

```bash
abp new <解决方案名称> [options]
```

Bash

Copy

示例:

```bash
abp new Acme.BookStore
```

Bash

Copy

- Acme.BookStore是解决方案的名称.
- 常见的命名方式类似于 *YourCompany.YourProject*. 不过你可以使用自己喜欢的方式,如 *YourProject* (单级命名空间) 或 *YourCompany.YourProduct.YourModule* (三级命名空间).

#### Options

- ```
  --template
  ```

   

  或者

   

  ```
  -t
  ```

  : 指定模板. 默认的模板是

   

  ```
  app
  ```

  ,会生成web项目.可用的模板有:

  - ```
    app
    ```

     

    (default):

     

    应用程序模板

    . 其他选项:

    - ```
      --ui
      ```

       

      或者

       

      ```
      -u
      ```

      : 指定ui框架.默认

      ```
      mvc
      ```

      框架.其他选项:

      - ```
        mvc
        ```

        : ASP.NET Core MVC.此模板的其他选项:

        - `--tiered`: 创建分层解决方案,Web和Http Api层在物理上是分开的.如果未指定会创建一个分层的解决方案,此解决方案没有那么复杂,适合大多数场景.

      - ```
        angular
        ```

        : Angular. 这个模板还有一些额外的选项:

        - `--separate-identity-server`: 将Identity Server应用程序与API host应用程序分开. 如果未指定,则服务器端将只有一个端点.

      - ```
        blazor
        ```

        : Blazor. 这个模板还有一些额外的选项:

        - `--separate-identity-server`: 将Identity Server应用程序与API host应用程序分开. 如果未指定,则服务器端将只有一个端点.

      - ```
        none
        ```

        : 无UI. 这个模板还有一些额外的选项:

        - `--separate-identity-server`: 将Identity Server应用程序与API host应用程序分开. 如果未指定,则服务器端将只有一个端点.

    - ```
      --mobile
      ```

       

      或者

       

      ```
      -m
      ```

      : 指定移动应用程序框架. 如果未指定,则不会创建任何移动应用程序,其他选项:

      - `none`: 不包含移动应用程序.
      - `react-native`: React Native.

    - ```
      --database-provider
      ```

       

      或者

       

      ```
      -d
      ```

      : 指定数据库提供程序.默认是

       

      ```
      ef
      ```

      .其他选项:

      - `ef`: Entity Framework Core.
      - `mongodb`: MongoDB.

  - ```
    module
    ```

    :

     

    Module template

    . 其他选项:

    - `--no-ui`: 不包含UI.仅创建服务模块(也称为微服务 - 没有UI).

  - **`console`**: [Console template](https://docs.abp.io/zh-Hans/abp/latest/Startup-Templates/Console).

- `--output-folder` 或者 `-o`: 指定输出文件夹,默认是当前目录.

- `--version` 或者 `-v`: 指定ABP和模板的版本.它可以是 [release tag](https://github.com/abpframework/abp/releases) 或者 [branch name](https://github.com/abpframework/abp/branches). 如果没有指定,则使用最新版本.大多数情况下,你会希望使用最新的版本.

- `--preview`: 使用最新的预览版本.

- `--template-source` 或者 `-ts`: 指定自定义模板源用于生成项目,可以使用本地源和网络源(例如 `D:\local-templat` 或 `https://.../my-template-file.zip`).

- `--create-solution-folder` 或者 `-csf`: 指定项目是在输出文件夹中的新文件夹中还是直接在输出文件夹中.

- `--connection-string` 或者 `-cs`: 重写所有 `appsettings.json` 文件的默认连接字符串. 默认连接字符串是 `Server=localhost;Database=MyProjectName;Trusted_Connection=True`. 默认的数据库提供程序是 `SQL Server`. 如果你使用EF Core但需要更改DBMS,可以按[这里所述](https://docs.abp.io/zh-Hans/abp/latest/Entity-Framework-Core-Other-DBMS)进行更改(创建解决方案之后).

- `--local-framework-ref --abp-path`: 使用对项目的本地引用,而不是替换为NuGet包引用.

### update

更新所有ABP相关的包可能会很繁琐,框架和模块都有很多包. 此命令自动将解决方案或项目中所有ABP相关的包更新到最新版本.

用法:

```bash
abp update [options]
```

Bash

Copy

- 如果你的文件夹中有.sln文件,运行命令会将解决方案中所有项目ABP相关的包更新到最新版本.
- 如果你的文件夹中有.csproj文件,运行命令会将项目中所有ABP相关的包更新到最新版本.

#### Options

- `--npm`: 仅更新NPM包
- `--nuget`: 仅更新的NuGet包
- `--solution-path` 或 `-sp`: 指定解决方案路径/目录. 默认使用当前目录
- `--solution-name` 或 `-sn`: 指定解决方案名称. 默认在目录中搜索`*.sln`文件.
- `--check-all`: 分别检查每个包的新版本. 默认是 `false`.
- `--version` or `-v`: 指定用于升级的版本. 如果没有指定,则使用最新版本.

### add-package

通过以下方式将ABP包添加到项目中

- 添加nuget包做为项目的依赖项目.
- 添加 `[DependsOn(...)]` attribute到项目的模块类 (请参阅 [模块开发文档](https://docs.abp.io/zh-Hans/abp/latest/Module-Development-Basics)).

> 需要注意的是添加的模块可能需要额外的配置,通常会在包的文档中指出.

用法:

```bash
abp add-package <包名> [options]
```

Bash

Copy

示例:

```
abp add-package Volo.Abp.MongoDB
```

- 示例中将Volo.Abp.MongoDB包添加到项目中.

#### Options

- `--project` 或 `-p`: 指定项目 (.csproj) 路径. 如果未指定,Cli会尝试在当前目录查找.csproj文件.
- `--with-source-code`: 下载包的源码到你的解决方案文件夹，而不是NuGet/NPM软件包.
- `--add-to-solution-file`: 添加下载/创建的包添加到解决方案文件中,你在IDE中打开解决方案时也会看到包的项目. (仅当 `--with-source-code` 为 `True` 时可用.)

### add-module

通过查找模块的所有包,查找解决方案中的相关项目,并将每个包添加到解决方案中的相应项目,从而将[多包应用程序模块](https://docs.abp.io/zh-Hans/abp/latest/Modules/Index)添加到解决方案中.

> 由于分层,不同的数据库提供程序选项或其他原因,业务模块通常由多个包组成. 使用`add-module`命令可以大大简化向模块添加模块的过程. 但是每个模块可能需要一些其他配置,这些配置通常在相关模块的文档中指出.

用法:

```bash
abp add-module <模块名称> [options]
```

Bash

Copy

示例:

```bash
abp add-module Volo.Blogging
```

Bash

Copy

- 示例中将Volo.Blogging模块添加到解决方案中.

#### Options

- `--solution` 或 `-s`: 指定解决方案 (.sln) 路径. 如果未指定,CLI会尝试在当前目录中寻找.sln文件.
- `--skip-db-migrations`: 对于EF Core 数据库提供程序,它会自动添加新代码的第一次迁移 (`Add-Migration`) 并且在需要时更新数据库 (`Update-Database`). 指定此选项可跳过此操作.
- `-sp` 或 `--startup-project`: 启动项目的项目文件夹的相对路径. 默认值是当前文件夹.
- `--with-source-code`: 添加模块的源代码,而不是NuGet/NPM软件包.
- `--add-to-solution-file`: 添加下载/创建的模块添加到解决方案文件中,你在IDE中打开解决方案时也会看到模块的项目. (仅当 `--with-source-code` 为 `True` 时可用.)

### generate-proxy

为您的HTTP API生成Angular服务代理,简化从客户端使用服务的成本. 在运行此命令之前,你的host必须启动正在运行.

用法:

```bash
abp generate-proxy
```

Bash

Copy

#### Options

- `--module` 或 `-m`: 指定要为其生成代理的后端模块的名称. 默认值: `app`.
- `--api-name` 或 `-a`: 在 `/src/environments/environment.ts` 中定义的API端点名称。. 默认值: `default`.
- `--source` 或 `-s`: 指定解析根名称空间和API定义URL的Angular项目名称. 默认值: `defaultProject`
- `--target` 或 `-t`: 指定放置生成的代码的Angular项目名称. 默认值: `defaultProject`.
- `--prompt` 或 `-p`: 在命令行提示符下询问选项(未指定的选项).

> 参阅 [Angular服务代理文档](https://docs.abp.io/zh-Hans/abp/latest/UI/Angular/Service-Proxies) 了解更多.

### remove-proxy

从Angular应用程序中删除以前生成的代理代码. 在运行此命令之前,你的host必须启动正在运行.

This can be especially useful when you generate proxies for multiple modules before and need to remove one of them later.

Usage:

```bash
abp remove-proxy
```

Bash

Copy

#### Options

- `--module` 或 `-m`: 指定要为其生成代理的后端模块的名称. 默认值: `app`.
- `--api-name` 或 `-a`: 在 `/src/environments/environment.ts` 中定义的API端点名称。. 默认值: `default`.
- `--source` 或 `-s`: 指定解析根名称空间和API定义URL的Angular项目名称. 默认值: `defaultProject`
- `--target` 或 `-t`: 指定放置生成的代码的Angular项目名称. 默认值: `defaultProject`.
- `--prompt` 或 `-p`: 在命令行提示符下询问选项(未指定的选项).

> 参阅 [Angular服务代理文档](https://docs.abp.io/zh-Hans/abp/latest/UI/Angular/Service-Proxies) 了解更多.

### switch-to-preview

你可以使用此命令将项目切换到ABP框架的最新预览版本.

用法:

```bash
abp switch-to-preview [options]
```

Bash

Copy

#### Options

- `--solution-directory` 或 `-sd`: 指定目录. 解决方案应该在该目录或其子目录中. 如果未指定默认为当前目录.

### switch-to-nightly

想要切换到ABP框架的最新[每晚构建](https://docs.abp.io/zh-Hans/abp/latest/Nightly-Builds)预览版可以使用此命令.

用法:

```bash
abp switch-to-nightly [options]
```

Bash

Copy

#### Options

`--solution-directory` 或 `-sd`: 指定目录. 解决方案应该在该目录或其子目录中. 如果未指定默认为当前目录.

### switch-to-stable

如果你使用的是ABP框架预览包(包括每晚构建),可以使用此命令切换回最新的稳定版本.

用法:

```bash
abp switch-to-stable [options]
```

Bash

Copy

#### Options

`--solution-directory` 或 `-sd`: 指定目录. 解决方案应该在该目录或其子目录中. 如果未指定默认为当前目录.

### translate

源代码控制存储库中有多个JSON[本地化](https://docs.abp.io/zh-Hans/abp/latest/Localization)文件时,用于简化翻译[本地化](https://docs.abp.io/zh-Hans/abp/latest/Localization)文件的过程.

- 该命令将基于参考文化创建一个统一的json文件
- 它搜索当前目录和所有子目录中的所有本地化"JSON"文件(递归). 然后创建一个包含所有需要翻译的条目的文件(默认情况下名为 "abp-translation.json").
- 翻译了此文件中的条目后,你就可以使用 `--apply` 命令将更改应用于原始本地化文件.

> 该命令的主要目的是翻译ABP框架本地化文件(因为[abp仓库](https://github.com/abpframework/abp)包括数十个要在不同目录中转换的本地化文件).

#### 创建翻译文件

第一步是创建统一的翻译文件:

```bash
abp translate -c <culture> [options]
```

Bash

Copy

示例:

```bash
abp translate -c de-DE
```

Bash

Copy

该命令为 `de-DE` (德语)文化创建了统一的翻译文件.

##### 附加选项

- `--reference-culture` 或 `-r`: 默认值 `en`. 指定参考文化.
- `--output` 或 `-o`: 输出文件名. 默认值 `abp-translation.json`.
- `--all-values` 或 `-all`: 包括所有要翻译的键. 默认情况下,统一翻译文件仅包含目标文化的缺失文本. 如果你可能需要修改之前已经翻译的值,请指定此参数.

#### 应用更改

翻译完统一翻译文件中的条目后,你可以使用 `--apply` 参数将更改应用于原始本地化文件:

```bash
abp translate --apply  # apply all changes
abp translate -a       # shortcut for --apply
```

Bash

Copy

然后,检查源代码控制系统上的更改,以确保它已更改了正确的文件. 如果你翻译了ABP框架资源, 请发送 "Pull Request". 提前感谢你的贡献.

##### 附加选项

- `--file` 或 `-f`: 默认值: `abp-translation.json`. 翻译文件(仅在之前使用过 `--output` 选项时使用).

### login

CLI的一些功能需要登录到abp.io平台. 使用你的用户名登录

```bash
abp login <username>                                  # Allows you to enter your password hidden
abp login <username> -p <password>                    # Specify the password as a parameter (password is visible)
abp login <username> --organization <organization>    # If you have multiple organizations, you need set your active organization
abp login <username> -p <password> -o <organization>  # You can enter both your password and organization in the same command
```

Bash

Copy

> 当使用-p参数,请注意,因为你的密码是可见的. 它对于CI / CD自动化管道很有用.

请注意,新的登录将终止先前的会话并创建一个新的会话.

### logout

通过从计算机中删除会话令牌来注销.

```
abp logout
```

### install-libs

为 MVC / Razor Pages 和 Blazor Server UI 类型安装NPM包, 它的 **执行目录** 或者传递的 `--working-directory` 目录必须包含一个项目文件(*.csproj).

`install-libs` 命令读取 `abp.resourcemapping.js` 来管理包. 参阅[客户端包管理](https://docs.abp.io/zh-Hans/abp/latest/UI/AspNetCore/Client-Side-Package-Management)了解更多细节.

用法:

```bash
abp install-libs [options]
```

Bash

Copy

#### Options

- `--working-directory` 或 `-wd`: 指定工作目录, 当执行目录不包含项目文件时会很有用.