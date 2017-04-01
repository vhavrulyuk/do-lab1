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
        private static TextBox[] _goal;


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
                    labels[i, j].Text = "X" + (j + 1).ToString();
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
            _goal = new TextBox[xCount];
            int x = 20;
            int y = 50;
            int xOffset = 50;
            int i;
            for (i = 0; i < xCount; i++)
            {
                _goal[i] = new TextBox();
                _goal[i].Name = "textbox_input_goal_" + i.ToString();
                _goal[i].Width = 40;
                _goal[i].Height = 32;
                _goal[i].Location = new Point(i*xOffset + 10, y);
                limitationsGb.Controls.Add(_goal[i]);
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

        public static double[,] FormAdditionalVarsArray(int limCount)
        {
            double[,] additionalVars = new double[limCount, limCount];
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

        public static double[,] FormMatrixOfCoefcients(double[,] mainV, double[,] additionalV)
        {
            int mainVRows = mainV.GetLength(0);
            int mainVCols = mainV.GetLength(1);
            int additionalRCols = additionalV.GetLength(1);

            double[,] coeficientsMatrix = new double[mainVRows, mainVCols + additionalRCols];
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

        public static double[,] GetValuesOfLimitationCoeficients()
        {
            int dataRowsCount = _data.GetLength(0);
            int dataColCount = _data.GetLength(1);
            double[,] limitationCoeficients = new double[dataRowsCount, dataColCount];
            for (int i = 0; i < _data.GetLength(0); i++)
                for (int j = 0; j < _data.GetLength(1); j++)
                   Double.TryParse(_data[i, j].Text, out limitationCoeficients[i, j]);
            return limitationCoeficients;
        }

        public static double[] GetValuesOfGoalFuncCoeficients()
        {
            int goalLength = _goal.Length;
            double[] goalCoeficients = new double[goalLength];
            for (int i = 0; i < goalLength; i++)
                Double.TryParse(_goal[i].Text, out goalCoeficients[i]);
            return goalCoeficients;
        }

        public static double[] GetFreeMembersValues()
        {
            double[] freeMemebersValues = new double[_freeMembers.Length];
            for (int i = 0; i < _freeMembers.Length; i++)
                Double.TryParse(_freeMembers[i].Text, out freeMemebersValues[i]);
            return freeMemebersValues;
        }

        private static void WriteToFile(string s)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\solution.txt", true);
            file.WriteLine(s);
            file.Close();
        }

        //source - Симплекс таблиця без базисих змінних
        private double[,] table;
        int _m, _n;
        List<int> _basis; //список базисних змінних
        int _simplexTanleNumber = 1;

        internal double[,] Table
        {
            get
            {
                return table;
            }

            set
            {
                table = value;
            }
        }

        public SimplexMethod(double[,] source)
        {
            _m = source.GetLength(0);
            _n = source.GetLength(1);
            Table = new double[_m, _n + _m - 1];
            _basis = new List<int>();

            for (int i = 0; i < _m; i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (j < _n) Table[i, j] = source[i, j];
                    else Table[i, j] = 0;
                }
            //Виставляємо коефіцієнт 1 перед базисною змінною
                    if ((_n + i) < Table.GetLength(1))
                    {
                        Table[i, _n + i] = 1;
                        _basis.Add(_n + i);
                    }
                }
            _n = Table.GetLength(1);
           }

        public double[,] Calculate(double[] result, bool stepByStep)
        {
            int mainCol, mainRow; //Провідні стовпчик та рядок
            PrintSimplexTable(_simplexTanleNumber, stepByStep);
            while (!IsItEnd())
            {
                mainCol = FindMainCol();
                mainRow = FindMainRow(mainCol);
                _basis[mainRow] = mainCol;

                double[,] newTable = new double[_m, _n];

                for (int j = 0; j < _n; j++)
                {
                    newTable[mainRow, j] = Table[mainRow, j]/Table[mainRow, mainCol];
                    //if (stepByStep) Console.WriteLine(newTable[mainRow,j]);
                }
                
                for (int i = 0; i < _m; i++)
                {
                    if (i == mainRow)
                        continue;
                    for (int j = 0; j < _n; j++)
                    {
                        newTable[i, j] = Table[i, j] - Table[i, mainCol]*newTable[mainRow, j];
                        //if (stepByStep) Console.WriteLine(newTable[i, j]);
                    }

                }

                Table = newTable;

                checkWhetheOptimalPlanExists(Table);
                PrintSimplexTable(++_simplexTanleNumber, stepByStep);
                }

            //Заносимо в result знайдені значення x
            for (int i = 0; i < result.Length; i++)
            {
                int k = _basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = Table[k, 0];
                else
                    result[i] = 0;
            }

            return Table;
            }

        void checkWhetheOptimalPlanExists(double[,] simplexTable)
        {
            int coeficientsCount = simplexTable.Length - simplexTable.GetLength(0);
            int negativeCoeficientsCount = 0;
            for (int i=0;i<simplexTable.GetLength(0);i++)
                for (int j =1;j<simplexTable.GetLength(1);j++)
                    if (simplexTable[i, j] < 0) negativeCoeficientsCount++;
            if (coeficientsCount == negativeCoeficientsCount)
            Console.WriteLine("Функція мети еобмежено зростає. Кінець. Задачу некоректно сформульовано.");
        }

        private void PrintSimplexTable(int number, bool stepByStep)
            {
            Console.WriteLine("Симплекс таблиця №" + number.ToString());
            for (int i = 0; i<Table.GetLength(0);i++)
                    for (int j = 0; j<Table.GetLength(1); j++)
                    {
                        Console.Write(Table[i, j]+" ");
                    //if (stepByStep) Console.ReadLine();
                    if (j==Table.GetLength(1)-1) Console.WriteLine();
                        
                    }
            Console.WriteLine();
            }
        
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < _n; j++)
            {
                if (Table[_m - 1, j] < 0)
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
                if (Table[_m - 1, j] < Table[_m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int FindMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i=0;i<_m-1;i++)
                if (Table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            for (int i = mainRow + 1; i < _m - 1; i++)
                if ((Table[i, mainCol] > 0) && ((Table[i, 0] / Table[i, mainCol]) < (Table[mainRow, 0] / Table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }
    }
}

