using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using lr1;


namespace SimplexMethodNS
{
    public class SimplexMethod
    {
        public SimplexMethod()
        {
        }

        private static TextBox[,] _data;
        private static TextBox[] _freeMembers;

        public static void AddInterfaceElements(int limCount, int xCount, GroupBox limitationsGb)
        {
            AddMainVarInputs(limCount, xCount, limitationsGb);
            AddGoalFuncInputs(xCount, limitationsGb);
        }

        private static void AddMainVarInputs(int limCount, int xCount, GroupBox limitationsGb)
        {
            Label mainVarLabel = new Label();
            mainVarLabel.Text = "Основні змінні";
            mainVarLabel.Location = new Point(10, 75);
            limitationsGb.Controls.Add(mainVarLabel);
            _data = new TextBox[limCount, xCount];
            Label[,] labels = new Label[limCount, xCount];
            int x = 50;
            int y = 50;
            int xOffset = 70;
            int yOffset = 50;
            int j = 0;
            for (int i = 0; i < limCount; i++)
            {
                for (j = 0; j < xCount; j++)
                {
                    _data[i, j] = new TextBox();
                    _data[i, j].Name = "textbox_input_coef_" + i.ToString() + "_" + j.ToString();
                    _data[i, j].Width = 40;
                    _data[i, j].Height = 32;
                    _data[i, j].Location = new Point(j*xOffset + 10, i*yOffset + 100);
                    labels[i, j] = new Label();
                    labels[i, j].Width = 20;
                    labels[i, j].Height = 32;
                    labels[i, j].Text = "X" + (i + 1).ToString();
                    //labels[i,j].ForeColor = Color.Black;
                    labels[i, j].Location = new Point(_data[i, j].Location.X + 45, _data[i, j].Location.Y);
                    limitationsGb.Controls.Add(_data[i, j]);
                    limitationsGb.Controls.Add(labels[i, j]);
                }
            }
            AddFreeMembersInputs(limCount, limitationsGb, (j*xOffset + 30));
        }

        private static void AddGoalFuncInputs(int xCount, GroupBox limitationsGb)
        {
            TextBox[] goal = new TextBox[xCount];
            int x = 20;
            int y = 50;
            int xOffset = 50;
            int i;
            for (i = 0; i < xCount; i++)
            {
                goal[i] = new TextBox();
                goal[i].Name = "textbox_input_goal_" + i.ToString();
                goal[i].Width = 40;
                goal[i].Height = 32;
                goal[i].Location = new Point(i*xOffset + 10, y);
                limitationsGb.Controls.Add(goal[i]);
            }
            Label goaLabel = new Label();
            goaLabel.Text = "-> MAX";
            goaLabel.Location = new Point(i*xOffset + 10, y);
            limitationsGb.Controls.Add(goaLabel);
        }

        private static void AddFreeMembersInputs(int limCount, GroupBox limitationsGb, int startXPos)
        {
            _freeMembers = new TextBox[limCount];
            Label[] equalityLabels = new Label[limCount];
            int y = 50;
            int yOffset = 50;
            int i;
            for (i = 0; i < limCount; i++)
            {
                _freeMembers[i] = new TextBox();
                _freeMembers[i].Name = "textbox_input_goal_" + i.ToString();
                _freeMembers[i].Width = 40;
                _freeMembers[i].Height = 32;
                _freeMembers[i].Location = new Point(startXPos, i*yOffset + 100);
                equalityLabels[i] = new Label();
                equalityLabels[i].Width = 20;
                equalityLabels[i].Height = 32;
                equalityLabels[i].Text = "<=";
                //labels[i,j].ForeColor = Color.Black;
                equalityLabels[i].Location = new Point(_freeMembers[i].Location.X - 25, _freeMembers[i].Location.Y);
                limitationsGb.Controls.Add(_freeMembers[i]);
                limitationsGb.Controls.Add(equalityLabels[i]);
            }
        }

        public static int[,] FormAdditionalVarsArray(int limCount)
        {
            int[,] additionalVars = new int[limCount, limCount];
            for (int i = 0; i < limCount; i++)
            {
                for (int j = 0; j < limCount; j++)
                {
                    if (i == j) additionalVars[i, j] = 1;
                    else additionalVars[i, j] = 0;
                    //Console.WriteLine( "additionalVars[" +i+ ","+j+"]= "+ additionalVars[i, j]);
                }
            }
            return additionalVars;
        }

        public static int[,] FormMatrixOfCoefcients(int[,] mainV, int[,] additionalV)
        {
            int mainVRows = mainV.GetLength(0);
            int mainVCols = mainV.GetLength(1);
            int additionalRCols = additionalV.GetLength(1);

            int[,] coeficientsMatrix = new int[mainVRows, mainVCols + additionalRCols];
            for (int i = 0; i < mainVRows; i++)
                for (int j = 0; j < mainVCols; j++)
                {
                    coeficientsMatrix[i, j] = mainV[i, j];
                }
            for (int i = 0; i < mainVRows; i++)
                for (int j = mainVCols; j < coeficientsMatrix.GetLength(1); j++)
                {
                    coeficientsMatrix[i, j] = additionalV[i, j - mainVCols];
                }
            //for (int i = 0; i < mainVRows; i++)
            //{
            //    for (int j = 0; j < coeficientsMatrix.GetLength(1); j++)
            //    {
            //        Console.WriteLine("CoeficientsMatrix[" + i + "," + j + "]= " + coeficientsMatrix[i, j]);
            //    }
            //}
            return coeficientsMatrix;
        }

