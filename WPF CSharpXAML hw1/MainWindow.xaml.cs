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
        public delegate double Mathematic(double num1, double num2);
        Mathematic action;
        string box1 = String.Empty;
        bool write = true;
        bool clear = false;
        bool mathSymbol = false;
        string replace;
        bool defaultNull = true;
        bool clearTextbox1 = false;
        bool deleting = false;
        bool nullpressed = false;
        bool dotpressed = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Math_Operation(object sender, char operat)
        {

            if (clear) { textBox1.Content = String.Empty; textBox2.Content = String.Empty; clear = false; }
            switch (operat)
            {
                case '+':
                    Rename_TextBox2('+');
                    break;
                case '-':
                    Rename_TextBox2('-');
                    break;
                case '/':
                    Rename_TextBox2('/');
                    break;
                case '*':
                    Rename_TextBox2('*');
                    break;
                case '=':
                    textBox2.Content += textBox1.Content.ToString() + '=';
                    box1 = Calc().ToString();
                    write = true;
                    clear = true;
                    break;
                default:
                    if (char.IsDigit(operat) || operat == ',')
                    {
                        if (write)
                        {
                            textBox1.Content = String.Empty;
                            write = false;
                            mathSymbol = true;
                        }
                        if (!deleting)
                        {
                            mathSymbol = false;
                            defaultNull = false;
                        }
                    }
                    break;
            }
            if (write)
            {
                textBox1.Content = box1;
            }
            if (clearTextbox1)
            {
                textBox1.Content = String.Empty;
                clearTextbox1 = false;
                deleting = false;
            }
        }

        private void Rename_TextBox2(char symbol)
        {
            if (mathSymbol == false)
            {
                if (textBox1.Content.ToString() == "")
                {
                    textBox1.Content = "0";
                }
                textBox2.Content += textBox1.Content.ToString() + symbol;
                box1 = Calc().ToString();
                write = true;
            }
            else
            {
                deleting = true;
                replace = String.Empty;
                for (int i = 0; i < textBox2.Content.ToString().Length - 1; i++)
                {
                    replace += textBox2.Content.ToString()[i];
                }
                textBox2.Content = replace + symbol;
                clearTextbox1 = true;
            }
        }

        private double Calc()
        {
            double num1 = 0, num2 = 0;
            bool operat = false;
            bool dot = false;
            int dotnum1 = 1;
            int dotnum2 = 1;
            for (int i = 0; i < textBox2.Content.ToString().Length; i++)
            {
                switch (textBox2.Content.ToString().ElementAt(i))
                {
                    case '+':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x + y;
                        dot = false;
                        operat = true;
                        break;
                    case '-':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x - y;
                        dot = false;
                        operat = true;
                        break;
                    case '*':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x * y;
                        dot = false;
                        operat = true;
                        break;
                    case '/':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x / y;
                        dot = false;
                        operat = true;
                        break;
                    case '=':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        return num1;
                    default:
                        if (!operat)
                        {
                            if (textBox2.Content.ToString().ElementAt(i) != ',')
                            {
                                num1 *= 10;
                                num1 += Double.Parse(textBox2.Content.ToString().ElementAt(i).ToString());
                                if (dot) { dotnum1 *= 10; }
                                break;
                            }
                            dot = true;
                            break;
                        }
                        if (textBox2.Content.ToString().ElementAt(i) != ',')
                        {
                            num2 *= 10;
                            num2 += Double.Parse(textBox2.Content.ToString().ElementAt(i).ToString());
                            if (dot) { dotnum2 *= 10; }
                            break;
                        }
                        dot = true;
                        break;
                }
            }
            return num1;
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            char buttonSymbol = (sender as Button).Content.ToString().ToCharArray()[0];
            if (nullpressed && !dotpressed)
            {
                if (Char.IsDigit(buttonSymbol))
                {
                    textBox1.Content = String.Empty;
                    nullpressed = false;
                }
            }
            if (buttonSymbol == '0' && (textBox1.Content.ToString() == String.Empty || defaultNull)) { nullpressed = true; }
            if (dotpressed && buttonSymbol == ',') { return; }
            else if (buttonSymbol == ',')
            {
                if (textBox1.Content.ToString() == String.Empty || defaultNull || write) { textBox1.Content = "0"; write = false; defaultNull = false; }
                dotpressed = true;
            }
            Math_Operation(sender, buttonSymbol);
            if (defaultNull)
            {
                textBox1.Content = (sender as Button).Content.ToString();
                defaultNull = false;
                return;
            }
            textBox1.Content += (sender as Button).Content.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Content = "0";
            textBox2.Content = String.Empty;
            clear = false;
            defaultNull = true;
            write = true;
            dotpressed = false;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            replace = "0";
            if (!write)
            {
                replace = String.Empty;
                bool work = false;
                int i = 0;
                for (; i < textBox1.Content.ToString().Length - 1; i++)
                {
                    work = true;
                    replace += textBox1.Content.ToString()[i];
                }
                if (work)
                {
                    if (textBox1.Content.ToString()[i] == ',')
                    {
                        dotpressed = false;
                    }
                }
                else
                {
                    replace = "0";
                    write = true;
                    defaultNull = true;
                }
            }
            textBox1.Content = replace;
        }

        private void MathSymbol_Click(object sender, EventArgs e)
        {
            dotpressed = false;
            Math_Operation(sender, (sender as Button).Content.ToString().ToCharArray()[0]);
        }

        private void CE_Click(object sender, EventArgs e)
        {
            textBox1.Content = "0";
            clear = false;
            defaultNull = true;
            write = true;
            dotpressed = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            char key = e.Key.ToString()[e.Key.ToString().Length - 1];
            Button tmp = new Button();
            if (Char.IsDigit(key) || key == ',')
            {
                tmp.Content = key.ToString();
                Digit_Click(tmp, e);
                return;
            }
            if (e.Key.ToString() == "Back")
            {
                Back_Click(sender, e);
                return;
            }
            switch (e.Key.ToString())
            {
                case "Back":
                    Back_Click(sender, e);
                    return;
                case "Add":
                case "OemPlus":
                    key = '+';
                    break;
                case "Subtract":
                case "OemMinus":
                    key = '-';
                    break;
                case "Multiply":
                    key = '*';
                    break;
                case "Divide":
                    key = '/';
                    break;
                case "Return":
                    key = '=';
                    break;
                default:
                    return;
            }
            tmp.Content = key.ToString();
            MathSymbol_Click(tmp, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Клавиатура тоже работает!", "Info");
        }
    }
}
