using System;
using System.Windows.Forms;

namespace Case3
{
    public partial class Form1 : Form
    {
        private double currentResult = 0;      // ������� ���������
        private string currentOperation = "";  // ������� ��������
        private bool isNewEntry = true;        // ��������, �������� �� ����� ��������

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ������������� ��������� �������� �������
            textBox1.Text = "0";
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (isNewEntry || textBox1.Text == "0")
            {
                textBox1.Text = button.Text;
                isNewEntry = false;
            }
            else
            {
                textBox1.Text += button.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string newOperation = button.Text;

            if (currentOperation != "")
            {
                ExecuteOperation();
            }
            else
            {
                currentResult = double.Parse(textBox1.Text);
            }

            currentOperation = newOperation;
            isNewEntry = true;
        }

        private void ExecuteOperation()
        {
            double newNumber = double.Parse(textBox1.Text);

            switch (currentOperation)
            {
                case "+":
                    currentResult += newNumber;
                    break;
                case "-":
                    currentResult -= newNumber;
                    break;
                case "*":
                    currentResult *= newNumber;
                    break;
                case "/":
                    if (newNumber != 0)
                    {
                        currentResult /= newNumber;
                    }
                    else
                    {
                        MessageBox.Show("������: ������� �� ����");
                        return;
                    }
                    break;
            }

            textBox1.Text = currentResult.ToString();
            currentOperation = "";
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            ExecuteOperation();
            isNewEntry = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            currentResult = 0;
            currentOperation = "";
            textBox1.Text = "0";
            isNewEntry = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveResult();
        }

        private void SaveResult()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("calculator_result.txt", true))
            {
                file.WriteLine("���������: " + currentResult);
            }
            MessageBox.Show("��������� ��������.");
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadLastResult();
        }

        private void LoadLastResult()
        {
            if (System.IO.File.Exists("calculator_result.txt"))
            {
                string[] results = System.IO.File.ReadAllLines("calculator_result.txt");
                string lastResult = results[results.Length - 1];
                textBox1.Text = lastResult.Replace("���������: ", "");
                currentResult = double.Parse(textBox1.Text);
            }
            else
            {
                MessageBox.Show("���� � ������������ �� ������");
            }
        }

        private void ColorChangeButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ChangeCalculatorColor(colorDialog.Color);
                }
            }
        }

        private void ChangeCalculatorColor(System.Drawing.Color color)
        {
            // ��������� ���� �� ���� ������� � �����, ����� ���������� ����
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    control.BackColor = color;
                }
            }
            this.BackColor = color; // ��������� ���� �����
        }
    }
}
