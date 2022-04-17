/*****************************************
 * This application uses Fody Property Changed NuGet package, the licence is stated bellow:
 * The MIT License

Copyright (c) 2012 Simon Cropp and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

* https://github.com/Fody/PropertyChanged
 *****************************************/

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
        public int MainTextFontSize { get; set; } = 35;
        public string DotText { get; set; } = ".";
        public string DeleteText { get; set; } = "C";

        private void Function_Executed(object sender, ExecutedRoutedEventArgs e) {
            return;
        }

        private void Number_Executed(object sender, ExecutedRoutedEventArgs e) {
            var num = e.Parameter.ToString();
            return;
        }
        private void Operation_Executed(object sender, ExecutedRoutedEventArgs e) {
            return;
        }
        private void Equals_Executed(object sender, ExecutedRoutedEventArgs e) {
            return;
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            return;
        }

        private void Dot_Executed(object sender, ExecutedRoutedEventArgs e) {
            return;
        }
    }
}
