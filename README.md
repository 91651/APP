# 基于跨平台解决方案.net core和vue框架的通用开源系统


## 简介

本系统为个人开发和维护的一个项目，旨在于学习和分享技术体系。目前项目处理初始阶段，结构和功能上有诸多不完善的地方，我会逐步的改进和修复。

项目前后端分离，后端基于.net core跨平台解决方案（EF code first模式）， 前端基于vue的SPA构建。

## 技术栈

#### 后端

 - .Net Core
 - Entity Framework Core
 - IOC
 - AutoMap
 - AspNetCore Identity
 - JWT
 - Swagger (NSwag)

#### 前端

 - Vue
 - Vuex
 - Iview
 - Typescript
 - Fetch
 - Bable
 - Swagger Code Generate (NSwag)

## 使用

确保已经完整克隆本仓储到本地。

使用VS(建议visual studio 2017)打开解决方案 APP.sln，设置APP.UI.Admin为启动项目，直接运行，项目会自动编译后端代码，并调用 npm i 安装前端所要的package，安装完成后，自动执行npm run serve运行前端代码。

如果只单纯展示前端SPA应用，请定位项目到 ClientApp 目录，执行 npm i 安装所需package，安装完成后， 执行 npm run serve 启动应用。

## 反馈

提交issue 或者 邮件到mail@521.org.cn ，我会第一时间处理。

## License

MIT