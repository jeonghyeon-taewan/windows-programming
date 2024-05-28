
using System.IO;
using System.Windows;


namespace WindowsProgrammingApp
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void OnBrowseButtonClick(object sender, RoutedEventArgs e)
        {
            FolderPathTextBox.Text = "C:\\Users\\jeonghyeon\\Desktop\\3-1";
        }

        private void OnListFilesButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Clear the ListBox
                FilesListBox.Items.Clear();

                // Get the folder path from the TextBox
                string folderPath = FolderPathTextBox.Text;

                // Check if the folder exists
                if (Directory.Exists(folderPath))
                {
                    // Get all files in the directory and subdirectories
                    string[] files = GetAllFiles(folderPath);

                    // Filter files to include only .xaml and .pdf
                    var filteredFiles = files.Where(file => file.EndsWith(".xaml") || file.EndsWith(".pdf"));

                    // Add each filtered file to the ListBox
                    foreach (string file in filteredFiles)
                    {
                        FilesListBox.Items.Add(file);
                    }
                }
                else
                {
                    MessageBox.Show("The specified folder path does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string[] GetAllFiles(string path)
        {
            // Get all files in the current directory
            var files = Directory.GetFiles(path);

            // Get all subdirectories in the current directory
            var directories = Directory.GetDirectories(path);

            // For each subdirectory, get all files recursively and add them to the file list
            foreach (var directory in directories)
            {
                files = files.Concat(GetAllFiles(directory)).ToArray();
            }

            return files;
        }
    }
}
