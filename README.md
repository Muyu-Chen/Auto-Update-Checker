# Auto-Update-Checker
The **Auto-Update Checker** is a **lightweight, small, and portable** tool designed for **automatic updates**. By checking the JSON file on a remote server to compare the current version with the latest version, the software automatically downloads, extracts, and replaces files to ensure it is always up-to-date. The tool includes features such as update pop-ups, update instructions, and more.  
**Auto-Update Checker** 是一款用于自动更新的**轻量、小巧、便携**、**实用**且**易用的更新工具**。通过检查远程服务器上的 JSON 文件来对比当前版本和最新版本号，软件实现自动下载、解压和覆盖操作，从而保持软件始终为最新版本。工具还包括更新弹窗、更新说明等功能。  

# TL;DR Version | 太长不看版  
在服务器上新建一个 JSON 文件，并确保外网可以访问，内容如下：  
  
Create a JSON file on the server and ensure it is accessible from the internet. The content should be as follows:  

```json
   {
    "version": "10.9.8.7",
    "download_url": "https://download.xxx/new_version.zip",
    "showConfirm": "true",
    "update_notes": "your update notes",
   }
```

## What you should change | 你需要修改的部分  
version: 最新版本的版本号。  
The latest version number.  
download_url: 更新包的下载地址。  
The URL to download the update package.  
update_notes: 更新日志。
The update log/notes.
## Executing the Update Check | 执行更新检查
你可以使用 GPT 询问如何在特定编程语言中执行 CMD 命令，并获取返回值。  
You may ask GPT how to execute a CMD command in your preferred programming language and retrieve its return value.  
执行以下命令以检查更新：  
Run the following command to check for updates:  
`Auto-Update-Checker.exe -url website.com/yourJsonUrl.json -version "1.1.1"`
yourJsonUrl.json: 需要替换为你的 JSON 文件的实际地址。  
Replace with the actual URL of your JSON file.  
"1.1.1": 需要替换为当前程序的版本号。  
Replace with your application's current version number.  

## Return Value | 返回值
返回值 = 1（Update available）
The return value is 1 (Update available).
→ 立刻关闭程序，否则更新时可能发生文件冲突，导致更新失败。
→ Immediately close the application to avoid file conflicts and update failures.

返回值 ≠ 1（No update needed）
If the return value is not 1 (No update needed).
→ 无需更新，程序可正常运行。
→ No update required, continue running normally.



# Why choose me? Is there any benefits?  
## 为什么选择我？有什么优势？

### Why is it beneficial?  
By choosing this tool, you gain a simple yet powerful solution to ensure your applications stay updated with minimal effort. Its seamless integration, self-update capabilities, and ease of use save time, simplify maintenance, and empower developers with complete control over the update process.  

Once integrated, there’s no need to modify anything to take advantage of future updates and features. Our interface ensures forward compatibility—simply replace the `.exe` file with the latest version, and the updates will take effect automatically. For clients, all you need to do is package the new `.exe` into your update package, and your application will seamlessly update to include the latest features.  

You no longer need to worry about any details related to updates. Just update the version number in the JSON file, upload your latest build, and the entire update process is handled for you—no additional effort required.  

### 为什么选择它更好？  
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
 此程序接受以下命令行参数：

 #### 中文
 ```plaintext
 用法: Auto-Update-Checker.exe [选项]
 选项:
   -url <url>          指定用于获取版本数据的URL（可选，默认使用硬编码的url；若你使用release版本，请一定带参数）；
   -version <version>  指定程序的当前版本；
   -showConfirm        在更新前显示确认对话框（可选，默认显示，优先级低于json中的设置）；
   -help               显示此帮助消息，优先级最高。
 ```

 #### English
 ```plaintext
 Usage: Auto-Update-Checker.exe [options]
 Options:
   -url <url>          Specify the URL to fetch version data.
   -version <version>  Specify the current version of the program.
   -showConfirm        Show confirmation dialog before updating (optional, default is true) (json online command > here > defualt).
   -help               Show this help message. If used with other options, other command will be ignored.
 ```

 在你需要更新的应用程序中，使用系统的调用命令调用此exe，例如在C语言中：  
 ```c
 // 定义当前版本号和更新的URL。
 const char* version = "3.2.1"; // 当前程序的版本号（建议硬编码）。
 const char* url = "http://xxx/xxx.json"; // 更新地址。

 // 构造命令。
 char command[256];
 snprintf(command, sizeof(command), "path\\to\\dic\\Auto-Update-Checker.exe -version %s -url %s", version, url);

 // 执行命令并处理结果。
 int result = system(command);

 if (result == 1) {
    // 新版本已下载。
    printf("正在更新...\n");
    // 通知用户关闭主程序。
    printf("请在5秒内关闭主程序以避免更新失败。\n");
    return 0; // 退出程序以允许更新继续。
 } else if (result == 0) {
    // 当前版本已是最新或用户拒绝更新。
    printf("未执行更新操作。\n");
 } else if (result == -1) {
    // 用户同意更新但更新失败。
    printf("更新失败。\n");
 } else {
    // 处理其他意外结果。
    printf("检查更新时发生未知错误。\n");
 }
 ```
