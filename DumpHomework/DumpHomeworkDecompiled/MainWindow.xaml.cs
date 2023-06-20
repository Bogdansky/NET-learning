using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace DumpHomeworkDecompiled
{
    public partial class MainWindow : Window, IComponentConnector
    {
        private readonly char[] _calculationSymbols;
        private bool _signClicked = false;
        public MainWindow()
        {
            this.InitializeComponent();
            _calculationSymbols = new char[] { '+', '-', '*', '/' };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var symbol = ((Button)sender).Content.ToString();

            // change 3: don't type more then one calculation sign
            if(_calculationSymbols.Contains(char.Parse(symbol)))
            {
                if (_signClicked)
                {
                    return;
                }

                _signClicked = true;
            }

            tb.Text += symbol;
        }

        private void Result_click(object sender, RoutedEventArgs e) => this.Result();

        private void Result()
        {
            // change first: if equals sign is already here, no calculations should be performed
            if (tb.Text.Contains("="))
            {
                return;
            }

            // just small refactoring
            int num1 = tb.Text.IndexOfAny(_calculationSymbols);

            // change second: if second operand is not defined, calculations will not be performed
            if (num1 + 1 == tb.Text.Length)
            {
                return;
            }

            double num2 = Convert.ToDouble(this.tb.Text.Substring(0, num1));
            string num3Str = this.tb.Text.Substring(num1 + 1, this.tb.Text.Length - num1 - 1);
            double num3 = Convert.ToDouble(num3Str);
            string str = this.tb.Text.Substring(num1, 1);

            switch (str)
            {
                case "+":
                    TextBox tb1 = this.tb;
                    tb1.Text = tb1.Text + "=" + (object)(num2 + num3);
                    break;
                case "-":
                    TextBox tb2 = this.tb;
                    tb2.Text = tb2.Text + "=" + (object)(num2 - num3);
                    break;
                case "*":
                    TextBox tb3 = this.tb;
                    tb3.Text = tb3.Text + "=" + (object)(num2 * num3);
                    break;
                default:
                    TextBox tb4 = this.tb;
                    tb4.Text = tb4.Text + "=" + (object)(num2 / num3);
                    break;
            }
        }

        private void Off_Click_1(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            this.tb.Text = "";
            _signClicked = false;
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb.Text.Length <= 0)
                return;

            if (_calculationSymbols.Any(x => x == tb.Text[tb.Text.Length - 1]))
            {
                _signClicked = false;
            }

            this.tb.Text = this.tb.Text.Substring(0, this.tb.Text.Length - 1);
        }
    }
}