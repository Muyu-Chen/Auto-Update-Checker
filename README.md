# Auto-Update-Checker
 Auto Update software is a tool designed for automatic updates. By checking the JSON file on a remote server to compare the current version with the latest version, the software automatically downloads, extracts, and replaces files to ensure it is always up-to-date.     
 Auto Update 软件是一款用于自动更新的工具。通过检查远程服务器上的 JSON 文件来对比当前版本和最新版本号，软件实现自动下载、解压和覆盖操作，从而保持软件始终为最新版本。  

# How to use?
 在你需要更新的应用程序中，使用系统的调用命令调用此exe，例如在C语言中：  
 ```
 // 第一个参数 3.2.1 为目前程序的版本号，建议硬编码进入程序
 // 第二个参数为你的网址，可以放入配置文件中，或硬编码
 path = "path\\to\\dic\\Auto-Update-Checker.exe 3.2.1 \"http://xxx/xxx.json\""
 // .data为传递指针
 int result = system(path.data());  
 if(result == 1){
    // 本软件会直接更新，未来将加入弹窗来确认是否更新；
    // 在result结果出来的时候，文件已经下载好，
    // 并在5s后开始覆盖文件，请及时关闭主程序，避免更新失败。
    printf("正在更新...");
    return 0; //退出程序，避免占用导致更新失败
 }
 ```
 其中仅支持三位版本号，若仅使用两位版本号，可以直接把最后一位写为0即可。  
 
 返回值为1：新版本下载完成，5s后更新，请在5s内退出所有占用；  
 返回值为0：版本是最新版。  
 ## How to use?
 In your application that requires updates, use a system call to invoke this `.exe` file. For example, in C language:

 ```
 // The first parameter "3.2.1" represents the current version of the program. It's recommended to hardcode this in your program.
 // The second parameter is your URL, which can be stored in a configuration file or hardcoded.
 path = "path\\to\\dic\\Auto-Update-Checker.exe 3.2.1 \"http://xxx/xxx.json\""
 // .data is used to pass a pointer
 int result = system(path.data());
 if(result == 1){
     // This software will directly update. In the future, a popup will be added to  confirm the update.
     // When the result is returned, the file has already been downloaded,
     // and the update will begin after 5 seconds. Please close the main program in time to avoid update failure.
    printf("Updating...");
    return 0; // Exit the program to avoid file occupation causing the update to fail.
 }
 ```
 Only three-segment version numbers are supported. If you only need two-segment version numbers, you can set the last segment to 0.

 Return Values:
 1: A new version has been downloaded. The update will start after 5 seconds. Please close all processes within 5 seconds.
 0: The version is already up-to-date.
 
# About compilation | 如何编译？

要编译此项目，请按照以下步骤操作：  

1. **安装 Visual Studio**：
   确保您已安装 Visual Studio，并且安装了 .NET Framework 开发工具。您可以从 [Visual Studio 官方网站](https://visualstudio.microsoft.com/) 下载并安装。

2. **克隆或下载项目**：
   将项目克隆或下载到您的本地计算机。

3. **打开解决方案**：
   使用 Visual Studio 打开项目根目录下的 `ConsoleApp1.sln` 解决方案文件。

4. **还原 NuGet 包**：
   在解决方案资源管理器中右键点击解决方案，然后选择“还原 NuGet 包”。这将下载并安装项目所需的所有依赖项。根据Visual Studio报错的内容搜索安装即可。  

5. **构建项目**：
   在 Visual Studio 中，选择“生成”菜单，然后点击“生成解决方案”，随后按 `Ctrl+Shift+B`（即“启动”）。这将编译项目并生成可执行文件。目前已经配置为，Release版本打包到一个exe文件中。参考配置链接：[博客园：WPF程序只生成一个exe文件](https://www.cnblogs.com/luziking/p/15032206.html)  

6. **运行项目**：
   在 Visual Studio 中，选择“调试”菜单，然后点击“开始调试”或按 `F5`。这将启动项目并运行程序。

编译成功后，您可以在项目的 `bin\Debug` 或 `bin\Release` 目录下找到生成的可执行文件。  

如果您遇到任何问题，请参考项目的 [README.md](README.md) 文件或联系[项目发起者](https://github.com/Muyu-Chen)以获取帮助。  

# 免责声明 | Disclaimer
本软件使用Apache协议，可以闭源、商业使用，请使用时携带作者信息与许可证。  
本软件按“原样”提供，不附带任何明示或暗示的担保。在任何情况下，作者均不对因使用本软件而产生的任何损害或其他责任负责，包括但不限于数据丢失、业务中断或其他商业损失。  
图标为作者构建提示词、使用gpt-plus中的DELL.E生成黑白图（并非完全黑白但肉眼难以和黑色区分），并自己修改部分图片的结构（原图中间的勾非常奇怪；原图中的箭头是朝下的；原图没有彩色）完成。使用彩虹色并**不意味**着作者**支持或反对LGBTQ+**，只能代表作者对大自然的敬畏与喜爱，对牛顿发现光谱的致敬。  
 
