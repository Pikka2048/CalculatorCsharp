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

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CalcString = "";
        private int CalcStack = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        void NumButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "btn_0":
                    CalcString += "0";
                    break;
                case "btn_1":
                    CalcString += "1";
                    break;
                case "btn_2":
                    CalcString += "2";
                    break;
                case "btn_3":
                    CalcString += "3";
                    break;
                case "btn_4":
                    CalcString += "4";
                    break;
                case "btn_5":
                    CalcString += "5";
                    break;
                case "btn_6":
                    CalcString += "6";
                    break;
                case "btn_7":
                    CalcString += "7";
                    break;
                case "btn_8":
                    CalcString += "8";
                    break;
                case "btn_9":
                    CalcString += "9";
                    break;
                case "btn_add":
                    CalcString += "+";
                    break;
                case "btn_sub":
                    CalcString += "-";
                    break;
                case "btn_mul":
                    CalcString += "*";
                    break;
                case "btn_div":
                    CalcString += "/";
                    break;
            }
            ResultCalc.Text = CalcString;
        }

        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            if (CalcString.Length == 0) return;
            CalcString = CalcString.Remove(CalcString.Length - 1, 1);
            ResultCalc.Text = CalcString;
        }
        private List<String> ParseString2List(string calcStr)
        {
            string calc = calcStr;
            string[] splited = calc.Split(new[] { '+', '-', '*', '/' });

            List<String> calc_list = new List<string>();
            int lastStringLengh = 0;
            //演算記号を特定する
            for (int i = 0; i < splited.Length; i++)
            {
                calc_list.Add(splited[i]);
                int sepLen = splited[i].Length + lastStringLengh;

                if (calc.Length > sepLen)
                {
                    calc_list.Add(calc[sepLen].ToString());
                    //演算記号分増やす
                    lastStringLengh = sepLen + 1;
                }
            }
            return calc_list;
        }
        private int CalcFromList(List<String> target)
        {
            for (int i = 0; i < target.Count; i++)
            {
                switch (target[i])
                {
                    case "+":
                        int resadd = Add(target[i - 1], target[i + 1]);
                        //演算記号の前後を削除する
                        target.RemoveRange(i - 1, 2);
                        //結果をセット
                        target[i - 1] = resadd.ToString();
                        CalcFromList(target);
                        break;
                    case "-":
                        int ressub = Sub(target[i - 1], target[i + 1]);
                        target.RemoveRange(i - 1, 2);
                        target[i - 1] = ressub.ToString();
                        CalcFromList(target);
                        break;
                    case "*":
                        break;
                    case "/":
                        break;
                }
            }
            return -1;
        }
        int TryToInt(string str)
        {
            int result = 0;
            if (str == "") return 0;
            try
            {
                result = int.Parse(str);
            }
            catch (FormatException)
            {
                MessageBox.Show("Int32 Parse Error","ERROR", MessageBoxButton.OK);
            }
            return result;
        }
        int Add(string n1, string n2)
        {
            int result = TryToInt(n1) + TryToInt(n2);
            CalcStack = result;
            return result;
        }
        int Sub(string n1, string n2)
        {
            int result = TryToInt(n1) - TryToInt(n2);
            CalcStack = result;
            return result;
        }
        // 演算記号で文字を区切りリストに入れる
        // *、/ を先に計算する
        // +,- を実行する
        // 結果を表示する
        private void RunCalc(object sender, RoutedEventArgs e)
        {
            if (CalcString.Length == 0) return;

            CalcStack = 0;

            List<String> ParsedList;
            ParsedList = ParseString2List(CalcString);

            CalcFromList(ParsedList);

            //Set Result
            CalcString = CalcStack.ToString();
            ResultCalc.Text = CalcStack.ToString();
        }
    }
}
