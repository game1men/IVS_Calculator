using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMath.Controls;

namespace GUI_Application {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        public MainWindow() {
            InitializeComponent();
            DataContext = this; // This enables binding to properties
           
        }
#pragma warning disable CS0067
        public event PropertyChangedEventHandler? PropertyChanged; // Binding event
#pragma warning restore CS0067

        public string EquationTextBox { get; set; } = @"1000.583666 + \sqrt[2]{36}";
        public string MainTextBox { get; set; } = @"1 006.583 666";

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            // Keyboard press event
            throw new NotImplementedException();
        }

        private void Number_Click(object sender, RoutedEventArgs e) { 
            if(sender is ContentControl c) {
                if(c.Content is TextBlock t) {
                    // Example
                    // MainTextBox += t.Text;

                    throw new NotImplementedException();
                }
            }
        }

        private void MathFunction_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
                throw new NotImplementedException();
            }
        }

        private void MathOperation_Click(object sender, RoutedEventArgs e) {
            if (sender is ContentControl c) {
                if (c.Content is FormulaControl fc) {
                    throw new NotImplementedException();
                } else if (c.Content is TextBlock t) {
                    // For mod operation
                    throw new NotImplementedException();
                }
            }
        }
        private void TextClenaup_Click(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
