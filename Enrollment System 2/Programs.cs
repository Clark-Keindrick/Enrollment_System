using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Enrollment_System_2
{
    public partial class Programs : Form
    {
        public Point mouseLocation;
        int id;
        enrollmentDataContext db = new enrollmentDataContext();
        public Programs()
        {
            InitializeComponent();
            progData.DataSource = db.prog_view();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void Program_Load(object sender, EventArgs e)
        {
            progData.DataSource = db.prog_view();
            timer1.Start();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();

            dash.Show();
            this.Hide();
        }

        private void btnStud_Click(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

            this.Hide();
        }

        private void btnProf_Click(object sender, EventArgs e)
        {
            Professor prof = new Professor();

            prof.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            db.prog_save(description.Texts);
            MessageBox.Show("Save Successfully!", "OK");
            progData.DataSource = db.prog_view();
            clear();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            db.prog_update(id, description.Texts);
            MessageBox.Show("Update Successfully!", "OK");
            progData.DataSource = db.prog_view();
            clear();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            db.prog_delete(id);
            MessageBox.Show("Successfully Deleted!", "OK");
            progData.DataSource = db.prog_view();
            clear();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        public void clear()
        {
            description.Texts = string.Empty;
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            progData.DataSource = db.prog_search(searchbox.Texts);
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            Classes cls = new Classes();

            cls.Show();

            this.Hide();
        }

        private void progData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (progData.CurrentRow != null)
            {
                updateBTN.Enabled = true;
                deleteBTN.Enabled = true;
                id = int.Parse(progData.CurrentRow.Cells[0].Value.ToString());
                description.Texts = progData.CurrentRow.Cells[1].Value.ToString();
            }
            else
            {
                MessageBox.Show("Row is empty!", "OK");
            }
        }

        private void searchbox_Leave(object sender, EventArgs e)
        {
            progData.DataSource = db.prog_view();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void Programs_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Programs_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }
    }
}
