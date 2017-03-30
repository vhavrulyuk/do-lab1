using System;
using System.Windows.Forms;
using SimplexMethodNS;

namespace lr1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int _limitationsCount, _variablesCount;
        private double[] _freeMembersValues;
        private double[,] _tempMainArr;
        private double[] _goalCoeficientsArray;

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lCount.Enabled = false;
            xCount.Enabled = false;
            _limitationsCount = (int) lCount.Value;
            _variablesCount = (int) xCount.Value;
            SimplexMethod.AddInterfaceElements(_limitationsCount, _variablesCount, limitationsGB);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void xCount_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] table =
            {
                {40, 2, 1},
                {65, 3, 2},
                {80, 4, 2},
                {0, -25, -15}
            };
            double[] result = new double[2];
            SimplexMethod s = new SimplexMethod(table);
            s.Calculate(result);
            printResults(result);
        }

        private double[,] CreateFirstStFromInput()
        {
            int firstCTableRowsCount = _tempMainArr.GetLength(0) + 1;
            int firstCTableColsCount = _tempMainArr.GetLength(1) + 1;
            Console.WriteLine(firstCTableRowsCount + @" " + firstCTableColsCount);
            double[,] firstCTable = new double[firstCTableRowsCount, firstCTableColsCount];
            
            for (int i = 0; i < firstCTableRowsCount - 1; i++)
                firstCTable[i, 0] = _freeMembersValues[i];

            firstCTable[firstCTableRowsCount-1, 0] = 0;

            for (int i=0;i<firstCTableRowsCount-1;i++)
                for (int j = 1; j < firstCTableColsCount; j++)
                    firstCTable[i, j] = _tempMainArr[i, j-1];
            for (int j = 1; j < firstCTableColsCount; j++)
                    firstCTable[firstCTableRowsCount - 1, j] = -_goalCoeficientsArray[j-1];

            for (int i = 0; i < firstCTable.GetLength(0); i++)
            { for (int j = 0; j < firstCTable.GetLength(1); j++)
                    Console.Write(firstCTable[i,j]+@" ");
                Console.WriteLine();
            }

            return firstCTable;
        }

        private void printResults(double[] results)
        {
            Console.WriteLine(@"Рішення: ");
            for (int i=0;i<results.Length;i++)
                Console.Write(@"X"+(i+1).ToString()+@" = "+results[i] + Environment.NewLine);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            _tempMainArr = SimplexMethod.GetValuesOfLimitationCoeficients();
            _freeMembersValues = SimplexMethod.GetFreeMembersValues();
            _goalCoeficientsArray = SimplexMethod.GetValuesOfGoalFuncCoeficients();
            double[,] table =  CreateFirstStFromInput();
            SimplexMethod s = new SimplexMethod(table);
            double[] tableResult = new double[s.Table.GetLength(1)-1];
            s.Calculate(tableResult);
            printResults(tableResult);
        }
    }
}
