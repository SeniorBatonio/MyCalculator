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
 
            this.MainField.TextChanged += (object sender, TextChangedEventArgs e) => MainField.Text = MainField.Text.TrimStart('0', ' ', '+');

            AddNumbersHendlers((object sender, RoutedEventArgs e) =>
            {
                var button = (Button)sender;
                this.MainField.Text += button.Content.ToString();
            });

            this.AddButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += AddButton.Content.ToString();
            this.SubtractButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += SubtractButton.Content.ToString();
            this.DotButton.Click += (object sender, RoutedEventArgs e) => this.MainField.Text += DotButton.Content.ToString();
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
            };
            this.DeleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                MainField.Text = MainField.Text.Remove(MainField.Text.Length - 1);
                RemoveNumbersHendlers(ReadContent);
            };
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNumbersHendlers(NewCalculation);
            this.calculating = Calculation.Divide;
            this.a = MainField.Text.Remove(MainField.Text.Length - 1);
            AddNumbersHendlers(ReadContent);
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNumbersHendlers(NewCalculation);
            this.calculating = Calculation.Multiply;
            this.a = MainField.Text.Remove(MainField.Text.Length - 1);
            AddNumbersHendlers(ReadContent);
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNumbersHendlers(NewCalculation);
            this.calculating = Calculation.Subtract;
            this.a = MainField.Text.Remove(MainField.Text.Length - 1);
            AddNumbersHendlers(ReadContent);
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
        }

        private void ReadContent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            this.b += button.Content;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNumbersHendlers(NewCalculation);
            this.calculating = Calculation.Add;
            this.a = MainField.Text.Remove(MainField.Text.Length - 1);
            AddNumbersHendlers(ReadContent);
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void NewCalculation(object sender, RoutedEventArgs e)
        {
            MainField.Clear();
            RemoveNumbersHendlers(NewCalculation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
