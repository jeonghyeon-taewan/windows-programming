using Microsoft.Win32;
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

                    // 이미지 크기를 조정합니다.
                    image.Width = bitmap.PixelWidth / 10; // 이미지 크기를 절반으로 줄입니다.
                    image.Height = bitmap.PixelHeight / 10;

                    var thumb = new Thumb();
                    thumb.Width = 100;
                    thumb.Height = 10;
                    thumb.Background = System.Windows.Media.Brushes.Gray;
                    thumb.DragDelta += Thumb_DragDelta;

                    // 이미지와 Thumb을 포함하는 StackPanel을 생성합니다.
                    var stackPanel = new StackPanel();
                    stackPanel.Children.Add(image);
                    stackPanel.Children.Add(thumb);

          

                    var container = new InlineUIContainer(stackPanel);

                    var paragraph = new Paragraph(container);
                    richTextBox.Document.Blocks.Add(paragraph);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지를 삽입하는 도중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Thumb을 클릭할 때 이벤트가 발생하도록 합니다.
            e.Handled = false;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            var stackPanel = thumb.Parent as StackPanel;
            var image = stackPanel.Children[0] as Image;

            // 이미지의 너비가 최소값인 50보다 작으면 크기를 조정하지 않습니다.
            if (image.Width + e.HorizontalChange >= 50)
            {
              
                image.Width += e.HorizontalChange;
            }

            // 이미지의 높이가 최소값인 50보다 작으면 크기를 조정하지 않습니다.
            if (image.Height + e.VerticalChange >= 50)
            {
                image.Height += e.VerticalChange;
            }
         
        }


        private void PasteRecentImage(object sender, RoutedEventArgs e)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject != null)
            {
                
        
                    try
                    {
                        // 클립보드에서 이미지를 가져옵니다.
                        BitmapSource bitmapSource = Clipboard.GetImage();

                        // 가져온 이미지를 Image 컨트롤에 설정합니다.
                        Image image = new Image();
                    image.Source = bitmapSource;

                    // 이미지 크기를 조정합니다.
                    image.Width = bitmapSource.PixelWidth; // 이미지 크기를 절반으로 줄입니다.
                    image.Height = bitmapSource.PixelHeight;

                    var thumb = new Thumb();
                    thumb.Width = 100;
                    thumb.Height = 10;
                    thumb.Background = System.Windows.Media.Brushes.Gray;
                    thumb.DragDelta += Thumb_DragDelta;

                    // 이미지와 Thumb을 포함하는 StackPanel을 생성합니다.
                    var stackPanel = new StackPanel();
                    stackPanel.Children.Add(image);
                    stackPanel.Children.Add(thumb);

              

                    var container = new InlineUIContainer(stackPanel);

                    TextPointer caretPosition = richTextBox.CaretPosition;
                    caretPosition.InsertParagraphBreak();
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