例如在Python中：
 ```python
 try:
    exe_path = "Auto-Update-Checker.exe"
    version = "2.3.1"
    result = subprocess.Popen(

        [
            exe_path,
            "-version",
            version,
            "-url",
            "http://xxx/version.json",
        ],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        text=True,
        encoding="utf-8",
    )
    stdout, stderr = result.communicate()
    print("return code:", result.returncode)
    print("std out:", stdout) # optional
    print("err out:", stderr) # optional
    if result.returncode == 1:
        print("Exiting due to user choosing to update")
        exit(0)
    else:
        print(f"return value is {result.returncode}, don't need to update")
 except Exception as e:
    print(f"error when updating: {e}")
 ```

 其中仅支持不高于4位的版本号，即，如果本地版为4.3.2.1，在线获取的最新版为4.3.3，也可以更新。无需补位。版本号从左往右获取并比较大小。   
 
 返回值为1：新版本下载完成，5s后更新，请在5s内退出所有占用；    
 返回值为0：版本是最新版。   

 对于放在网站上的json，格式为：  
 ```json
   {
    "version": "10.9.8.7",
    "download_url": "https://download.xxx/new_version.zip",
    "showConfirm": "true",
    "update_notes": "your update notes",
    "md5": "xxxxxxxxxx",
    "restart": "true",
    "newName": "new exe name you want to restart"

   }
 ```
 "showConfirm": "true"的意思是是否提示用户需要更新，若为"false"则不提示，直接更新。此设置高于--showConfirm中的设置。
 "md5"可选，若没有就不对完整性进行验证。
 你只需要更新版本号，所有客户端都可以在更新时检查是否有新的版本（若客户端版本更新，则不更新），并自动更新。  
 "restart"可选，若不需要，建议填写为“false”而不是直接删除以免兼容问题。
 "newName"是更新后打开的文件名，可以为相对路径，以 xxxFold\\exeName.exe 形式完成。开头和结尾不要有斜杠。

 ## How to use?
 In your application that requires updates, use a system call to invoke this `.exe` file. For example, in C language:  

 ```c
 // Define the current version and update URL.
 const char* version = "3.2.1"; // Current program version (hardcoded).
 const char* url = "http://xxx/xxx.json"; // Update URL.

 // Construct the command.
 char command[256];
 snprintf(command, sizeof(command), "path\\to\\dic\\Auto-Update-Checker.exe -version %s -url %s", version, url);

 // Execute the command and handle the result.
 int result = system(command);

 if (result == 1) {
    // A new version has been downloaded.
    printf("Updating...\n");
    // Notify the user to close the main program.
    printf("Please close the main program within 5 seconds to avoid update failure.\n");
    return 0; // Exit the program to allow the update to proceed.
 } else if (result == 0) {
    // The current version is up-to-date or the user refused to update.
    printf("No update performed.\n");
 } else if (result == -1) {
    // The user agreed to update but the update failed.
    printf("Update failed.\n");
 } else {
    // Handle other unexpected results.
    printf("An unknown error occurred while checking for updates.\n");
 }
 ```
 Example in Python:
 ```python
 try:
    exe_path = "Auto-Update-Checker.exe"
    version = "2.3.1"
    result = subprocess.Popen(

        [
            exe_path,
            "-version",
            version,
            "-url",
            "http://xxx/version.json",
        ],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        text=True,
        encoding="utf-8",
    )
    stdout, stderr = result.communicate()
    print("return code:", result.returncode)
    print("std out:", stdout) # optional
    print("err out:", stderr) # optional
    if result.returncode == 1:
        print("Exiting due to user choosing to update")
        exit(0)
    else:
        print(f"return value is {result.returncode}, don't need to update")
 except Exception as e:
    print(f"error when updating: {e}")
 ```
 Implemented version number comparison that supports updating to versions with less than four segments. If the local version is 4.3.2.1 and the online latest version is 4.3.3, an update is still possible. Version numbers are fetched and compared from left to right without padding.

 Return Values:
 1: A new version has been downloaded. The update will start after 5 seconds. Please close all processes within 5 seconds.  
 0: The version is already up-to-date.    

 For the Json file on the website:  

  ```json
   {
    "version": "10.9.8.7",
    "download_url": "https://download.xxx/new_version.zip",
    "showConfirm": "true",
    "update_notes": "your update notes",
    "md5": "xxxxxxx",
    "restart": "true",
    "newName": "new exe name you want to restart"
   }
 ```
 md5 is optional.  

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

# Todo  
 - [x] 添加参数：重新启动程序（写入bat中）
 - [x] 弹窗弹出更新说明
 - [x] 增加完整性验证
 - [ ] 优化界面  

# 免责声明 | Disclaimer
本软件使用Apache协议，可以闭源、商业使用，请使用时携带作者信息与许可证。  
本软件按“原样”提供，不附带任何明示或暗示的担保。在任何情况下，作者均不对因使用本软件而产生的任何损害或其他责任负责，包括但不限于数据丢失、业务中断或其他商业损失。  
图标为作者构建提示词、使用gpt-plus中的DELL.E生成黑白图（并非完全黑白但肉眼难以和黑色区分），并自己修改部分图片的结构（原图中间的勾非常奇怪；原图中的箭头是朝下的；原图没有彩色）完成。使用彩虹色并**不意味**着作者**支持或反对LGBTQ+**，只能代表作者对大自然的敬畏与喜爱，对牛顿发现光谱的致敬。  
 
