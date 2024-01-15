using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaceRecognition;

namespace Enrollment_System_2
{
    public partial class Face : Form
    {      
        public Face()
        {
            InitializeComponent();
            FaceRec fc = new FaceRec();
            fc.openCamera(pictureBox1, pictureBox1);
            fc.isTrained = true;
            

            okBTN.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            okBTN.Visible = true;
        }

        private void okBTN_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Hide();
        }
    }
}
