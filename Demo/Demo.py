import os
import subprocess

if os.path.exists("NovaFP.exe"):
    process = subprocess.Popen(
        ["NovaFP.exe", "C:\\"],
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE
    )

    process.wait()

    temp_file_path = os.path.join(os.getenv('TEMP'), '.temp')

    if os.path.exists(temp_file_path):
        with open(temp_file_path, 'r') as temp_file:
            temp_path = temp_file.read().strip()
            print("Selected Folder:", temp_path)

        os.remove(temp_file_path)
else:
    print("NovaFP.exe not found!")
