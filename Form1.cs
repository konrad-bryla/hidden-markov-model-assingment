//Konrad Bryla
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Statistics.Models.Markov;

namespace HMM_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int HEALTHY = 0;
        public const int SICK = 1;

        public const int NEGATIVE = 0;
        public const int POSITIVE = 1;

        double[] initial = { 0.20, 0.80 };

        double[,] sensorModel =
        {
           /*they are not sick next day*/ { 0.5, 0.95 },
           /*they are sick next day*/{ 0.90, 0.10 }
        };

        double[,] transitionModel =
        {
            /*sick + positive*/{ 0.75, 0.25 },
            /*not sick + positive*/{ 0.15, 0.85 }
        };

        private void btnDoStuff_Click(object sender, EventArgs e)
        {
            HiddenMarkovModel hmm = new HiddenMarkovModel(transitionModel, sensorModel, initial);
            char[] input = inputTxtBox.Text.ToCharArray();
            int[] observations = new int[input.Length];

            int i = 0;
            foreach (char c in input)
            {
                if (c.Equals('n') || c.Equals('N'))
                {
                    observations[i] = NEGATIVE;
                    i++;
                }else if (c.Equals('p') || c.Equals('P'))
                {
                    observations[i] = POSITIVE;
                    i++;
                }
                else{ return; }
            }
   

            int[] path = hmm.Decide(observations);

            string output = "";
            foreach (int guess in path)
            {
                Console.WriteLine(guess == 1 ? "Healthy" : "Sick");
                
                
                output += guess == 1 ? "Healthy" + "\n" : "Sick" + "\n";
                
            }
            MessageBox.Show(output);
        }

        
    }
}
