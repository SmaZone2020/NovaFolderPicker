using MessageBox = iNKORE.UI.WPF.Modern.Controls.MessageBox;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using iNKORE.UI.WPF.Modern;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO.Pipes;


namespace NovaFolderPicker
{
    public partial class MainWindow : Window
    {
        private string currentPath;
        public string selectPath;
        public MainWindow()
        {
            InitializeComponent();
            SetApplicationTheme();
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && Directory.Exists(args[1]))
            {
                currentPath = args[1];
            }
            else
            {
                currentPath = "/Home";
            }
            UpdateFolderView();
        }

        private void SetApplicationTheme()
        {
            var isLightTheme = IsLightTheme();
            ThemeManager.Current.ApplicationTheme = isLightTheme ? ApplicationTheme.Light : ApplicationTheme.Dark;
        }

        private bool IsLightTheme()
        {
            const string registryKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string registryValue = "AppsUseLightTheme";

            using (var key = Registry.CurrentUser.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    var registryValueObject = key.GetValue(registryValue);
                    if (registryValueObject != null)
                    {
                        return (int)registryValueObject == 1;
                    }
                }
            }
            return false; // 默认使用暗色主题
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPath != "/Home")
            {
                var parentPath = Directory.GetParent(currentPath)?.FullName;
                currentPath = parentPath ?? "/Home";
            }
            else
            {
                currentPath = "/Home";
            }

            UpdateFolderView();
        }

        private void UpdateFolderView()
        {
            TextBoxPath.Text = currentPath;

            ListViewFolder.Items.Clear();

            if (currentPath == "/Home")
            {
                // 展示系统特定文件夹
                ListViewFolder.Items.Add(new ListViewItem { Content = "Documents" });
                ListViewFolder.Items.Add(new ListViewItem { Content = "Pictures" });
                ListViewFolder.Items.Add(new ListViewItem { Content = "Videos" });
                ListViewFolder.Items.Add(new ListViewItem { Content = "Downloads" });
                ListViewFolder.Items.Add(new ListViewItem { Content = "Music" });
                ListViewFolder.Items.Add(new ListViewItem { Content = "Desktop" });
                ListViewFolder.Items.Add(new Separator());
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        var driveItem = new ListViewItem { Content = $"{drive.Name}", ToolTip = $"{drive.TotalFreeSpace / (1024 * 1024 * 1024)}GB / {drive.TotalSize / (1024 * 1024 * 1024)}GB" };
                        ListViewFolder.Items.Add(driveItem);
                    }
                }
            }
            else
            {
                var directories = Directory.GetDirectories(currentPath);
                foreach (var dir in directories)
                {
                    var directoryItem = new ListViewItem { Content = Path.GetFileName(dir) };
                    ListViewFolder.Items.Add(directoryItem);
                }
            }
            BTN_NEWFOLDER.IsEnabled = Directory.Exists(currentPath);
            BTN_OK.IsEnabled = false;
        }

        private void ListViewFolder_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ListViewFolder.SelectedItem is ListViewItem selectedItem)
            {
                var selectedContent = selectedItem.Content.ToString();
                if (currentPath == "/Home")
                {
                    if (selectedContent == "Documents")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    else if (selectedContent == "Pictures")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                    else if (selectedContent == "Videos")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
                    else if (selectedContent == "Downloads")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    else if (selectedContent == "Music")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
                    else if (selectedContent == "Desktop")
                        currentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                    if (selectedContent.Contains(":"))
                    {
                        currentPath = selectedContent.Split(' ')[0];
                    }
                }
                else
                {
                    currentPath = Path.Combine(currentPath, selectedContent);
                }

                UpdateFolderView();
            }
        }

        private void BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(0);
            Close();
        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), ".temp");
            File.WriteAllText(tempFilePath, selectPath);
            this.Close();
        }

        private void ListViewFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewFolder.SelectedItem is ListViewItem selectedItem)
            {
                if (currentPath == "/Home")
                {
                    var selectedContent = selectedItem.Content.ToString();
                    var selectPath_ = "";

                    if (selectedContent == "Documents")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    else if (selectedContent == "Pictures")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                    else if (selectedContent == "Videos")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
                    else if (selectedContent == "Downloads")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    else if (selectedContent == "Music")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
                    else if (selectedContent == "Desktop")
                        selectPath_ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                    BTN_OK.IsEnabled = true;
                    selectPath = selectPath_;

                    if (Directory.Exists(selectedContent))
                    {
                        BTN_OK.IsEnabled = true;
                        selectPath = selectedContent;
                    }
                }
                else
                {
                    var tempP = Path.Combine(currentPath, selectedItem.Content.ToString());
                    selectPath = tempP;
                    BTN_OK.IsEnabled = Directory.Exists(tempP);
                }

            }
        }

        private void BTN_NEWFOLDER_Click(object sender, RoutedEventArgs e)
        {
            var newFolderName = "New Folder";
            var dialog = new MessageBox
            {
                Title = "Create a new folder",
                Content = "Please enter a name for the new folder："
            };

            var inputTextBox = new TextBox { Text = newFolderName };
            dialog.Content = inputTextBox;

            var result = dialog.ShowDialog();

            if (result == MessageBoxResult.OK)
            {
                newFolderName = inputTextBox.Text.Trim();
                if (string.IsNullOrEmpty(newFolderName))
                {
                    MessageBox.Show("The folder name cannot be empty.", "Wrongs", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newFolderPath = Path.Combine(currentPath, newFolderName);
                if (Directory.Exists(newFolderPath))
                {
                    MessageBox.Show("The folder already exists.", "Wrongs", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    Directory.CreateDirectory(newFolderPath);
                    UpdateFolderView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create folder:{ex.Message}", "Wrongs", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
