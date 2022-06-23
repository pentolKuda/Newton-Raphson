using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Newton_Rhapson
{
    public partial class Form1 : Form
    {
        int matrixSize;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                matrixSize = int.Parse(tbInputNumber.Text);
            }
            catch
            {
                MessageBox.Show("Masukkan nilai dengan benar!");
                return;
            }
            tbInputNumber.Text = "";

            label6.Text = matrixSize.ToString();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            //pertanyaan
            for (int i = 0; i <= matrixSize; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderText = (i == matrixSize ? "C" : "X^" + (matrixSize - i).ToString());
                column.CellTemplate = new DataGridViewTextBoxCell();
                column.Width = 30;
                dataGridView1.Columns.Add(column);


            }
            dataGridView1.Rows.Add();

            String[] captionDataGrid = {"Enter Guess", "Tolerable Error", "Max Step"};

            for (int i = 0; i < 3; i++)
            {
                var col_guess = new DataGridViewColumn();
                col_guess.HeaderText = captionDataGrid[i];
                col_guess.CellTemplate = new DataGridViewTextBoxCell();
                col_guess.Width = 80;
                dataGridView2.Columns.Add(col_guess);
            }
            dataGridView2.Rows.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get dataGrid2 data
            double guessNumber = double.Parse((string)dataGridView2.Rows[0].Cells[0].Value);
            double tolerableError = double.Parse((string)dataGridView2.Rows[0].Cells[1].Value);
            int maxStep = int.Parse((string)dataGridView2.Rows[0].Cells[2].Value);

            //get polinom function
            int pwr = matrixSize;
            double[] func_coef = new double[pwr+1];
            double[] derr_func_coef = new double[pwr];
            for (int i = (pwr+1); i > 0; i--)
            {
                //Console.Write(i);
                func_coef[i-1] = double.Parse((string)dataGridView1.Rows[0].Cells[(pwr-i+1)].Value);//int.Parse(Console.ReadLine());
                Console.Write(func_coef[i-1]);
                Console.Write("\t");

                if (i != (pwr+1))
                {
                    derr_func_coef[i-1] = func_coef[i] * (i);
                    Console.Write(derr_func_coef[i-1]);
                }
                Console.WriteLine();
            }


            newton_rhapson(func_coef, derr_func_coef, guessNumber, tolerableError, maxStep);



        }

        private double functionVal(double[] mat_coef, double x)
        {
            double ret = 0;
            for(int i = 0; i < mat_coef.GetLength(0); i++){
                ret += mat_coef[i] * Math.Pow(x, i); 
            }
            return ret;
        }

        private void newton_rhapson(double[] _coefficient, double[] _derrivativeCoefficient, double _guessNumber, double _tolerableError, int _maxStep)
        {
            int step = 1;
            bool convergent = true;
            while (true)
            {
                
                if(functionVal(_derrivativeCoefficient, _guessNumber) == 0)
                {
                    Console.WriteLine("Divide by Zero");
                    break;
                }
                double newGuessNumber = _guessNumber - (functionVal(_coefficient, _guessNumber) / functionVal(_derrivativeCoefficient, _guessNumber));
                String list_box_msg = "iteration-" + step.ToString() + ", x = " + _guessNumber.ToString() + " and f(x0) = " + functionVal(_coefficient, _guessNumber).ToString();
                Console.WriteLine(list_box_msg);
                listBox1.Items.Add(list_box_msg);
                _guessNumber = newGuessNumber;
                step++;

                if (step > _maxStep)
                {
                    convergent = false;
                }

                if(Math.Abs(functionVal(_coefficient, _guessNumber)) < _tolerableError)
                {
                    break;
                }


            }

            if (convergent)
            {
                Console.WriteLine("\n The Root is {0}", _guessNumber);
                label1.Text = "The Root is " + _guessNumber;
            }
            else
            {
                Console.WriteLine("not convergent");
                label1.Text = "Not Convergent";
            }


        }


    }
}
