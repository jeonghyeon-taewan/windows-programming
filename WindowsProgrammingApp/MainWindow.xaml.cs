using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                }
                // 휠을 내릴 때
                else if (e.Delta < 0)
                {
                    foreach (Image image in images)
                    {
                        image.Width *= 0.9;
                        image.Height *= 0.9;
                    }
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



     

   
    }


}