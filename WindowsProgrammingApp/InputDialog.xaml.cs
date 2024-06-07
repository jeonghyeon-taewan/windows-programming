using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectFileManage
{
    /// <summary>
    /// InputDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputDialog : Window
    {
        public string ResponseText { get; private set; }

        public InputDialog(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            QuestionTextBlock.Text = question;
            InputTextBox.Text = defaultAnswer;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = InputTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
