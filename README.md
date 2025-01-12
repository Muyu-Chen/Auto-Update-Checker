# Auto-Update-Checker
 Auto Update software is a tool designed for automatic updates. By checking the JSON file on a remote server to compare the current version with the latest version, the software automatically downloads, extracts, and replaces files to ensure it is always up-to-date.     
 Auto Update 软件是一款用于自动更新的工具。通过检查远程服务器上的 JSON 文件来对比当前版本和最新版本号，软件实现自动下载、解压和覆盖操作，从而保持软件始终为最新版本。  


# Why choose me? Is there any benefits?  
## 为什么选择我？有什么优势？

### Why is it beneficial?  
By choosing this tool, you gain a simple yet powerful solution to ensure your applications stay updated with minimal effort. Its seamless integration, self-update capabilities, and ease of use save time, simplify maintenance, and empower developers with complete control over the update process.  

Once integrated, there’s no need to modify anything to take advantage of future updates and features. Our interface ensures forward compatibility—simply replace the `.exe` file with the latest version, and the updates will take effect automatically. For clients, all you need to do is package the new `.exe` into your update package, and your application will seamlessly update to include the latest features.  

You no longer need to worry about any details related to updates. Just update the version number in the JSON file, upload your latest build, and the entire update process is handled for you—no additional effort required.  

### 为什么选择它更有益处？  
通过选择此工具，您可以获得一个简单而强大的解决方案，以最低的成本确保您的应用程序始终保持最新。它的无缝集成、自我更新功能以及简单的使用体验，为您节省时间、简化维护，并让开发者对更新流程拥有完全掌控。  

集成一次后，您无需再修改任何内容，即可享受未来所有的更新和新功能。我们的接口支持向前兼容，只需替换新的 `.exe` 文件即可生效。而对于客户端，只需将新的 `.exe` 文件打包到更新包中，即可完成更新并获得最新功能。  

您再也无需操心任何关于更新的事情了。只需在 JSON 文件中更新版本号，上传最新的版本，即可完成整个更新过程，无需费心处理任何细节。  

### Advantages | 优势  
1. **Utilizes Built-in Windows Framework**  
   - The tool is developed based on the Windows built-in .NET Framework, eliminating the need for extra dependencies.  
   - It can be seamlessly embedded into any other Windows application.  
   - **利用 Windows 自带的 Framework**  
     - 此工具基于 Windows 自带的 .NET Framework 开发，无需额外依赖项。  
     - 可以直接嵌入到任何其他 Windows 应用程序中。  

2. **Simple and Quick**  
   - The update process is straightforward: you only need to execute a single CMD command to complete the update.  
   - This reduces the integration cost for developers and simplifies maintenance.  
   - **简单快捷**  
     - 更新流程非常简单：只需执行一条 CMD 指令即可完成更新。  
     - 降低开发者的集成成本，同时简化维护工作。  

3. **Highly Coupled with Application Version**  
   - The tool is inherently tied to the application's version, making updates logical and efficient.  
   - Developers can conveniently stop the auto-update functionality simply by removing the tool from the application.  
   - The auto-update mechanism is capable of updating all components, including itself, ensuring comprehensive version management.  
   - **与应用版本高度关联**  
     - 工具本身与应用程序的版本强相关，使得更新过程逻辑清晰且高效。  
     - 开发者只需删除此工具即可随时停止自动更新功能，方便灵活。  
     - 自动更新机制可以更新所有内容，包括工具自身，实现全面的版本管理。  

4. **Lightweight and Efficient**  
   - The tool is lightweight and does not affect the performance of the host application.  
   - Updates are executed efficiently with minimal resource consumption.  
   - **轻量高效**  
     - 工具本身非常轻量，不会对宿主程序的性能产生影响。  
     - 更新过程高效，资源消耗极低。  

5. **Highly Compatible**  
   - Compatible with all Windows applications that support CMD commands, making it versatile and reliable.  
   - **高兼容性**  
     - 支持所有可执行 CMD 指令的 Windows 应用程序，使用范围广泛且稳定可靠。  

6. **As Easy as Common Command-Line Tools**  
   - Using this tool is as quick and straightforward as executing simple commands like `mkdir xxx` in the command line.  
   - Developers can seamlessly integrate the tool into their workflows without additional complexity.  
   - **与常见命令行工具一样简单**  
     - 使用此工具的体验就像在命令行中执行 `mkdir xxx` 等简单命令一样快捷直观。  
     - 开发者可以轻松将此工具集成到工作流中，而无需额外学习成本。  

7. **No Need to Learn Development Details**  
   - You can use the release version of the tool directly without needing to understand its development language or process.  
   - **无需了解开发细节**  
     - 您可以直接使用此工具的 Release 版本，无需了解其开发语言或开发过程。  

8. **Customizable for Future Enhancements**  
   - Future versions may include popup confirmations for updates, providing greater user control and interaction.  
   - **可定制的未来升级**  
     - 未来版本可能加入弹窗确认功能，提供更高的用户控制和交互体验。  



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

 对于放在网站上的json，格式为：  
 ```
   {
    "version": "10.9.8",
    "download_url": "https://download.xxx.xxx/new_version.zip"
   }
 ```
你只需要更新版本号，所有客户端都可以在更新时检查是否有新的版本（若客户端版本更新，则不更新），并自动更新。  

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

 For the Json file on the website:  

  ```
   {
    "version": "10.9.8",
    "download_url": "https://download.xxx.xxx/new_version.zip"
   }
 ```
   
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
 
