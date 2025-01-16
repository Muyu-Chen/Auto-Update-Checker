using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Windows;
using System.Diagnostics;
using System.Globalization;

namespace autoupdate
{
    class Program
    {
        static async Task<int> Main(string[] args)
        // 需要关闭当前程序的返回值为1
        {
            Console.OutputEncoding = Encoding.UTF8;
            string defaultURL = "input your defualt url here";

            Console.WriteLine("program start...");

            string[] input = ParseArguments(args, defaultURL);
            // 顺序：url, version, showConfirm
            if (input[0].Equals("helpModel"))
            {
                // 如果是帮助模式，直接返回
                ShowHelp();
                return 0;
            }
            if (input[0] == null || input[1] == null || input[2] == null)
            {
                // 如果缺少url或version参数，返回错误
                // 若缺少showConfirm参数，使用默认值
                return -1;
            }

            // 启动异步操作，但不等待它完成
            var fetchTask = FetchAndPrintVersion(input[0]);
            // 在等待异步操作完成期间执行其他操作
            Console.WriteLine("Doing other work...");
            // 等待异步操作完成并获取结果
            var jsonContent = await fetchTask;
            if(jsonContent == null)
            {
                Console.WriteLine("jsonContent is null, return");
                return -1;
            }
            Console.WriteLine(jsonContent["url"], jsonContent["version"]);
            //string downloadUrl = version["url"];

            if (jsonContent == null)
            {
                Console.WriteLine("Failed to fetch version data.");
                return -1;
            }
            int result = CompareVersions(input[1], jsonContent["version"]);
            if (result == 1)
            {
                Console.WriteLine("The version is newer than the current version.");
                return 0;
            }
            else if (result == 2)
            {
                Console.WriteLine("The version is older, updating...");
                try
                {
                    Console.WriteLine("Checking for updates...");
                    int res = await updateProgram(jsonContent["url"], jsonContent["update_notes"],jsonContent["showConfirm"]);
                    if (res == 0)
                    {
                        // 用户拒绝更新或无新版本
                        return 0;
                    }
                    else if (res == 1)
                    {
                        // 用户同意更新且下载成功，主程序请立刻关闭程序
                        return 1;
                    }
                    else
                    {
                        // 用户同意更新但更新失败
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Failed to update the program.");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("The version is the same.");
                return 0;
            }

        }

        static async Task<int> updateProgram(string downloadUrl, string updateNotes, string showConfirm = "true")
        {
            // 0: 用户拒绝更新
            // 1: 用户同意更新且更新成功
            // -1: 用户同意更新但更新失败
            if (showConfirm == "false")
            {
                Console.WriteLine("The publisher has set to update without asking the user.");
            }
            else
            {
                if (AskUserToUpdate(updateNotes))
                {
                    Console.WriteLine("User confirmed the update.");
                }
                else
                {
                    Console.WriteLine("User declined the update.");
                    return 0;
                }
            }
            // 获取程序所在目录
            string location = findLocation();
            Console.WriteLine($"The location of the program is: {location}");

            // 下载文件
            string savePath = location + "\\downloaded_file.zip";      // 保存路径
            bool success = await DownloadZipFileAsync(downloadUrl, savePath);
            if (success)
            {
                Console.WriteLine($"File downloaded successfully: {savePath}");
            }
            else
            {
                Console.WriteLine("File download failed.");
                return -1;
            }



            // ZIP 文件路径
            string zipFilePath = savePath; // ZIP 文件路径
            string extractPath = location + "\\extracted_files\\";    // 解压目标路径
            string batFilePath = location + "\\update_script.bat";  // 生成的 BAT 脚本路径
            string applicationPath = location;         // 程序目标路径
                                                       // 调用解压 ZIP 文件的函数
            if (ExtractZipFile(zipFilePath, extractPath))
            {
                Console.WriteLine("ZIP file extracted successfully.");

                // 调用创建 BAT 脚本的函数
                CreateBatScript(batFilePath, extractPath, applicationPath);
                Console.WriteLine($"BAT script generated: {batFilePath}");
                // 启动 BAT 脚本
                System.Diagnostics.Process.Start(batFilePath);
                // 需要关闭程序的返回值为1
                return 1;
            }
            else
            {
                Console.WriteLine("Failed to extract the ZIP file.");
            }
            return -1;
        }

        static int CompareVersions(string version1, string version2)
        {
            Console.WriteLine("version in user's pc: " + version1);
            Console.WriteLine("version from the web: " + version2);
            int[] v1Parts = new int[4];
            int[] v2Parts = new int[4];

            string[] v1Strings = version1.Split('.');
            string[] v2Strings = version2.Split('.');

            for (int i = 0; i < 4; i++)
            {
                int v1Part = (i < v1Strings.Length ? int.Parse(v1Strings[i]) : 0);
                int v2Part = (i < v2Strings.Length ? int.Parse(v2Strings[i]) : 0);

                //Console.WriteLine("v1: " + v1Part);
                //Console.WriteLine("v2: " + v2Part);
                //Console.WriteLine("--------");

                if (v1Part > v2Part)
                {
                    //Console.WriteLine("v1 > v2");
                    return 1; //version1 大 返回1
                }
                if (v1Part < v2Part)
                {
                    Console.WriteLine("v1 < v2");
                    return 2; //version2 大 返回2
                }
            }

            return 0;
        }

        static string findLocation()
        {
            //Console.WriteLine($"\n程序实际所在目录（与调用无关）不关心程序集的物理位置，始终返回程序启动所在的根目录。用于找到与程序同目录的配置文件、资源文件等: {AppContext.BaseDirectory}");
            //Console.WriteLine($"\n程序实际所在目录（更精确到程序集路径）,如果程序集被影子复制，它返回的是程序集的原始位置，而不是复制后的路径。如果程序集被加载到内存中运行（如某些托管环境下的动态加载），它可能返回空字符串或特殊路径: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            //Console.WriteLine($"\n当前工作目录（与调用路径相关）: {Environment.CurrentDirectory}");
            return Path.GetDirectoryName(AppContext.BaseDirectory);
        }



        // 异步方法，返回一个 Task 字典，其中包含版本号和下载链接
        static async Task<Dictionary<string, string>> FetchAndPrintVersion(string urlFromWeb)
        {
            try
            {
                // 创建 HttpClient 实例
                using (HttpClient client = new HttpClient())
                {
                    // 从 URL 获取 JSON 内容
                    Console.WriteLine("Fetching JSON data...");
                    string jsonString = await client.GetStringAsync(urlFromWeb);

                    // 解析 JSON 并读取 "version" 键
                    var jsonData = JsonSerializer.Deserialize<JsonDocument>(jsonString);

                    if (jsonData != null)
                    {
                        bool versionFound = jsonData.RootElement.TryGetProperty("version", out var version);
                        bool urlFound = jsonData.RootElement.TryGetProperty("download_url", out var url);
                        bool showConfirmFound = jsonData.RootElement.TryGetProperty("showConfirm", out var showConfirm);
                        bool updateNotesFound = jsonData.RootElement.TryGetProperty("update_notes", out var updateNotes);

                        if(versionFound == false || urlFound == false)
                        {
                            Console.WriteLine("JSON data is invalid.");
                            return null;
                        }
                        // 设置返回值
                        string versionValue = versionFound ? version.GetString() : "default_value";
                        string urlValue = urlFound ? url.GetString() : "default_value";
                        string showConfirmValue = showConfirmFound ? showConfirm.GetString() : "true";
                        string updateNotesValue = updateNotesFound ? updateNotes.GetString() : "No update notes available";

                        Console.WriteLine($"Version: {versionValue}");
                        Console.WriteLine($"Download URL: {urlValue}");
                        Console.WriteLine($"showConfirm: {showConfirmValue}");
                        Console.WriteLine($"Update Notes: {updateNotesValue}");

                        var result = new Dictionary<string, string>
                        {
                            { "version", versionValue },
                            { "url", urlValue },
                            { "showConfirm", showConfirmValue.ToString() },
                            { "update_notes", updateNotesValue }
                        };

                        return result;
                    }
                    else
                    {
                        Console.WriteLine("JSON data is null.");
                        return null;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching JSON data: {e.Message}");
                return null;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error parsing JSON: {e.Message}");
                return null;
            }
        }

        static async Task<bool> DownloadZipFileAsync(string url, string savePath)
        {
            try
            {
                // 创建 HttpClient 实例
                using (HttpClient client = new HttpClient())
                {
                    Console.WriteLine($"Downloading ZIP file from: {url}");

                    // 下载文件内容为字节数组
                    byte[] fileBytes = await client.GetByteArrayAsync(url);

                    // 将字节数组写入本地文件
                    await Task.Run(() => File.WriteAllBytes(savePath, fileBytes));

                    Console.WriteLine($"ZIP file downloaded and saved as: {savePath}");
                    return true; // 下载成功
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error downloading the ZIP file: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error saving the ZIP file: {e.Message}");
            }

            return false; // 下载失败
        }

        static bool ExtractZipFile(string zipPath, string extractPath)
        {
            try
            {
                // 如果解压路径存在，先删除
                if (Directory.Exists(extractPath))
                {
                    Directory.Delete(extractPath, true);
                }
                else if (File.Exists(extractPath))
                {
                    Console.WriteLine("A file exists with the same name as the target directory, attempting to delete...");
                    File.Delete(extractPath);
                    Console.WriteLine("File deleted successfully.");
                }


                // 解压 ZIP 文件到指定文件夹
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                return true; // 解压成功
            }
            catch (Exception e)
            {
                Console.WriteLine(extractPath);
                Console.WriteLine($"Error extracting ZIP file: {e.Message}");
                return false; // 解压失败
            }
        }


        static void CreateBatScript(string batFilePath, string extractPath, string applicationPath)
        {
            try
            {
                // 创建 BAT 脚本内容
                string batContent = $@"
            @echo off
            chcp 65001 >nul
            echo Update process started...

            REM Wait for 5 seconds
            echo 5s: Updating the program...
            timeout /t 1 /nobreak >nul
            echo 4s: Updating the program...
            timeout /t 1 /nobreak >nul
            echo 3s: Updating the program...
            timeout /t 1 /nobreak >nul
            echo 2s: Updating the program...
            timeout /t 1 /nobreak >nul
            echo 1s: Updating the program...
            timeout /t 1 /nobreak >nul

            REM 替代新文件
            set extractPath=""{extractPath}""
            set applicationPath=""{applicationPath}""

            xcopy ""%extractPath%\*"" ""%applicationPath%"" /E /Y /R /C /I

            REM 重启程序，可选
            REM start """" ""%applicationPath%\GUIClient.exe""

            REM End of script
            ";

                // 将内容写入 BAT 文件
                //File.WriteAllText(batFilePath, batContent, Encoding.UTF8);
                //旧的 BAT 文件写入方式，可能会导致乱码
                using (StreamWriter writer = new StreamWriter(batFilePath, false, new UTF8Encoding(false)))
                {
                    writer.Write(batContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating BAT script: {e.Message}");
            }
        }

        // 新增函数：显示更新确认对话框


        static bool AskUserToUpdate(string pdateNotes)
        {
            string title = "Update Available";
            string message = $"A new version is available. Do you want to update?\n\nUpdate Notes:\n{pdateNotes}";

            // 显示消息框，询问用户是否确认更新
            MessageBoxResult result = MessageBox.Show(
                message, // 消息内容
                title,   // 窗口标题
                MessageBoxButton.YesNo, // 按钮选项
                MessageBoxImage.Question // 图标样式
            );

            // 返回用户是否选择了 "Yes"
            return result == MessageBoxResult.Yes;
        }


        static string[] ParseArguments(string[] argv, string defaultURL = null)
        {
            if (argv.Length == 0)
            {
                Console.WriteLine("No arguments provided. Please provide the required arguments.");
                return new string[] { "helpModel", null, null };
            }

            Dictionary<string, string> argsDict = new Dictionary<string, string>();
            for (int i = 0; i < argv.Length; i++)
            {
                if (argv[i].StartsWith("-") || argv[i].StartsWith("/"))
                {
                    string key = argv[i].Substring(1);
                    string value = null;

                    if (i + 1 < argv.Length && !argv[i + 1].StartsWith("-") && !argv[i + 1].StartsWith("/") && !string.IsNullOrWhiteSpace(argv[i + 1]))
                    {
                        value = argv[i + 1];
                        i++; // Skip the next argument as it is the value
                    }

                    argsDict[key] = value ?? "true"; // 没有显式值时，默认为 "true"（布尔开关参数）
                }
            }

            if (argsDict.ContainsKey("help"))
            {
                return new string[] { "helpModel", null, null };
            }

            // 设置默认值或获取用户提供的值
            string url = argsDict.ContainsKey("url") ? argsDict["url"] : defaultURL;
            string version = argsDict.ContainsKey("version") ? argsDict["version"] : null;
            string showConfirm = argsDict.ContainsKey("showConfirm") && argsDict["showConfirm"]?.ToLower() == "false" ? "false" : "true";


            if (!argsDict.ContainsKey("version"))
            {
                Console.WriteLine("No version argument provided. Please provide the version of the program.");
            }

            if (url.Equals("input your defualt url here"))
            {
                Console.WriteLine("Attention, please input your default url in the code or provide the url argument.");
                return new string[] { "helpModel", null, null };
            }


            return new string[] { url, version, showConfirm };
        }


        static void ShowHelp()
        {
            string programName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            string programNameWithoutExtension = Path.GetFileNameWithoutExtension(programName);
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (currentCulture == "zh-CN")
            {
                Console.WriteLine($"用法: {programNameWithoutExtension} [选项]");
                Console.WriteLine("选项:");
                Console.WriteLine("  -url <url>          指定用于获取版本数据的URL（可选，默认使用硬编码的url）；");
                Console.WriteLine("  -version <version>  指定程序的当前版本；");
                Console.WriteLine("  -showConfirm        在更新前显示确认对话框（可选，默认显示）；");
                Console.WriteLine("  -help               显示此帮助消息，若同时与前文使用，会被忽略。");
            }
            else
            {
                Console.WriteLine($"Usage: {programNameWithoutExtension} [options]");
                Console.WriteLine("Options:");
                Console.WriteLine("  -url <url>          Specify the URL to fetch version data.");
                Console.WriteLine("  -version <version>  Specify the current version of the program.");
                Console.WriteLine("  -showConfirm        Show confirmation dialog before updating(optional).");
                Console.WriteLine("  -help               Show this help message.");
            }
        }
    }
}
