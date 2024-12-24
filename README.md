## NovaFolderPicker

### 简介

`NovaFolderPicker` 是一个美观且易于使用的文件夹选择工具，采用 FluentUI 设计风格进行重构。它不仅提供了标准的文件夹选择功能，还在外观上进行了优化，使其更加符合现代应用程序的界面标准。通过集成这个文件夹选择器，您可以让您的程序具有更好的用户体验。

### 特性

- **现代化界面**：使用 FluentUI 重构，使界面更加美观，符合现代 UI 设计。
- **简便的文件夹选择**：用户可以轻松选择本地文件夹，并且支持文件夹路径的复制和保存。
- **支持外部程序调用**：通过二进制文件 `NovaFP.exe`，可以方便地与其他程序进行集成，获取用户选择的文件夹路径。

### 如何使用

1. **下载并集成 NovaFP.exe**

   下载 `NovaFP.exe`，它是该 FolderPicker 的二进制可执行程序。您可以通过以下代码启动 `NovaFP.exe`，让它显示文件夹选择器：


2. **解释代码流程**

   - **`NovaFP.exe` 启动**：该程序通过 `Process` 启动 `NovaFP.exe`，并传入默认路径 `C:\` 作为初始显示的文件夹路径。
   - **等待退出**：使用 `WaitForExit` 方法来确保文件夹选择完成。
   - **读取临时文件**：在用户选择完文件夹后，`NovaFP.exe` 会将选择的路径保存到一个临时文件中（`.temp`）。程序会读取该文件并获取路径。
   - **更新配置**：路径读取后，程序将更新 UI 组件并保存该路径到配置文件中，确保路径能够在后续使用。

   - **查看Demo代码**：
	- [Demo.cpp](Demo/Demo.cpp)（C++）
	- [Demo.cs](Demo/Demo.cs)（C#）
	- [Demo.vb](Demo/Demo.vb)（VisualBasic）
	- [Demo.py](Demo/Demo.py)（Python）
	- [Demo.js](Demo/Demo.js)（Node.JS）
3. **FluentUI 文件夹选择器界面**

   - `NovaFP.exe` 提供的文件夹选择器界面采用 FluentUI 设计，保证了现代和美观的用户体验。
   - 界面支持快速导航、路径显示、文件夹选择等功能，符合大多数桌面应用的交互设计要求。

### 下载链接

您可以下载 `NovaFP.exe`，并将其与您的项目一起使用：

[下载 NovaFP.exe](https://github.com/SmaZone2020/NovaFolderPicker/releases/download/o.o/NovaFP.exe)

### 集成说明

1. **准备 `NovaFP.exe` 文件**：确保 `NovaFP.exe` 与调用代码在同一目录下，或者在代码中指定完整路径。
2. **运行时依赖**：确保您的程序有足够的权限运行外部程序，特别是在 Windows 环境中可能需要管理员权限。
3. **错误处理**：在实际集成时，确保处理程序异常，例如 `NovaFP.exe` 文件不存在或无法启动时，能够适当处理这些错误。

### 总结

通过集成 `NovaFP.exe`，您可以轻松地在您的应用程序中实现一个现代化的文件夹选择器，借助 FluentUI 提供的美观界面，提升用户体验。通过上述代码，您不仅能够启动文件夹选择器，还能在文件夹选择完成后获取用户选择的路径，并将其保存到配置文件中。
