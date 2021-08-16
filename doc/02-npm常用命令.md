

# npm 常用命令

## 常用命令

- 查找帮助
  - `npm -l`
    显示所有可用命令
  - `npm <command> -h`
    查看 `<command>` 命令的简单使用帮助
  - `npm help <command>`
    查看 `<command>` 命令的详细使用指南
- 创建项目
  - `npm init`
    在命令行所在的文件夹初始化一个项目（创建 `package.json` 文件）
- 安装模块
  - `npm install package-name`
    局部安装模块，安装在命令行所在的文件夹；并将模块依赖写入到 `package.json` 文件的 `dependencies` 中（生产环境）
    简写：`npm i package-name`
  - `npm install --save-prod package-name`
    局部安装时将模块依赖写入到 `package.json` 文件的 `dependencies` 中（生产环境）
    简写：`npm install -P package-name`
  - `npm install --save-dev package-name`
    局部安装时将模块依赖写入到 `package.json` 文件的 `devDependencies` 中（开发环境）
    简写：`npm install -D package-name`
  - `npm install -g package-name`
    全局安装模块
- 卸载模块
  - `npm uninstall package-name`
    卸载局部模块
  - `npm uninstall -g package-name`
    卸载全局模块
- 更新模块
  - `npm update package-name`
    更新局部模块
  - `npm update -g package-name`
    更新全局模块
  - `npm install -g package-name@x.x.x`
    更新全局模块 `package-name` 到 `x.x.x` 版本
- 查看某个模块的全部版本
  - `npm view package-name versions`

## 国内优秀 npm 镜像

### 淘宝 npm 镜像

- 搜索地址：[npm.taobao.org/](https://link.juejin.cn/?target=https%3A%2F%2Fnpm.taobao.org%2F)
- registry 地址：[registry.npm.taobao.org/](https://link.juejin.cn/?target=https%3A%2F%2Fregistry.npm.taobao.org%2F)

### cnpmjs 镜像

- 搜索地址：[cnpmjs.org/](https://link.juejin.cn/?target=https%3A%2F%2Fcnpmjs.org%2F)
- registry 地址：[r.cnpmjs.org/](https://link.juejin.cn/?target=https%3A%2F%2Fr.cnpmjs.org%2F)

## 使用方法

### 临时使用

```
npm install package-name --registry=https://registry.npm.taobao.org
复制代码
```

### 长期使用

```
npm config set registry=https://registry.npm.taobao.org
复制代码
```

### 使用 cnpm

```
npm install -g cnpm --registry=https://registry.npm.taobao.org
复制代码
```

使用 `cnpm` 命令代替 `npm` 命令

```
cnpm install package-name
复制代码
```





# npm 常用命令

在使用npm时，官方的源下载npm包会比较慢，国内我们基本使用淘宝的源，最近公司内部搭建了一套npm私有仓库。要添加自己公司内部的npm源，公司内部的源不可能把npm官方的npm包都全量同步，故需要npm源之间的切换，如果使用npm registry xxx的话，太不好管理了。nrm是管理npm源切换的利器。使用方法如下：
 安装nrm



```undefined
npm install -g nrm
```

nrm --help



```bash
Usage: nrm [options] [command]


  Commands:

    ls                           list all the registries
    current                      show current registry name
    use <registry>               change registry to registry
    add <registry> <url> [home]  add one custom registry
    del|rm <registry>            delete one custom registry
    home <registry> [browser]    open the homepage of registry with optional browser
    test [registry]              show response time for specific or all registries
    help                         print this help

  Options:

    -h, --help     output usage information
    -V, --version  output the version number
```

主要使用ls和use命令
 1)nrm  ls是列出来现在已经配置好的所有的原地址



```undefined
nrm ls
```



```cpp
 npm ---- https://registry.npmjs.org/
* cnpm --- http://r.cnpmjs.org/
  taobao - http://registry.npm.taobao.org/
  nj ----- https://registry.nodejitsu.com/
  rednpm - http://registry.mirror.cqupt.edu.cn
  npmMirror  https://skimdb.npmjs.com/registry
```

2)nrm use是切换到哪个源上



```php
nrm use npm
```

3)nrm add添加源
 4)nrm del删除源
 5)nrm test测试源的响应时间，可以作为使用哪个源的参考