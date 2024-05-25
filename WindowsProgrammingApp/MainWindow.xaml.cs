using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
        public MainWindow()
        {
            InitializeComponent();
            myColorPicker.Color = Colors.Black;
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
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void MiddlelineText_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = richTextBox.Selection;
            if (!selectedText.IsEmpty)
            {
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
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
            Dictionary<string, string> shortcuts = new Dictionary<string, string>
{
    { "Increase Font Size", "Ctrl+I" },
    { "Decrease Font Size", "Ctrl+D" },
    { "Change Text Color", "Ctrl+T" },
    // Add more shortcuts as needed
};
            Window1 modalWindow = new Window1(shortcuts);
            bool? dialogResult = modalWindow.ShowDialog();
            if (dialogResult == true)
            {
                MessageBox.Show("Modal window closed with OK");
            }
            else
            {
                MessageBox.Show("Modal window closed with Cancel");
            }
        }

    }

}


