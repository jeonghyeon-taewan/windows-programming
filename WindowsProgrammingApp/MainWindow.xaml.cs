using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Shared;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Path = System.IO.Path;


namespace WindowsProgrammingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? rootFolderPath;
        private string currentFile;
        double orginalWidth, originalHeight;
        ScaleTransform scale = new ScaleTransform();


        Dictionary<string, string> shortcuts = new Dictionary<string, string>
{
    { Const.INCREASE_FONT_SIZE, "P" },
    { Const.DECREASE_FONT_SIZE, "O" },
    { Const.CHANGE_TEXT_COLOR, "T" },
                { Const.CHANGE_HIGHLIGHT_COLOR, "K" },
                { Const.BOLD, "B" },
                { Const.ITALICK, "I" },
                { Const.UNDERLINE, "U" },
                { Const.MIDDLELINE, "M" },
};
        public MainWindow()
        {
            InitializeComponent();
            SetRootFolder();
            LoadTreeView();
            myColorPicker.Color = Colors.Black;
            InitializeFontSizeComboBox();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);

            colDef5 = new ColumnDefinition { Width = new GridLength(5) };
            colDef2Star = new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star), MinWidth = 100 };



        }
        private bool _isMenuOpen = false;

        private void ToggleMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isMenuOpen)
            {
                CloseMenu();
                RemoveColumns();
            }
            else
            {
                OpenMenu();
                AddColumns();
            }
        }
        private ColumnDefinition colDef5;
        private ColumnDefinition colDef2Star;
        private void AddColumns()
        {
            // Add the column definitions to the grid
            myGrid.ColumnDefinitions.Add(colDef5);
            myGrid.ColumnDefinitions.Add(colDef2Star);
        }

        private void RemoveColumns()
        {
            // Remove the column definitions from the grid
            myGrid.ColumnDefinitions.Remove(colDef5);
            myGrid.ColumnDefinitions.Remove(colDef2Star);
        }

        private void OpenMenu()
        {
            HiddenPanel.Visibility = Visibility.Visible;
            splitter.Visibility = Visibility.Visible;
            Storyboard openStoryboard = (Storyboard)FindResource("OpenMenuAnimation");
            openStoryboard.Begin();
            _isMenuOpen = true;
        }

        private void CloseMenu()
        {
            Storyboard closeStoryboard = (Storyboard)FindResource("CloseMenuAnimation");
            closeStoryboard.Completed += (s, e) => HiddenPanel.Visibility = Visibility.Collapsed;
            closeStoryboard.Completed += (s, e) => splitter.Visibility = Visibility.Collapsed;
            closeStoryboard.Begin();
            _isMenuOpen = false;
        }
        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            orginalWidth = this.Width;
            originalHeight = this.Height;

            if (this.WindowState == WindowState.Maximized)
            {
                ChangeSize(this.ActualWidth, this.ActualHeight);
            }

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);
        }

        private void ChangeSize(double width, double height)
        {
    

            pdfViewer.Height = height; 
            FrameworkElement rootElement = this.Content as FrameworkElement;

            rootElement.LayoutTransform = scale;
        }

        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (e.Key == getKey(shortcuts[Const.CHANGE_TEXT_COLOR]))
                {
                    ChangeTextColor_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.DECREASE_FONT_SIZE]))
                {
                    DecreaseFontSize_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.INCREASE_FONT_SIZE]))
                {
                    IncreaseFontSize_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.UNDERLINE]))
                {
                    UnderlineText_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.MIDDLELINE]))
                {
                    MiddlelineText_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.BOLD]))
                {
                    BoldText_Click(null, null);
                    e.Handled = true;
                }
                if (e.Key == getKey(shortcuts[Const.ITALICK]))
                {
                    ItalicText_Click(null, null);
                    e.Handled = true;
                }
            }
        }
        private void functionPlay(string func)
        {
            switch (func)
            {
                case Const.CHANGE_TEXT_COLOR:
                    ChangeTextColor_Click(null, null);
                    break;
                case Const.INCREASE_FONT_SIZE:
                    IncreaseFontSize_Click(null, null);
                    break;
                case Const.DECREASE_FONT_SIZE:
                    DecreaseFontSize_Click(null, null);
                    break;
                case Const.UNDERLINE:
                    UnderlineText_Click(null, null);
                    break;
                case Const.MIDDLELINE:
                    MiddlelineText_Click(null, null);
                    break;
                case Const.BOLD:
                    BoldText_Click(null, null);
                    break;
                case Const.ITALICK:
                    ItalicText_Click(null, null);
                    break;
            }
        }
        private Key getKey(string key)
        {
            switch (key)
            {
                case "A": return Key.A;
                case "B": return Key.B;
                case "C": return Key.C;
                case "D": return Key.D;
                case "E": return Key.E;
                case "F": return Key.F;
                case "G": return Key.G;
                case "H": return Key.H;
                case "I": return Key.I;
                case "J": return Key.J;
                case "K": return Key.K;
                case "L": return Key.L;
                case "M": return Key.M;
                case "N": return Key.N;
                case "O": return Key.O;
                case "P": return Key.P;
                case "Q": return Key.Q;
                case "R": return Key.R;
                case "S": return Key.S;
                case "T": return Key.T;
                case "U": return Key.U;
                case "V": return Key.V;
                case "W": return Key.W;
                case "X": return Key.X;
                case "Y": return Key.Y;
                case "Z": return Key.Z;
            }
            return Key.None;
        }



        private void rtb_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextRange range = richTextBox.Selection;
            IList<Image> images = new List<Image>();
            for (var position = range.Start;
                position != null && position.CompareTo(range.End) <= 0;
                position = position.GetNextContextPosition(LogicalDirection.Forward))
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.ElementStart
                    && position.GetAdjacentElement(LogicalDirection.Forward) is InlineUIContainer uic && uic.Child is Image img)
                {
                    images.Add(img);
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                // 휠을 올릴 때
                if (e.Delta > 0)
                {
                    foreach (Image image in images)
                    {
                        image.Width *= 1.1;
                        image.Height *= 1.1;
                    }
                    ChangeFontSize(1);


                }
                // 휠을 내릴 때
                else if (e.Delta < 0)
                {
                    foreach (Image image in images)
                    {
                        image.Width *= 0.9;
                        image.Height *= 0.9;
                    }
                    ChangeFontSize(-1);
                }
            }

            // 이벤트를 처리했으므로 버블링을 중단.
            e.Handled = true;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            save(currentFile);
            //var folderDialog = new OpenFolderDialog
            //{
            //    // Set options here
            //};

            //if (folderDialog.ShowDialog() == true)
            //{
            //    var folderName = folderDialog.FolderName;
            //    MessageBox.Show(folderName);
            //    // Do something with the result
            //}
            //TextRange range;
            //FileStream fStream;
            //range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            //string directoryPath = @"C:\Users\jeonghyeon\Desktop";
            //string filePath = Path.Combine(directoryPath, "zzzzzz.xaml");


            //if (!Directory.Exists(directoryPath))
            //{
            //    //Directory.CreateDirectory(directoryPath);
            //}
            //else
            //{
            //    fStream = new FileStream(filePath, FileMode.Create);
            //    range.Save(fStream, DataFormats.XamlPackage);
            //    fStream.Close();
            //}


        }
        private void save(string path)
        {
            MessageBox.Show(path);
            TextRange range;
            FileStream fStream = null;

            range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                }
                else
                {
                    File.Delete(path); // 파일 삭제
                    fStream = new FileStream(path, FileMode.Create);
                    range.Save(fStream, DataFormats.XamlPackage);
                }

       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message);
            }
            finally
            {
                fStream?.Close();
            }
        }



        private void Load(object sender, RoutedEventArgs e)
        {
         

        }
        private void InsertImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var image = new Image();
                    var bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                    image.Source = bitmap;

                    image.Width = bitmap.PixelWidth / 10;
                    image.Height = bitmap.PixelHeight / 10;


                    var container = new InlineUIContainer(image);

                    var paragraph = new Paragraph(container);
                    richTextBox.Document.Blocks.Add(paragraph);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지를 삽입하는 도중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }
        private void IncreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            ChangeFontSize(2);
        }

        private void DecreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            ChangeFontSize(-2);
        }

        private void ChangeFontSize(int delta)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                double currentFontSize = selectedText.GetPropertyValue(TextElement.FontSizeProperty) is double size ? size : 12;
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, currentFontSize + delta);
            }
        }

        private void ChangeTextColor_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {

                selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(myColorPicker.Color));
            }

        }
        private void ChangeHighlightColor_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {

                selectedText.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(myColorPicker.Color));
            }
        }
        private void UnderlineText_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                TextRange selectionRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
                if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
        }
        private void MiddlelineText_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                TextRange selectionRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);

                if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Strikethrough)
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);

                }

            }

        }

        private void BoldText_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                var currentWeight = selectedText.GetPropertyValue(TextElement.FontWeightProperty);

                if (currentWeight != DependencyProperty.UnsetValue && currentWeight.Equals(FontWeights.Bold))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
        }
        private void ItalicText_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                FontStyle currentStyle = (FontStyle)selectedText.GetPropertyValue(TextElement.FontStyleProperty);

                if (currentStyle == FontStyles.Italic)
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
            }
        }



        private void RichTextBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[]? fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (fileNames != null && fileNames.Length > 0)
                {
                    string fileExtension = Path.GetExtension(fileNames[0]).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".bmp" || fileExtension == ".gif")
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void RichTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[]? fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (fileNames != null && fileNames.Length > 0)
                {
                    string filePath = fileNames[0];
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".bmp" || fileExtension == ".gif")
                    {
                        try
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                            Image image = new Image
                            {
                                Source = bitmap,
                                Width = bitmap.PixelWidth,
                                Height = bitmap.PixelHeight
                            };

                            InlineUIContainer container = new InlineUIContainer(image);
                            Paragraph paragraph = richTextBox.CaretPosition.Paragraph;

                            if (paragraph != null)
                            {
                                paragraph.Inlines.Add(container);
                            }
                            else
                            {
                                richTextBox.Document.Blocks.Add(new Paragraph(container));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"이미지를 로드하는 중 오류가 발생했습니다: {ex.Message}");
                        }
                    }
                }
            }
        }
        private void OpenModalWindow_Click(object sender, RoutedEventArgs e)
        {

            CustomShortCut modalWindow = new CustomShortCut(shortcuts);

            // Window1에서 발생한 이벤트를 구독
            modalWindow.ShortcutsSaved += ModalWindow_ShortcutsSaved;

            bool? dialogResult = modalWindow.ShowDialog();
            if (dialogResult == true)
            {
            }
            else
            {
            }
        }
        private void ModalWindow_ShortcutsSaved(object sender, Dictionary<string, string> savedShortcuts)
        {
            // 저장된 단축키를 가져와서 처리
            foreach (var kvp in savedShortcuts)
            {
                string action = kvp.Key;
                string shortcut = kvp.Value;

                shortcuts[action] = shortcut;
            }
        }
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (richTextBox != null && FontSizeComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)FontSizeComboBox.SelectedItem;
                double fontSize;
                if (double.TryParse(selectedItem.Content.ToString(), out fontSize))
                {
                    if (richTextBox.Selection.Start == richTextBox.Selection.End)
                    {
                        TextRange documentRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        documentRange.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                    }
                    else
                    {
                        richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                    }
                }
            }
        }
        private void SaveToLocal(string path)
        {
            try
            {
                // RichTextBox의 내용을 텍스트로 추출
                string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

                // 파일에 텍스트를 쓰기
                File.WriteAllText(path, text);

                MessageBox.Show("저장되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일을 저장하는 동안 오류가 발생했습니다: " + ex.Message);
            }
        }


        //private void LoadPDFs(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {


        //        // Get the folder path from the TextBox
        //        string folderPath = "C:\\Users\\jeonghyeon\\Desktop\\3-1";

        //        // Check if the folder exists
        //        if (Directory.Exists(folderPath))
        //        {
        //            // Get all files in the directory and subdirectories
        //            string[] files = GetAllFiles(folderPath);

        //            // Filter files to include only .xaml and .pdf
        //            var filteredFiles = files.Where(file => file.EndsWith(".xaml") || file.EndsWith(".pdf"));

        //            // Add each filtered file to the ListBox
        //            foreach (string file in filteredFiles)
        //            {
        //                listbox.Items.Add(file);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("The specified folder path does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

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
        private void InitializeFontSizeComboBox()
        {
            for (int i = 6; i <= 50; i++)
            {
                FontSizeComboBox.Items.Add(new ComboBoxItem { Content = i.ToString() });
            }
            FontSizeComboBox.SelectedIndex = 8; // Default selected index
        }


        private void LoadTreeView()
        {

            var rootDirectoryInfo = new DirectoryInfo(rootFolderPath);
            FoldersTreeView.Items.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private void SetRootFolder()
        {
            var folderDialog = new OpenFolderDialog
            {
                // Set options here
            };

            if (folderDialog.ShowDialog() == true)
            {
                rootFolderPath = folderDialog.FolderName;
                if (!Directory.Exists(rootFolderPath))
                {
                    MessageBox.Show("지정된 경로가 유효하지 않습니다.");
                }
                else
                {
                }
            }

        }
        private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name, Tag = directoryInfo };
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Items.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Items.Add(new TreeViewItem { Header = file.Name, Tag = file });
            return directoryNode;
        }

        private void NewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItem(isFolder: true);
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItem(isFolder: false);
        }
        private void CreateNewItem(bool isFolder)
        {
            var selectedItem = FoldersTreeView.SelectedItem as TreeViewItem;
            if (selectedItem == null) return;
            var directoryInfo = selectedItem.Tag as DirectoryInfo;
            if (directoryInfo == null || !Directory.Exists(directoryInfo.FullName))
            {
                return;
            }
            TreeViewItem newItem = new TreeViewItem();
            TextBox textBox = new TextBox();
            textBox.Text = "이름";
            textBox.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    string newName = textBox.Text.Trim();
                    if (string.IsNullOrEmpty(newName)) return;

                    var selectedDirectory = selectedItem.Tag as DirectoryInfo;
                    if (selectedDirectory == null) return;

                    string newPath = isFolder ?
                        System.IO.Path.Combine(selectedDirectory.FullName, newName) :
                        System.IO.Path.Combine(selectedDirectory.FullName, newName + ".xaml");

                    try
                    {
                        if (isFolder)
                        {
                            Directory.CreateDirectory(newPath);
                            newItem.Header = newName;
                            newItem.Tag = new DirectoryInfo(newPath);
                        }
                        else
                        {
                        
                            TextRange range;
                            FileStream fStream;
                            RichTextBox box = new RichTextBox();

                            range = new TextRange(box.Document.ContentStart, box.Document.ContentEnd);
                            fStream = new FileStream(newPath, FileMode.Create);
                            range.Save(fStream, DataFormats.XamlPackage);
                            fStream.Close();
                            newItem.Header = newName + ".xaml";
                            newItem.Tag = new FileInfo(newPath);
                        }
                        newItem.IsExpanded = true; // 새로운 항목을 확장 상태로 설정합니다.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error occurred: {ex.Message}");
                    }
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                // Remove the textbox/new item if no name is provided (optional)
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    selectedItem.Items.Remove(newItem);
                }
            };

            newItem.Header = textBox;
            selectedItem.Items.Add(newItem);
            selectedItem.IsExpanded = true;

            textBox.Focus();
        }
        private void FoldersTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 선택된 아이템을 가져옵니다.
            var selectedItem = e.NewValue as TreeViewItem;
            if (selectedItem != null)
            {
                // 선택된 아이템의 Tag 속성을 DirectoryInfo 또는 FileInfo로 캐스팅합니다.
                var selectedDirectoryInfo = selectedItem.Tag as DirectoryInfo;
                var selectedFileInfo = selectedItem.Tag as FileInfo;

                if (selectedDirectoryInfo != null)
                {
                }
                else if (selectedFileInfo != null)
                {
             
                    // 선택된 아이템이 .pdf 파일인지 확인합니다.
                    if (selectedFileInfo.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        pdfViewer.Load(selectedFileInfo.FullName);
                        pdfViewer.Visibility = Visibility.Visible;
                        richTextBox.Visibility = Visibility.Collapsed;
                        settingText.Visibility = Visibility.Collapsed;
                        fileName.Visibility = Visibility.Collapsed;
                    }
                    else if(selectedFileInfo.FullName.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase))
                    {
               

                        TextRange range;
                        FileStream fStream;
                        if (File.Exists(selectedFileInfo.FullName))
                        {
                            range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                            fStream = new FileStream(selectedFileInfo.FullName, FileMode.OpenOrCreate);
                            range.Load(fStream, DataFormats.XamlPackage);
                            fStream.Close();
                            pdfViewer.Visibility = Visibility.Collapsed;
                            richTextBox.Visibility = Visibility.Visible;
                            settingText.Visibility = Visibility.Visible;
                            currentFile = selectedFileInfo.FullName;
                            fileName.Visibility = Visibility.Visible;
                            fileName.Text = selectedFileInfo.Name.ToString();
                        }
                    }
                }
            }
        }
        private async void SendPostRequest()
        {
            // 버튼을 로딩 상태로 변경
            CopyTextButton.IsEnabled = false;
            CopyTextButton.Content = "Loading...";

            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=";
            string jsonBody = "{\"contents\":[{\"parts\":[{\"text\":\"" + InputTextBlock.Text + "\"}]}]}";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        string extractedText = ExtractTextFromResponse(responseBody);
                        DisplayBoldText(extractedText);
                        //string plainText = MarkdownParser.ParseMarkdownToPlainText(extractedText);
                        //Paragraph paragraph = new Paragraph();
                        //paragraph.Inlines.Add(plainText);
                        //OutputRichTextBox.Document.Blocks.Add(paragraph);

                    }
                    else
                    {
                        MessageBox.Show("Error: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
                finally
                {
                    // 응답 처리 후 버튼을 다시 활성화하고, 원래의 텍스트로 변경
                    CopyTextButton.IsEnabled = true;
                    CopyTextButton.Content = "Search";
                }
            }
        }


        private string ExtractTextFromResponse(string jsonResponse)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                var text = jsonObject["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                return text ?? "No text found";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing JSON: " + ex.Message);
                return "Error";
            }
        }

        private void DisplayBoldText(string text)
        {
            OutputRichTextBox.Document.Blocks.Clear();
            if (string.IsNullOrEmpty(text))
            {
                OutputRichTextBox.Document.Blocks.Add(new Paragraph(new Run("No text found")));
                return;
            }



            string[] parts = text.Split(new[] { "**" }, StringSplitOptions.None);
            bool isBold = false;
            Paragraph paragraph = new Paragraph();
            foreach (var part in parts)
            {
                Run runText = new Run(part)
                {
                    FontSize = 16 // 기본 텍스트 크기
                };

                if (isBold)
                {
                    runText.FontWeight = FontWeights.Bold;
                }

                paragraph.Inlines.Add(runText);
                isBold = !isBold;
            }

            OutputRichTextBox.Document.Blocks.Add(paragraph);
        }

        private void CopyTextButton_Click(object sender, RoutedEventArgs e)
        {
            SendPostRequest();
        }


    }

}


