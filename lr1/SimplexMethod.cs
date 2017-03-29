using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
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
            addGoalFuncInputs(xCount,limitationsGB);
        }

        private static void addMainVarInputs(int limCount, int xCount, GroupBox limitationsGB)
        {
            Label mainVarLabel = new Label();
            mainVarLabel.Text = "Основні змінні";
            mainVarLabel.Location = new Point(10,75);
            limitationsGB.Controls.Add(mainVarLabel);
            data = new TextBox[limCount, xCount];
            Label[,] labels = new Label[limCount,xCount];
            int x = 50;
            int y = 50;
            int xOffset = 70;
            int yOffset = 50;
            int j=0;
            for (int i = 0; i < limCount; i++)
            {
                for (j = 0; j < xCount; j++)
                {
                    data[i, j] = new TextBox();
                    data[i, j].Name = "textbox_input_coef_" + i.ToString() + "_" + j.ToString();
                    data[i, j].Width = 40;
                    data[i, j].Height = 32;
                    data[i, j].Location = new Point(j * xOffset + 10, i * yOffset + 100);
                    labels[i, j] = new Label();
                    labels[i, j].Width = 20;
                    labels[i, j].Height = 32;
                    labels[i, j].Text = "X" + (i+1).ToString();
                    //labels[i,j].ForeColor = Color.Black;
                    labels[i,j].Location = new Point(data[i, j].Location.X+45, data[i, j].Location.Y);
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
                goal[i].Location = new Point(i * xOffset + 10, y);
                limitationsGB.Controls.Add(goal[i]);
            }
            Label goaLabel = new Label();
            goaLabel.Text = "-> MAX";
            goaLabel.Location = new Point(i * xOffset+10,y);
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
                freeMembers[i].Location = new Point(startXPos, i * yOffset + 100);
                equalityLabels[i] = new Label();
                equalityLabels[i].Width = 20;
                equalityLabels[i].Height = 32;
                equalityLabels[i].Text = "<=";
                //labels[i,j].ForeColor = Color.Black;
                equalityLabels[i].Location = new Point(freeMembers[i].Location.X -25, freeMembers[i].Location.Y);
                limitationsGB.Controls.Add(freeMembers[i]);
                limitationsGB.Controls.Add(equalityLabels[i]);
            }
        }

        public static int[,] formAdditionalVarsArray(int limCount)
        {
            int[,] additionalVars = new int[limCount,limCount];
            for (int i = 0; i<limCount; i++)
            {
                for (int j = 0; j < limCount; j++)
                {
                    if (i == j) additionalVars[i, j] = 1;
                    else additionalVars[i, j] = 0;
                    Console.WriteLine( "additionalVars[" +i+ ","+j+"]= "+ additionalVars[i, j]);
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
            for (int i=0;i < mainVRows; i++)
                for (int j = 0; j < mainVCols; j++)
                {
                    coeficientsMatrix[i, j] = mainV[i, j];
                }
            for (int i = 0; i < mainVRows; i++)
                for (int j=mainVCols; j < coeficientsMatrix.GetLength(1); j++)
                {
                    coeficientsMatrix[i, j] = additionalV[i, j- mainVCols];
                }
            for (int i = 0; i < mainVRows; i++)
            {
                for (int j = 0; j < coeficientsMatrix.GetLength(1); j++)
                {
                    Console.WriteLine("CoeficientsMatrix[" + i + "," + j + "]= " + coeficientsMatrix[i, j]);
                }
            }
            return coeficientsMatrix;
        }

        public static int[,] getValuesOfLimitationCoeficients()
        {
            int dataRowsCount =  data.GetLength(0);
            int dataColCount = data.GetLength(1);
            int[,] limitationCoeficients = new int[dataRowsCount, dataColCount];
            for (int i=0;i<data.GetLength(0);i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    Int32.TryParse(data[i,j].Text, out limitationCoeficients[i, j]);
            return limitationCoeficients;
        }
        public static int[] getFreeMembersValues()
        {
            int[] freeMemebersValues = new int[freeMembers.Length];
            for (int i = 0; i < freeMembers.Length; i++)
                Int32.TryParse(freeMembers[i].Text, out freeMemebersValues[i]);
            return freeMemebersValues;
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
                    if (j<coeficients.GetLength(1)-1)lines += "+";
                }
                lines += "=";
                lines += freeMembers[i].ToString() + "\r\n";
            }

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\solution.txt", true);
            file.WriteLine(lines);
            file.Close();
        }
    }
}
