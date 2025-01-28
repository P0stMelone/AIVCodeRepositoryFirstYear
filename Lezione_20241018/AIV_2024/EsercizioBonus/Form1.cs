using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsercizioBonus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float n1 = float.Parse(Numero1.Text);
            float n2 = float.Parse(Numero2.Text);
            Testo.Text = (n1 + n2).ToString();
        }

        private void BottoneDifferenza_Click(object sender, EventArgs e)
        {
            float n1 = float.Parse(Numero1.Text);
            float n2 = float.Parse(Numero2.Text);
            Testo.Text = (n1 - n2).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float n1 = float.Parse(Numero1.Text);
            float n2 = float.Parse(Numero2.Text);
            Testo.Text = (n1 * n2).ToString();
        }

        private void BottoneDivisione_Click(object sender, EventArgs e)
        {
            float n1 = float.Parse(Numero1.Text);
            float n2 = float.Parse(Numero2.Text);
            Testo.Text = (n1 / n2).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Testo_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
