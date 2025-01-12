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



namespace ConsoleApp1
{
    class Program
    {
        static async Task<int> Main(string[] args)
        // 需要关闭当前程序的返回值为1
        {
            Console.WriteLine("Hello, World!");
            string[] input = new string[args.Length];
            // 检查是否提供了参数
            if (args.Length == 0)
            {
                Console.WriteLine("There are no input argument.");
                return -1;
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    input[i] = args[i];
                    Console.WriteLine("The argument is: " + args[i]);
                }
            }

            // 启动异步操作，但不等待它完成
            var fetchTask = FetchAndPrintVersion(input[1]);
            // 在等待异步操作完成期间执行其他操作
            Console.WriteLine("Doing other work...");
            // 等待异步操作完成并获取结果
            var version = await fetchTask;
            Console.WriteLine(version["url"], version["version"]);
            string downloadUrl = version["url"];

            if (version == null)
            {
                Console.WriteLine("Failed to fetch version data.");
                return -1;
            }
            int result = CompareVersions(input[0], version["version"]);
            if (result == 1)
            {
                Console.WriteLine("The version is up to date.");
                return 0;
            }
            else if (result == 2)
            {
                Console.WriteLine("版本落后，\n即将自动更新。");
                return await updateProgram(downloadUrl);
            }
            else
            {
                Console.WriteLine("The version is the same.");
                return 0;
            }

        }

        static async Task<int> updateProgram(string downloadUrl)
        {
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
            int[] v1Parts = new int[3];
            int[] v2Parts = new int[3];

            string[] v1Strings = version1.Split('.');
            string[] v2Strings = version2.Split('.');

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("v1: " + v1Strings[i]);
                Console.WriteLine("v2: " + v2Strings[i]);
                Console.WriteLine("--------");
                int v1Part = int.Parse(v1Strings[i]);
                int v2Part = int.Parse(v2Strings[i]);

                if (v1Part > v2Part)
                {
                    Console.WriteLine("v1 > v2");
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
            Console.WriteLine($"\n程序实际所在目录（与调用无关）不关心程序集的物理位置，始终返回程序启动所在的根目录。用于找到与程序同目录的配置文件、资源文件等: {AppContext.BaseDirectory}");
            Console.WriteLine($"\n程序实际所在目录（更精确到程序集路径）,如果程序集被影子复制，它返回的是程序集的原始位置，而不是复制后的路径。如果程序集被加载到内存中运行（如某些托管环境下的动态加载），它可能返回空字符串或特殊路径: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            Console.WriteLine($"\n当前工作目录（与调用路径相关）: {Environment.CurrentDirectory}");
            return Path.GetDirectoryName(AppContext.BaseDirectory);
        }



        // 异步方法，返回一个 Task 字典，其中包含版本号和下载链接
        static async Task<Dictionary<string, string>> FetchAndPrintVersion(string urlFromWeb)
        {
            // JSON 文件的 URL
            if (string.IsNullOrEmpty(urlFromWeb))
            {
                // 硬编码 JSON URL
                urlFromWeb = "";
            }
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

                    if (jsonData != null && jsonData.RootElement.TryGetProperty("version", out var version) && jsonData.RootElement.TryGetProperty("download_url", out var url))
                    {
                        Console.WriteLine($"Version: {version.GetString()}");
                        var result = new Dictionary<string, string>
                        {
                            { "version", version.GetString() },
                            { "url", url.GetString() }
                        };
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Key 'version' not found in the JSON.");
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

    }
}
