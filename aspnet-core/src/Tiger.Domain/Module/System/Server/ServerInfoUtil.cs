using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Tiger.Module.System.Server
{

    /// <summary>
    /// 服务器信息帮助类
    /// </summary>
    public static class ServerInfoUtil
    {
        /// <summary>
        /// 内存信息
        /// </summary>
        /// <returns></returns>
        public static MemoryMetrics GetComputerInfo()
        {
            MemoryMetricsClient client = new();
            MemoryMetrics memoryMetrics = IsUnix() ? client.GetUnixMetrics() : client.GetWindowsMetrics();

            memoryMetrics.FreeRam = Math.Round(memoryMetrics.Free / 1024, 2) + "GB";
            memoryMetrics.UsedRam = Math.Round(memoryMetrics.Used / 1024, 2) + "GB";
            memoryMetrics.TotalRam = Math.Round(memoryMetrics.Total / 1024, 2) + "GB";
            memoryMetrics.RamRate = Math.Ceiling(100 * memoryMetrics.Used / memoryMetrics.Total).ToString() + "%";
            memoryMetrics.CpuRate = Math.Ceiling(Convert.ToDouble(GetCPURate())) + "%";
            return memoryMetrics;
        }

        /// <summary>
        /// 磁盘信息
        /// </summary>
        /// <returns></returns>
        public static List<DiskInfo> GetDiskInfos()
        {
            List<DiskInfo> diskInfos = new();

            if (IsUnix())
            {
                string output = ShellUtil.Bash("df -m / | awk '{print $2,$3,$4,$5,$6}'");
                var arr = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 0) return diskInfos;

                var rootDisk = arr[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (rootDisk == null || rootDisk.Length == 0)
                    return diskInfos;

                DiskInfo diskInfo = new()
                {
                    DiskName = "/",
                    TotalSize = long.Parse(rootDisk[0]) / 1024,
                    Used = long.Parse(rootDisk[1]) / 1024,
                    AvailableFreeSpace = long.Parse(rootDisk[2]) / 1024,
                    AvailablePercent = decimal.Parse(rootDisk[3].Replace("%", ""))
                };
                diskInfos.Add(diskInfo);
            }
            else
            {
                var driv = DriveInfo.GetDrives().Where(u => u.IsReady);
                foreach (var item in driv)
                {
                    if (item.DriveType == DriveType.CDRom) continue;
                    var obj = new DiskInfo()
                    {
                        DiskName = item.Name,
                        TypeName = item.DriveType.ToString(),
                        TotalSize = item.TotalSize / 1024 / 1024 / 1024,
                        AvailableFreeSpace = item.AvailableFreeSpace / 1024 / 1024 / 1024,
                    };
                    obj.Used = obj.TotalSize - obj.AvailableFreeSpace;
                    obj.AvailablePercent = decimal.Ceiling(obj.Used / (decimal)obj.TotalSize * 100);
                    diskInfos.Add(obj);
                }
            }
            return diskInfos;
        }

        /// <summary>
        /// 获取外网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpFromOnline()
        {
            var url = "http://myip.ipip.net";
            byte[] array = Encoding.ASCII.GetBytes(url);
            // TODO:请求外网接口获取外网的ip
            MemoryStream stream = new MemoryStream(array);
            var streamReader = new StreamReader(stream);
            var html = streamReader.ReadToEnd();
            return html.Replace("当前 IP：", "").Replace("来自于：", "");
        }

        public static bool IsUnix()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static string GetCPURate()
        {
            string cpuRate;
            if (IsUnix())
            {
                string output = ShellUtil.Bash("top -b -n1 | grep \"Cpu(s)\" | awk '{print $2 + $4}'");
                cpuRate = output.Trim();
            }
            else
            {
                string output = ShellUtil.Cmd("wmic", "cpu get LoadPercentage");
                cpuRate = output.Replace("LoadPercentage", string.Empty).Trim();
            }
            return cpuRate;
        }

        /// <summary>
        /// 获取系统运行时间
        /// </summary>
        /// <returns></returns>
        public static string GetRunTime()
        {
            string runTime = string.Empty;
            if (IsUnix())
            {
                string output = ShellUtil.Bash("uptime -s").Trim();
                runTime = ((DateTime.Now - Convert.ToDateTime(output)).TotalMilliseconds.ToString().Split('.')[0]);
            }
            else
            {
                string output = ShellUtil.Cmd("wmic", "OS get LastBootUpTime/Value");
                string[] outputArr = output.Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                if (outputArr.Length == 2)
                    runTime = (DateTime.Now - Convert.ToDateTime(outputArr[1].Split('.')[0])).TotalMilliseconds.ToString().Split('.')[0];
            }
            return runTime;
        }
    }

    

    

    public class MemoryMetricsClient
    {
        /// <summary>
        /// windows系统获取内存信息
        /// </summary>
        /// <returns></returns>
        public MemoryMetrics GetWindowsMetrics()
        {
            string output = ShellUtil.Cmd("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value");
            var metrics = new MemoryMetrics();
            var lines = output.Trim().Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length <= 0) return metrics;

            var freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);

            metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);//m
            metrics.Used = metrics.Total - metrics.Free;

            return metrics;
        }

        /// <summary>
        /// Unix系统获取
        /// </summary>
        /// <returns></returns>
        public MemoryMetrics GetUnixMetrics()
        {
            string output = ShellUtil.Bash("free -m | awk '{print $2,$3,$4,$5,$6}'");
            var metrics = new MemoryMetrics();
            var lines = output.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length <= 0) return metrics;

            if (lines != null && lines.Length > 0)
            {
                var memory = lines[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (memory.Length >= 3)
                {
                    metrics.Total = double.Parse(memory[0]);
                    metrics.Used = double.Parse(memory[1]);
                    metrics.Free = double.Parse(memory[2]);//m
                }
            }
            return metrics;
        }
    }

    public class ShellUtil
    {
        /// <summary>
        /// linux 系统命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string Bash(string command)
        {
            var escapedArgs = command.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();
            return result;
        }

        /// <summary>
        /// windows系统命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Cmd(string fileName, string args)
        {
            string output = string.Empty;

            var info = new ProcessStartInfo();
            info.FileName = fileName;
            info.Arguments = args;
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }
            return output;
        }



    }

}
