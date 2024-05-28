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

namespace WindowsProgrammingApp
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        private Dictionary<string, string> currentShortcuts = new Dictionary<string, string>();
        public event EventHandler<Dictionary<string, string>> ShortcutsSaved;
        public Window1(Dictionary<string, string> shortcuts)
        {
            InitializeComponent();
            currentShortcuts = shortcuts;

            // Initialize text boxes with current shortcuts
            IncreaseFontSizeShortcut.Text = currentShortcuts[Const.INCREASE_FONT_SIZE];
            DecreaseFontSizeShortcut.Text = currentShortcuts[Const.DECREASE_FONT_SIZE];
            ChangeTextColorShortcut.Text = currentShortcuts[Const.CHANGE_TEXT_COLOR];
            UnderlineTextShortcut.Text = currentShortcuts[Const.UNDERLINE];
            MiddlelineTextShortcut.Text = currentShortcuts[Const.MIDDLELINE];
            BoldTextShortcut.Text = currentShortcuts[Const.BOLD];
            ItalicTextShortcut.Text = currentShortcuts[Const.ITALICK];
        }

        private void SaveShortcuts_Click(object sender, RoutedEventArgs e)
        {
            // Logic to save the shortcuts
            string increaseFontSizeShortcut = IncreaseFontSizeShortcut.Text;
            string decreaseFontSizeShortcut = DecreaseFontSizeShortcut.Text;
            string changeTextColorShortcut = ChangeTextColorShortcut.Text;
            string underlineTextShortcut = UnderlineTextShortcut.Text;
            string middlelineTextShortcut = MiddlelineTextShortcut.Text;
            string boldTextShortcut = BoldTextShortcut.Text;
            string italicTextShortcut = ItalicTextShortcut.Text;

            // 저장된 단축키를 딕셔너리로 묶어 이벤트에 전달
            var savedShortcuts = new Dictionary<string, string>
            {
                {Const.INCREASE_FONT_SIZE, increaseFontSizeShortcut },
                { Const.DECREASE_FONT_SIZE, decreaseFontSizeShortcut },
                { Const.CHANGE_TEXT_COLOR, changeTextColorShortcut },
                { Const.UNDERLINE, underlineTextShortcut },
                { Const.MIDDLELINE, middlelineTextShortcut },
                { Const.BOLD, boldTextShortcut },
                { Const.ITALICK, italicTextShortcut }
            };

            // 이벤트 발생
            ShortcutsSaved?.Invoke(this, savedShortcuts);

            MessageBox.Show("저장되었습니다.");
            this.Close();
        }

        private void ShortcutTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // Prevent the default behavior

            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Capture the key and modifier
                string key = e.Key == Key.System ? e.SystemKey.ToString() : e.Key.ToString();

                // Check if the key is a letter from A to Z and if the modifier is Ctrl
                if (e.Key >= Key.A && e.Key <= Key.Z )
                {
                    string shortcut =  e.Key.ToString();
                    string existingShortcut = GetAssignedShortcut(shortcut);

                    // Check if the shortcut is already assigned
                    if (existingShortcut != null && existingShortcut != textBox.Name)
                    {
                        MessageBox.Show($"The shortcut {e.Key} is already assigned to {existingShortcut}.");
                        return;
                    }

                    // Display the shortcut in the TextBox
                    textBox.Text = $"{e.Key}";
                }
                else
                {
                    MessageBox.Show("Only A-Z keys are allowed for shortcuts.");
                }
            }
        }

        private string GetAssignedShortcut(string shortcut)
        {
            foreach (var child in modal_stack_panel.Children)
            {
                if (child is TextBox textBox)
                {
                    if (textBox.Text == shortcut)
                    {
                        return textBox.Text;
                    }
                }
            }

            return null;
        }

    }
}
