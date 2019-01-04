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

namespace CalculatorApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
     
    enum Calculation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    

    public partial class MainWindow : Window
    {
        Calculation calculating;
        string a, b;
        double? result = null;

        public MainWindow()
        {
            InitializeComponent();

            this.MainField.TextChanged += MainField_TextChanged;

            AddNumbersHendlers((object sender, RoutedEventArgs e) =>
            {
                var button = (Button)sender;
                this.MainField.Text += button.Content.ToString();
            });
            this.MainField.KeyDown += MainField_KeyDown;
            this.AddButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += AddButton.Content.ToString();
            this.SubtractButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += SubtractButton.Content.ToString();
            this.MultiplyButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += MultiplyButton.Content.ToString();
            this.DivideButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += DivideButton.Content.ToString();
            this.AddButton.Click += AddButton_Click;
            this.SubtractButton.Click += SubtractButton_Click;
            this.MultiplyButton.Click += MultiplyButton_Click;
            this.DivideButton.Click += DivideButton_Click;
            this.EqualsButton.Click += EqualsButton_Click;
            this.ClearButton.Click += (object sender, RoutedEventArgs e) =>
            {
                MainField.Clear();
                RemoveNumbersHendlers(ReadContent);
                RemoveNumbersHendlers(NewCalculation);
                this.MainField.KeyDown -= NewCalculation;
            };
            this.DeleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                MainField.Text = MainField.Text.Remove(MainField.Text.Length - 1);
                RemoveNumbersHendlers(ReadContent);
                this.MainField.KeyDown -= NewCalculation;
            };
        }

        private void MainField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainField.Text != "0")
            {
                MainField.Text = MainField.Text.TrimStart('0', ' ', '+');
            }
            Func<char, bool> func = (c) =>
             {
                 return Char.IsDigit(c) || "×+-*/.,".Contains(c);
             };
            this.MainField.Text = new String(MainField.Text.Where(func).ToArray());
            
        }

        private void MainField_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            switch(key)
            {
                case Key.Add:
                    AddButton_Click(sender, new RoutedEventArgs());
                    break;
                case Key.Subtract:
                    SubtractButton_Click(sender, new RoutedEventArgs());
                    break;
                case Key.Multiply:
                    MultiplyButton_Click(sender, new RoutedEventArgs());
                    break;
                case Key.Divide:
                    DivideButton_Click(sender, new RoutedEventArgs());
                    break;
                case Key.Enter:
                    EqualsButton_Click(sender, new RoutedEventArgs());
                    break;
            }
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.Text != "*")
            {
                RemoveNumbersHendlers(NewCalculation);
                this.calculating = Calculation.Divide;
                this.a = MainField.Text.TrimEnd('+', '-', '*', '/', ' ', '×');
                this.a = this.a.Replace('.', ',');
                AddNumbersHendlers(ReadContent);
                this.MainField.KeyDown += ReadKeys;
            }
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.Text != "*")
            {
                RemoveNumbersHendlers(NewCalculation);
                this.calculating = Calculation.Multiply;
                this.a = MainField.Text.TrimEnd('+', '-', '*', '/', ' ', '×');
                this.a = this.a.Replace('.', ',');
                AddNumbersHendlers(ReadContent);
                this.MainField.KeyDown += ReadKeys;
            }
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.Text != "-")
            {
                RemoveNumbersHendlers(NewCalculation);
                this.calculating = Calculation.Subtract;
                this.a = MainField.Text.TrimEnd('+', '-', '*', '/', ' ', '×');
                this.a = this.a.Replace('.', ',');
                AddNumbersHendlers(ReadContent);
                this.MainField.KeyDown += ReadKeys;
            }
        }

        private void AddNumbersHendlers(RoutedEventHandler eventHandler)
        {
            this.ZeroButton.Click += eventHandler;
            this.OneButton.Click += eventHandler;
            this.TwoButton.Click += eventHandler;
            this.ThreeButton.Click += eventHandler;
            this.FourButton.Click += eventHandler;
            this.FiveButton.Click += eventHandler;
            this.SixButton.Click += eventHandler;
            this.SevenButton.Click += eventHandler;
            this.EightButton.Click += eventHandler;
            this.NineButton.Click += eventHandler;
            this.DotButton.Click += eventHandler;
        }

        private void RemoveNumbersHendlers(RoutedEventHandler eventHandler)
        {
            this.ZeroButton.Click -= eventHandler;
            this.OneButton.Click -= eventHandler;
            this.TwoButton.Click -= eventHandler;
            this.ThreeButton.Click -= eventHandler;
            this.FourButton.Click -= eventHandler;
            this.FiveButton.Click -= eventHandler;
            this.SixButton.Click -= eventHandler;
            this.SevenButton.Click -= eventHandler;
            this.EightButton.Click -= eventHandler;
            this.NineButton.Click -= eventHandler;
            this.DotButton.Click -= eventHandler;
        }

        private void ReadContent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            this.b += button.Content;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainField.Text != "+")
            {
                RemoveNumbersHendlers(NewCalculation);
                this.calculating = Calculation.Add;
                this.a = MainField.Text.TrimEnd('+', '-', '*', '/', ' ', '×');
                this.a = this.a.Replace('.', ',');
                AddNumbersHendlers(ReadContent);
                this.MainField.KeyDown += ReadKeys;
            }
        }

        private void ReadKeys(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal){ b += ','; }
            this.b += new String(e.Key.ToString().Where(Char.IsDigit).ToArray());
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainField.KeyDown -= ReadKeys;
            RemoveNumbersHendlers(ReadContent);
            var a = Convert.ToDouble(this.a);
            var b = Convert.ToDouble(this.b);
            switch (this.calculating)
            {
                case Calculation.Add:
                    result = a + b;
                    break;
                case Calculation.Subtract:
                    result = a - b;
                    break;
                case Calculation.Multiply:
                    result = a * b;
                    break;
                case Calculation.Divide:
                    result = a / b;
                    break;
            }
            if(result != null)
            {
                MainField.Text = result.ToString();
                this.b = "";
                result = null;
            }
            AddNumbersHendlers(NewCalculation);
            this.MainField.KeyDown -= MainField_KeyDown;
            this.MainField.KeyDown += NewCalculation;
        }

        private void NewCalculation(object sender, RoutedEventArgs e)
        {
            MainField.Clear();
            RemoveNumbersHendlers(NewCalculation);
            this.MainField.KeyDown -= NewCalculation;
            this.MainField.KeyDown += MainField_KeyDown;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
