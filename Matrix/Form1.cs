using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Matrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Бірінші матрицага жол санын енгізу
        private void numCountRows_ValueChanged(object sender, EventArgs e)
        {
            matrixOne.RowCount = (int)numCountRows.Value;
        }

        // Бірінші матрицага баған санын енгізу
        private void numCountCollum_ValueChanged(object sender, EventArgs e)
        {
            matrixOne.ColumnCount = (int)numCountCollum.Value;
        }

        // Екінші матрицага жол санын енгізу
        private void numRowsCountM2_ValueChanged(object sender, EventArgs e)
        {
            matrixTwo.RowCount = (int)numRowsCountM2.Value;
        }

        // Екінші матрицага баған санын енгізу
        private void numCollCountM2_ValueChanged(object sender, EventArgs e)
        {
            matrixTwo.ColumnCount = (int)numCollCountM2.Value;
        }


        // Бастапқыда әр матрицаның өлшемі 1х1 болады
        private void Form1_Load(object sender, EventArgs e)
        {
            matrixOne.RowCount = 1;
            matrixOne.ColumnCount = 1;
            matrixTwo.RowCount = 1;
            matrixTwo.ColumnCount = 1;
        }

        // Матрица элементин озгерткенде орындалатын окига
        private void matrixOne_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }

      // Озгертилип басталганда осы окига орындалады
        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != '-') && (e.KeyChar != ',')))
            {
                if ((e.KeyChar != (char)Keys.Back) || (e.KeyChar != (char)Keys.Delete))
                { e.Handled = true; }
            }

        }

        // Бириншы матрицаны кездейсок сандармен толтыру
        private void button1_Click(object sender, EventArgs e)
        {
            randomMatrix(matrixOne);
        }

        // Бириншы матрицаны тазарту окигасы(событие)
        private void button3_Click(object sender, EventArgs e)
        {
            clearMatrix(matrixOne);
        }

       // Берилген матрицаны кездейсок сандармен толтыру функциясы
        private void randomMatrix(DataGridView dg)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            
            int collCount = dg.ColumnCount;
            int rowCount = dg.RowCount;
            for (int i = 0; i < collCount; i++)
                for (int j = 0; j < rowCount; j++)
                {
                    dg.Rows[j].Cells[i].Value = rand.Next(1, 4);
                }
        }

        // Берилген матрицаны тазарту функциясы
        private void clearMatrix(DataGridView dg)
        {
            
            int collCount = dg.ColumnCount;
            int rowCount = dg.RowCount;
            
            for (int i = 0; i < collCount; i++)
                for (int j = 0; j < rowCount; j++)
                {
                    dg.Rows[j].Cells[i].Value = 0;
                }
        }

        // Екиншы матрицаны кездейсок сандармен толтыру окигасы(событие)
        private void button6_Click(object sender, EventArgs e)
        {
            randomMatrix(matrixTwo);
        }

        // Екиншы матрицаны тазарту окигасы(событие)
        private void button4_Click(object sender, EventArgs e)
        {
            clearMatrix(matrixTwo);
        }

        // Еки матрицаны косу окигасы(событие)
        private void bPlus_Click(object sender, EventArgs e)
        {
            if (summMatrix(matrixOne, matrixTwo) == null) return;
            Result f = new Result(summMatrix(matrixOne, matrixTwo), "қосу");
            f.ShowDialog();
        }

        // Еки матрицаны косу функциясы
        private DataGridView summMatrix(DataGridView matr1, DataGridView matr2)
        {
            
            if (matr1.RowCount != matr2.RowCount)
            {
                MessageBox.Show("Қосу үшін матрицалардың өлшемі бірдей болуы керек!");
                return null;
            }




            
            int collCount = matr1.ColumnCount;
            int rowCount = matr1.RowCount;
            DataGridView dg = new DataGridView();
            dg.RowCount = rowCount;
            dg.ColumnCount = collCount;
            for (int i = 0; i < collCount; i++)
                for (int j = 0; j < rowCount; j++)
                    dg.Rows[j].Cells[i].Value = Convert.ToDouble(matr1.Rows[j].Cells[i].Value) + Convert.ToDouble(matr2.Rows[j].Cells[i].Value);
            return dg;
        }

        // Еки матрицаны кобейту функциясы
        private DataGridView multMatrix(DataGridView matr1, DataGridView matr2)
        {
            
            if (matr1.ColumnCount != matr2.RowCount)
            {
                MessageBox.Show("Бірінші матрицаның бағандарының саны екінші матрицаның жолдарының санына тең болуы керек");
                return null;
            }

            
            int collCount = matr2.ColumnCount;
            int rowCount = matr1.RowCount;
            int rCount = matr1.ColumnCount;
            DataGridView dg = new DataGridView();
            dg.RowCount = rowCount;
            dg.ColumnCount = collCount;
            for (int j = 0; j < collCount; j++)
                for (int i = 0; i < rowCount; i++)
                {
                    double temp = 0;
                    for (int r = 0; r < rCount; r++)
                        temp += Convert.ToDouble(matr1.Rows[j].Cells[r].Value) * Convert.ToDouble(matr2.Rows[r].Cells[i].Value);
                    dg.Rows[j].Cells[i].Value = temp;
                }
            return dg;
        }

        // Еки матрицаны кобейту окигасы(событие)
        private void bMul_Click(object sender, EventArgs e)
        {
            if (multMatrix(matrixOne, matrixTwo) == null) return;
            Result f = new Result(multMatrix(matrixOne, matrixTwo), "кобейту");
            f.ShowDialog();
        }

        // Матрицадан детерминант табу окигасы(событие)
        private void button2_Click(object sender, EventArgs e)
        {
            int collCount = matrixOne.ColumnCount;
            int rowCount = matrixOne.RowCount;
            
            if (collCount != rowCount)
            {
                MessageBox.Show("Детерминантты тек квадрат матрицалар үшін табуға болады!");
                return;
            }
            
            double[,] matr = new double[collCount, rowCount];
            for (int i = 0; i < collCount; i++)
                for (int j = 0; j < rowCount; j++)
                    matr[i, j] = Convert.ToDouble(matrixOne.Rows[j].Cells[i].Value);
            MessageBox.Show("Детерминант тең: " + matrixDeterminant(matr, collCount).ToString());
        }

        // Минорын табу функциясы
        private double[,] getMinor(double[,] matrix, int m, int i, int j)
        {
            int di = 0;
            double[,] b = new double[m, m];
            for (int ki = 0; ki < m - 1; ki++)
            {
                if (ki == i) di = 1;
                int dj = 0;
                for (int kj = 0; kj < m - 1; kj++)
                {
                    if (kj == j) dj = 1;
                    b[ki, kj] = matrix[ki + di, kj + dj];
                }
            }
            return b;
        }

        // Детерминант табу функциясы
        private double matrixDeterminant(double[,] matrix, int n)
        {
            double[,] b = new double[n, n];
            double d = 0, k = 1;
            if (n < 1) return 0;
            if (n == 1) d = matrix[0, 0]; 
            else if (n == 2) 
            {
                d = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            }
            else 
                for (int i = 0; i < n; i++)
                {
                    b = getMinor(matrix, n, i, 0);
                    d += k * matrix[i, 0] * matrixDeterminant(b, n - 1);
                    k = -k;
                }
            return d;
        }


        // Матрицаларды азайту окигасы(событие)
        private void button5_Click(object sender, EventArgs e)
        {
            if (subMatrix(matrixOne, matrixTwo) == null) return;
            Result f = new Result(subMatrix(matrixOne, matrixTwo), "азайту");
            f.ShowDialog();
        }


        // Азайту функциясы
        private DataGridView subMatrix(DataGridView matr1, DataGridView matr2)
        {
            
            if (matr1.RowCount != matr2.RowCount)
            {
                MessageBox.Show("Азайту үшін матрицалардың өлшемі бірдей болуы керек!");
                return null;
            }




            
            int collCount = matr1.ColumnCount;
            int rowCount = matr1.RowCount;
            DataGridView dg = new DataGridView();
            dg.RowCount = rowCount;
            dg.ColumnCount = collCount;
            for (int i = 0; i < collCount; i++)
                for (int j = 0; j < rowCount; j++)
                    dg.Rows[j].Cells[i].Value = Convert.ToDouble(matr1.Rows[j].Cells[i].Value) - Convert.ToDouble(matr2.Rows[j].Cells[i].Value);
            return dg;
        }

       
    }
    }


