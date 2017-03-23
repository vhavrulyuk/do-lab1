using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            int x = 50;
            int y = 50;
            int xOffset = 50;
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
                    limitationsGB.Controls.Add(data[i, j]);
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
            TextBox[] freeMembers = new TextBox[limCount];
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
                limitationsGB.Controls.Add(freeMembers[i]);
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

        public static int[,] formMatrixOfLimitationCoefcients(int[,] mainV, int[,] additionalV)
        {
            int mainVRows = mainV.GetLength(0);
            int mainVCols = mainV.GetLength(1);
            int additionalRCols = additionalV.GetLength(1);
            
            int[,] limitationCoeficients = new int[mainVRows, mainVCols + additionalRCols];
            for (int i=0;i < mainVRows; i++)
                for (int j = 0; j < mainVCols; j++)
                {
                    limitationCoeficients[i, j] = mainV[i, j];
                }
            for (int i = 0; i < mainVRows; i++)
                for (int j=mainVCols; j < limitationCoeficients.GetLength(1); j++)
                {
                    limitationCoeficients[i, j] = additionalV[i, j- mainVCols];
                }
            for (int i = 0; i < mainVRows; i++)
            {
                for (int j = 0; j < limitationCoeficients.GetLength(1); j++)
                {
                    Console.WriteLine("limitationCoeficients[" + i + "," + j + "]= " + limitationCoeficients[i, j]);
                }
            }
            return limitationCoeficients;
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
    }
}
