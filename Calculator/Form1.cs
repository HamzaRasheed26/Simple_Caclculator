namespace Calculator
{
    

    public partial class frmCalculator : Form
    {
        private string print = "";

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            print = print + "0";
            textBox2.Text = print;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            print = print + "0";
            textBox2.Text = print;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            print = print + "1";
            textBox2.Text = print;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            print = print + "2";
            textBox2.Text = print;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            print = print + "3";
            textBox2.Text = print;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            print = print + "4";
            textBox2.Text = print;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            print = print + "5";
            textBox2.Text = print;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            print = print + "6";
            textBox2.Text = print;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            print = print + "7";
            textBox2.Text = print;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            print = print + "8";
            textBox2.Text = print;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            print = print + "9";
            textBox2.Text = print;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (print != "")
            {
                int idx = print.Length - 1;

                if (print[idx] == '+' || print[idx] == '-' || print[idx] == '*' || print[idx] == '/')
                {
                    print = print + "0.";
                }
                if (print[print.Length-1] != '.')
                {
                    print = print + ".";
                }
            }
            else
            {
                print = print + "0.";
            }
            textBox2.Text = print;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (print != "")
            {
                char value = (char)print[print.Length - 1];
                if (value != '+' && value != '-' && value != '*' && value != '/')
                {
                    print = print + "+";
                    textBox2.Text = print;
                }
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            if (print != "")
            {
                char value = (char)print[print.Length - 1];
                if (value != '+' && value != '-' && value != '*' && value != '/')
                {
                    print = print + "-";
                    textBox2.Text = print;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (print != "")
            {
                char value = (char)print[print.Length - 1];
                if (value != '+' && value != '-' && value != '*' && value != '/')
                {
                    print = print + "*";
                    textBox2.Text = print;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (print != "")
            {
                char value = (char)print[print.Length - 1];
                if (value != '+' && value != '-' && value != '*' && value != '/')
                {
                    print = print + "/";
                    textBox2.Text = print;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (print != "")
            {
                string value = print;
                print = "";
                for (int i = 0; i < value.Length - 1; i++)
                {
                    print = print + value[i];
                }
                textBox2.Text = print;

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            print = "";
            textBox2.Text = print;
        }

        public bool checkIsValidEquation()
        {
            if(print == "")
            {
                return false;
            }
            else if(print[print.Length-1] == '+' || print[print.Length - 1] == '-' || print[print.Length - 1] == '*' || print[print.Length - 1] == '/')
            {
                return false;
            }
            return true;
        }

        public bool checkPoints(string value)
        {
            int count = 0;
            for(int i = 0; i < value.Length; i++)
            {
                if (value[i] == '.')
                {
                    count++;
                }
            }
            if(count > 1)
            {
                return false;
            }
            return true;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int idx = 0;
            bool check = true;
            if (checkIsValidEquation())
            {
                List<double> values = new List<double>();
                List<int> conditions = new List<int>();

                string value = "";

                for (idx = 0; idx < print.Length; idx++)
                {
                    if (print[idx] >= '0' && print[idx] <= '9' || print[idx] == '.')
                    {
                        value = value + print[idx];
                        continue;
                    }
                    else if (print[idx] == '/')
                    {
                        conditions.Add(1);
                    }
                    else if (print[idx] == '*')
                    {
                        conditions.Add(2);
                    }
                    else if (print[idx] == '+')
                    {
                        conditions.Add(3);
                    }
                    else if (print[idx] == '-')
                    {
                        conditions.Add(4);
                    }

                    if (checkPoints(value))
                    {
                        values.Add(double.Parse(value));
                        value = "";
                    }
                    else
                    {
                        check = false;
                    }
                }

                if (checkPoints(value))
                {
                    values.Add(double.Parse(value));
                    value = "";

                    solveByDMAS(1, conditions, values);
                    solveByDMAS(2, conditions, values);
                    solveByDMAS(3, conditions, values);
                    solveByDMAS(4, conditions, values);

                    textBox2.Text = print + "=" + values[0].ToString();
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }

            if(check == false)
            {
                textBox2.Text = "Error !";
            }
        }

        public  void solveByDMAS(int symbol, List<int> conditions, List<double> values)
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i] == symbol)
                {
                    double result;
                    result = calculate(values[i], conditions[i], values[i + 1]);
                    values.RemoveAt(i);
                    values.RemoveAt(i);
                    conditions.RemoveAt(i);
                    values.Insert(i, result);
                }
            }
        }

        public double calculate(double value1, int condition, double value2)
        {
            if(condition == 1)
            {
                return value1 / value2;
            }
            else if (condition == 2)
            {
                return value1 * value2;
            }
            else if (condition == 3)
            {
                return value1 + value2;
            }
            else if (condition == 4)
            {
                return value1 - value2;
            }
            return 0;
        }
    }
}