        public static int[,] GetValuesOfLimitationCoeficients()
        {
            int dataRowsCount = _data.GetLength(0);
            int dataColCount = _data.GetLength(1);
            int[,] limitationCoeficients = new int[dataRowsCount, dataColCount];
            for (int i = 0; i < _data.GetLength(0); i++)
                for (int j = 0; j < _data.GetLength(1); j++)
                    Int32.TryParse(_data[i, j].Text, out limitationCoeficients[i, j]);
            return limitationCoeficients;
        }

        public static int[] GetFreeMembersValues()
        {
            int[] freeMemebersValues = new int[_freeMembers.Length];
            for (int i = 0; i < _freeMembers.Length; i++)
                Int32.TryParse(_freeMembers[i].Text, out freeMemebersValues[i]);
            return freeMemebersValues;
        }

        private static void WriteToFile(string s)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\solution.txt", true);
            file.WriteLine(s);
            file.Close();
        }

        public static void PrintExtendedSystemToFile(int[,] coeficients, int[] freeMembers)
        {
            // Compose a string that consists of three lines.
            string lines = "Розширена система рівнянь: \r\n";
            for (int i = 0; i < coeficients.GetLength(0); i++)
            {
                for (int j = 0; j < coeficients.GetLength(1); j++)
                {
                    lines += coeficients[i, j].ToString() + "X" + (j + 1).ToString();
                    if (j < coeficients.GetLength(1) - 1) lines += "+";
                }
                lines += "=";
                lines += freeMembers[i].ToString() + "\r\n";
            }

            // Write the string to a file.
            //System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\solution.txt", true);
            //file.WriteLine(lines);
            //file.Close();
            WriteToFile(lines);
        }

        public static void PrintCoeficientsMatrix(int[,] cM)
        {
            string lines = "Матриця коефіцієнтів: \r\n";
            for (int i = 0; i < cM.GetLength(0); i++)
            {
                for (int j = 0; j < cM.GetLength(1); j++)
                {
                    lines += cM[i, j] + " ";
                }
                lines += "\r\n";
            }

            WriteToFile(lines);
        }

        public static string[] FormBasis(int[,] cM)
        {
            int cMRowsCount = cM.GetLength(0);
            int cMColsCount = cM.GetLength(1);
            string[] basis = new string[cMColsCount];
            int basisLength = basis.Length;
            for (int i = 0; i < cMRowsCount; i++)
            {
                for (int j = cMColsCount - cMRowsCount; j < cMColsCount; j++)
                {
                    if (cM[i, j] != 0)
                        basis[i] = "X" + (j + 1).ToString();
                }
            }
            return basis;
        }

        public static void PrintSimplexTable(string[] basis)
        {
            string lines = "СТ\r\n";
            char[] linesArray;
            for (int i = 0; i < basis.Length; i++)
            {
                lines += basis[i] + "\r\n";
            }
            //Console.WriteLine(lines);
            //linesArray = lines.ToCharArray();
            //for (int i = 0; i < linesArray.Length; i++)
            //    Console.WriteLine(linesArray[i]);
            //lines = new string(linesArray);
            //Console.WriteLine(lines);
        }

        //source - Симплекс таблиця без базисих змінних
        double[,] _table;
        int _m, _n;
        List<int> _basis; //список базисних змінних
        int _simplexTanleNumber = 1;

        public SimplexMethod(double[,] source)
        {
            _m = source.GetLength(0);
            _n = source.GetLength(1);
            _table = new double[_m, _n + _m - 1];
            _basis = new List<int>();

            for (int i = 0; i < _m; i++)
                for (int j = 0; j < _table.GetLength(1); j++)
                {
                    if (j < _n) _table[i, j] = source[i, j];
                    else _table[i, j] = 0;
                    //Виставляємо коефіцієнт 1 перед базисною змінною
                    if ((_n + i) < _table.GetLength(1))
                    {
                        _table[i, _n + i] = 1;
                        _basis.Add(_n + 1);
                    }
                }
            _n = _table.GetLength(1);
        }

        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; //Провідні стовпчик та рядок
            PrintSimplexTable(_simplexTanleNumber);
            while (!IsItEnd())
            {
                mainCol = FindMainCol();
                mainRow = FindMainRow(mainCol);
                _basis[mainRow] = mainCol;

                double[,] newTable = new double[_m, _n];

                for (int j = 0; j < _n; j++)
                    newTable[mainRow, j] = _table[mainRow, j]/_table[mainRow, mainCol];

                for (int i = 0; i < _m; i++)
                {
                    if (i == mainRow)
                        continue;
                    for (int j = 0; j < _n; j++)
                        newTable[i, j] = _table[i, j] - _table[i, mainCol]*newTable[mainRow, j];
                }
                _table = newTable;

                PrintSimplexTable(++_simplexTanleNumber);
                

            }

            //Заносимо в result знайдені значення x
            for (int i = 0; i < result.Length; i++)
            {
                int k = _basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = _table[k, 0];
                else
                    result[i] = 0;
            }
            return _table;
            }

        private void PrintSimplexTable(int number)
            {
            Console.WriteLine("Симплекс таблиця №" + number.ToString());
            for (int i = 0; i<_table.GetLength(0);i++)
                    for (int j = 0; j<_table.GetLength(1); j++)
                    {
                        Console.Write(_table[i, j]+" ");
                        if (j==_table.GetLength(1)-1) Console.WriteLine();
                    }
            Console.WriteLine();
            }
        
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < _n; j++)
            {
                if (_table[_m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private int FindMainCol()
        {
            int mainCol = 1;

            for(int j=2;j<_n;j++)
                if (_table[_m - 1, j] < _table[_m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int FindMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i=0;i<_m-1;i++)
                if (_table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            for (int i = mainRow + 1; i < _m - 1; i++)
                if ((_table[i, mainCol] > 0) && ((_table[i, 0] / _table[i, mainCol]) < (_table[mainRow, 0] / _table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }
    }
}

