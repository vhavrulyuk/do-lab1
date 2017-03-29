using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private int limitationsCount, variablesCount;

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lCount.Enabled = false;
            xCount.Enabled = false;
            limitationsCount = (int) lCount.Value;
            variablesCount = (int) xCount.Value;
            SimplexMethod.addInterfaceElements(limitationsCount, variablesCount, limitationsGB);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void xCount_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {  
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //TO DO read values of limitationCoeficents
            int[,] tempMainArr = SimplexMethod.getValuesOfLimitationCoeficients();
            int[,] tempAddArr = SimplexMethod.formAdditionalVarsArray(limitationsCount);
            int[,] AwithStartingBasicSoulutionLook = SimplexMethod.formMatrixOfCoefcients(tempMainArr,tempAddArr);
            int[] freeMembersValues = SimplexMethod.getFreeMembersValues();
            SimplexMethod.printExtendedSystemToFile(AwithStartingBasicSoulutionLook,freeMembersValues);
        }
    }
}
