#include <iostream>
#include <fstream>
#include <string>
#include <windows.h>

int main()
{
    if (GetFileAttributes("NovaFP.exe") != INVALID_FILE_ATTRIBUTES)
    {
        STARTUPINFO si = { 0 };
        PROCESS_INFORMATION pi = { 0 };

        si.cb = sizeof(STARTUPINFO);
        si.dwFlags = STARTF_USESHOWWINDOW;
        si.wShowWindow = SW_HIDE;

        std::string arguments = "C:\\";
        if (CreateProcess(
            "NovaFP.exe",       
            &arguments[0],   
            NULL,            
            NULL,            
            FALSE,         
            0,              
            NULL,             
            NULL,              
            &si,              
            &pi              
        ))
        {

            WaitForSingleObject(pi.hProcess, INFINITE);

            std::string tempFilePath = std::string(getenv("TEMP")) + "\\.temp";
            std::ifstream tempFile(tempFilePath);

            if (tempFile.is_open())
            {
                std::string tempPath;
                std::getline(tempFile, tempPath); // 读取路径
                tempFile.close();

                std::cout << "Selected Folder: " << tempPath << std::endl;
                DeleteFile(tempFilePath.c_str());
            }

            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
        }
        else
        {
            std::cerr << "Failed to start NovaFP.exe" << std::endl;
        }
    }
    else
    {
        std::cerr << "NovaFP.exe not found!" << std::endl;
    }

    return 0;
}
