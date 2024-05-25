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

        public Window1(Dictionary<string, string> shortcuts)
        {
            InitializeComponent();
            currentShortcuts = shortcuts;

            // Initialize text boxes with current shortcuts
            IncreaseFontSizeShortcut.Text = GetShortcutText(Key.I);
            DecreaseFontSizeShortcut.Text = GetShortcutText(Key.D);
            ChangeTextColorShortcut.Text = GetShortcutText(Key.T);
        }

        private string GetShortcutText(Key key)
        {
            // Check if the shortcut is already assigned
            foreach (var shortcut in currentShortcuts)
            {
                if (KeyInterop.VirtualKeyFromKey(Key.LeftCtrl) + key.ToString() == shortcut.Value)
                {
                    return shortcut.Key;
                }
            }

            return string.Empty;
        }
    
        private void SaveShortcuts_Click(object sender, RoutedEventArgs e)
        {
            // Logic to save the shortcuts
            string increaseFontSizeShortcut = IncreaseFontSizeShortcut.Text;
            string decreaseFontSizeShortcut = DecreaseFontSizeShortcut.Text;
            string changeTextColorShortcut = ChangeTextColorShortcut.Text;

            // You would save these values and apply them as key bindings in MainWindow
            // This example does not cover persistence and applying changes dynamically

            MessageBox.Show("Shortcuts saved! (Note: Applying changes is not implemented in this example)");
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
                if (  e.Key >= Key.A && e.Key <= Key.Z)
                {
                    string shortcut = KeyInterop.VirtualKeyFromKey(Key.LeftCtrl) + e.Key.ToString();
                    string existingShortcut = GetAssignedShortcut(shortcut);

                    // Check if the shortcut is already assigned
                    if (existingShortcut != null && existingShortcut != textBox.Name)
                    {
                        MessageBox.Show($"The shortcut Ctrl+{e.Key} is already assigned to {existingShortcut}.");
                        return;
                    }

                    // Display the shortcut in the TextBox
                    textBox.Text = $"Ctrl+{e.Key}";
                }
                else
                {
                    MessageBox.Show("Only Ctrl + A-Z keys are allowed for shortcuts.");
                }
            }
        }

        private string GetAssignedShortcut(string shortcut)
        {
            // Check if the shortcut is already assigned
            foreach (var kvp in currentShortcuts)
            {
                if (kvp.Value == shortcut)
                {
                    return kvp.Key;
                }
            }

            return null;
        }
    }
}
  

