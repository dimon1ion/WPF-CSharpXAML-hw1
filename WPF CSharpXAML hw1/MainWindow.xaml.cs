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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_CSharpXAML_hw1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Task2 t2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                numTextBox.Text += (sender as Button).Content.ToString();
            }
        }

        private void Clear_NumTextBox(object sender, RoutedEventArgs e)
        {
            numTextBox.Text = String.Empty;
            t2 = new Task2();
            t2.ShowDialog();
        }
    }
}
