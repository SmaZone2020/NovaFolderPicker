using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

if (File.Exists("NovaFP.exe"))
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "NovaFP.exe",
            Arguments = @"C:\",
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };
    process.Start();
    process.WaitForExit(); 

    var tempFilePath = Path.Combine(Path.GetTempPath(), ".temp");
    if (File.Exists(tempFilePath))
    {
        var _TempPath = File.ReadAllText(tempFilePath).Trim();  // 获取路径并处理
        Console.WriteLine(_TempPath);
        File.Delete(tempFilePath);  // 删除临时文件
    }
}

