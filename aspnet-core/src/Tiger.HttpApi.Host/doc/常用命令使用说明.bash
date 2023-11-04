

###  1. 添加数据迁移说明

1. Tiger.HttpApi.Host 项目设置为启动项目
2. 包管理控制台 Tiger.EntityFrameworkCore.DbMigrations 设置为默认项目
3. 执行下面的迁移脚本

``` bash
Add-Migration  迁移文件名称
Add-Migration Created_Book_Entity

Update-Database 

-- 以下命令将生成一个从指定 from 迁移到指定 to 迁移的 SQL 脚本。
Script-Migration AddNewTables AddAuditTable

```
