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
    // In Login class
    public partial class Login : Form
    {
        FaceRec face;

        public Login()
        {
            InitializeComponent();
            face = new FaceRec();
            face.openCamera(pictureBox2, pictureBox3);
        }

        //public void openCamera(PictureBox cameraPictureBox, PictureBox facePictureBox)
        //{
        //    face.openCamera(cameraPictureBox, facePictureBox);

        //    using (Graphics g = facePictureBox.CreateGraphics())
        //    {
        //        string text = "Face Detected";
        //        Font font = new Font("Arial", 12, FontStyle.Bold);
        //        Brush brush = Brushes.Green;
        //        PointF point = new PointF((facePictureBox.Width - g.MeasureString(text, font).Width) / 2,
        //                                  (facePictureBox.Height - g.MeasureString(text, font).Height) / 2);
        //        g.DrawString(text, font, brush, point);
        //    }
        //}
        private void lognbtn_Click(object sender, EventArgs e)
        {
            string user= username.Text;
            string pass = password.Text;
            MessageBox.Show("Login Successful!");
        }


    }



}
