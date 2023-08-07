using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Customer_Feedback
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(); // create a reference to Form2 called f2
            this.Hide(); // hide form 1 from view
            f2.ShowDialog(); // opens form 2, makes it the focus
            this.Close(); // close form 1 after form 2 is closed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(); // create a reference to Form2 called f2
            this.Hide(); // hide form 1 from view
            f3.ShowDialog(); // opens form 2, makes it the focus
            this.Close(); // close form 1 after form 2 is closed
        }
    }
}
