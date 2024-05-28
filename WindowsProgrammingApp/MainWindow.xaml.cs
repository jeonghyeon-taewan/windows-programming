using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Syncfusion.Windows.PdfViewer;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;


namespace WindowsProgrammingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
           
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
            myColorPicker.Color = Colors.Black;
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
            TextRange range;
            FileStream fStream;
            range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            string directoryPath = @"C:\workspace\test";
            string filePath = Path.Combine(directoryPath, "test.xaml");

            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            fStream = new FileStream(filePath, FileMode.Create);
            range.Save(fStream, DataFormats.XamlPackage);
            fStream.Close();
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            TextRange range;
            FileStream fStream;
            if (File.Exists("C:\\workspace\\test\\test.xaml"))
            {
                range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                fStream = new FileStream("C:\\workspace\\test\\test.xaml", FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.XamlPackage);
                fStream.Close();
            }


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
                double currentFontSize = selectedText.GetPropertyValue(TextElement.FontSizeProperty)is double size ? size : 12;
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
        private void OpenModalWindow_Click(object sender, RoutedEventArgs e) {

            Window1 modalWindow = new Window1(shortcuts);

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

                shortcuts[action]=shortcut;
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
    

        private void LoadPDFs(object sender, RoutedEventArgs e)
        {
            try
            {
             

                // Get the folder path from the TextBox
                string folderPath = "C:\\Users\\jeonghyeon\\Desktop\\3-1";

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
                        listbox.Items.Add(file);
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
        private void OnFileSelected(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected file
            if (listbox.SelectedItem is string selectedFile)
            {
               
                // Load the selected PDF file in the PdfViewerControl
                pdfViewer.Load(selectedFile);
            }
        }


    }

}


