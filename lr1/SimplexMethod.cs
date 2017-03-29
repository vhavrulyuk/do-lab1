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

        private static TextBox[,] data;
        private static TextBox[] freeMembers;

        public static void addInterfaceElements(int limCount, int xCount, GroupBox limitationsGB)
        {
            addMainVarInputs(limCount, xCount, limitationsGB);
            addGoalFuncInputs(xCount, limitationsGB);
        }

        private static void addMainVarInputs(int limCount, int xCount, GroupBox limitationsGB)
        {
            Label mainVarLabel = new Label();
            mainVarLabel.Text = "Основні змінні";
            mainVarLabel.Location = new Point(10, 75);
            limitationsGB.Controls.Add(mainVarLabel);
            data = new TextBox[limCount, xCount];
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
                    data[i, j] = new TextBox();
                    data[i, j].Name = "textbox_input_coef_" + i.ToString() + "_" + j.ToString();
                    data[i, j].Width = 40;
                    data[i, j].Height = 32;
                    data[i, j].Location = new Point(j*xOffset + 10, i*yOffset + 100);
                    labels[i, j] = new Label();
                    labels[i, j].Width = 20;
                    labels[i, j].Height = 32;
                    labels[i, j].Text = "X" + (i + 1).ToString();
                    //labels[i,j].ForeColor = Color.Black;
                    labels[i, j].Location = new Point(data[i, j].Location.X + 45, data[i, j].Location.Y);
                    limitationsGB.Controls.Add(data[i, j]);
                    limitationsGB.Controls.Add(labels[i, j]);
                }
            }
            addFreeMembersInputs(limCount, limitationsGB, (j*xOffset + 30));
        }

        private static void addGoalFuncInputs(int xCount, GroupBox limitationsGB)
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
                limitationsGB.Controls.Add(goal[i]);
            }
            Label goaLabel = new Label();
            goaLabel.Text = "-> MAX";
            goaLabel.Location = new Point(i*xOffset + 10, y);
            limitationsGB.Controls.Add(goaLabel);
        }

        private static void addFreeMembersInputs(int limCount, GroupBox limitationsGB, int startXPos)
        {
            freeMembers = new TextBox[limCount];
            Label[] equalityLabels = new Label[limCount];
            int y = 50;
            int yOffset = 50;
            int i;
            for (i = 0; i < limCount; i++)
            {
                freeMembers[i] = new TextBox();
                freeMembers[i].Name = "textbox_input_goal_" + i.ToString();
                freeMembers[i].Width = 40;
                freeMembers[i].Height = 32;
                freeMembers[i].Location = new Point(startXPos, i*yOffset + 100);
                equalityLabels[i] = new Label();
                equalityLabels[i].Width = 20;
                equalityLabels[i].Height = 32;
                equalityLabels[i].Text = "<=";
                //labels[i,j].ForeColor = Color.Black;
                equalityLabels[i].Location = new Point(freeMembers[i].Location.X - 25, freeMembers[i].Location.Y);
                limitationsGB.Controls.Add(freeMembers[i]);
                limitationsGB.Controls.Add(equalityLabels[i]);
            }
        }

        public static int[,] formAdditionalVarsArray(int limCount)
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

        public static int[,] formMatrixOfCoefcients(int[,] mainV, int[,] additionalV)
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

        public static int[,] getValuesOfLimitationCoeficients()
        {
            int dataRowsCount = data.GetLength(0);
            int dataColCount = data.GetLength(1);
            int[,] limitationCoeficients = new int[dataRowsCount, dataColCount];
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    Int32.TryParse(data[i, j].Text, out limitationCoeficients[i, j]);
            return limitationCoeficients;
        }

        public static int[] getFreeMembersValues()
        {
            int[] freeMemebersValues = new int[freeMembers.Length];
            for (int i = 0; i < freeMembers.Length; i++)
                Int32.TryParse(freeMembers[i].Text, out freeMemebersValues[i]);
            return freeMemebersValues;
        }

        private static void writeToFile(string s)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\solution.txt", true);
            file.WriteLine(s);
            file.Close();
        }

        public static void printExtendedSystemToFile(int[,] coeficients, int[] freeMembers)
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
            writeToFile(lines);
        }

        public static void printCoeficientsMatrix(int[,] cM)
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

            writeToFile(lines);
        }

        public static string[] formBasis(int[,] cM)
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

        public static void printSimplexTable(string[] basis)
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
        double[,] table;
        int m, n;
        List<int> basis; //список базисних змінних
        int simplexTanleNumber = 1;

        public SimplexMethod(double[,] source)
        {
            m = source.GetLength(0);
            n = source.GetLength(1);
            table = new double[m, n + m - 1];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n) table[i, j] = source[i, j];
                    else table[i, j] = 0;
                    //Виставляємо коефіцієнт 1 перед базисною змінною
                    if ((n + i) < table.GetLength(1))
                    {
                        table[i, n + i] = 1;
                        basis.Add(n + 1);
                    }
                }
            n = table.GetLength(1);
        }

        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; //Провідні стовпчик та рядок
            printSimplexTable(simplexTanleNumber);
            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = table[mainRow, j]/table[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;
                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol]*new_table[mainRow, j];
                }
                table = new_table;

                printSimplexTable(++simplexTanleNumber);
                

            }

            //Заносимо в result знайдені значення x
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }
            return table;
            }

        private void printSimplexTable(int number)
            {
            Console.WriteLine("Симплекс таблиця №" + number.ToString());
            for (int i = 0; i<table.GetLength(0);i++)
                    for (int j = 0; j<table.GetLength(1); j++)
                    {
                        Console.Write(table[i, j]+" ");
                        if (j==table.GetLength(1)-1) Console.WriteLine();
                    }
            Console.WriteLine();
            }
        
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private int findMainCol()
        {
            int mainCol = 1;

            for(int j=2;j<n;j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i=0;i<m-1;i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }
    }
}

