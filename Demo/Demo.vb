Imports System.IO
Imports System.Diagnostics

Module Program
    Sub Main()
        If File.Exists("NovaFP.exe") Then
            Try
                Dim process As New Process()
                process.StartInfo.FileName = "NovaFP.exe"
                process.StartInfo.Arguments = "C:\"
                process.StartInfo.UseShellExecute = False
                process.StartInfo.CreateNoWindow = True
                process.Start()

                process.WaitForExit()

                Dim tempFilePath As String = Path.Combine(Path.GetTempPath(), ".temp")

                If File.Exists(tempFilePath) Then
                    Dim tempPath As String = File.ReadAllText(tempFilePath).Trim()
                    Console.WriteLine("Selected Folder: " & tempPath)

                    File.Delete(tempFilePath)
                End If
            Catch ex As Exception
                Console.WriteLine("Error starting NovaFP.exe: " & ex.Message)
            End Try
        Else
            Console.WriteLine("NovaFP.exe not found!")
        End If
    End Sub
End Module
