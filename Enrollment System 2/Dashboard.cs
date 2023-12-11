using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Enrollment_System_2
{
    public partial class Dashboard : Form
    {
        DataTable dt = new DataTable();
        enrollmentDataContext db = new enrollmentDataContext();
        SqlConnection conn = new SqlConnection(@"Data Source = CLARK-KEINDRICK\SQLEXPRESS; Initial Catalog = ENROLLMENT_DB; Integrated security=True;");
        
        public Dashboard()
        {
            InitializeComponent();
            enrolData.DataSource = db.enrollees();
            DBdisplay();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enrolData.DataSource = db.enrollees();
            timer1.Start();
        }

        private void DBdisplay()
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT (PROF_ID) FROM PROFESSOR", conn);
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT (SUB_CODE) FROM SUBJECTS", conn);
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT (PROGRAM_ID) FROM PROGRAM", conn);
            SqlCommand cmd4 = new SqlCommand("SELECT COUNT (DISTINCT ID) FROM ENROLL", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lblprof.Text = dr.GetValue(0).ToString();
            }
            dr.Close();

            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lblsub.Text = dr2.GetValue(0).ToString();
            }
            dr2.Close();

            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                lblprog.Text = dr3.GetValue(0).ToString();
            }
            dr3.Close();

            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                lblenrollee.Text = dr4.GetValue(0).ToString();
            }

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
                InitializeComponent();
            }
        }

        private void btnstudent_Click(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnprof_Click(object sender, EventArgs e)
        {
            Professor prof = new Professor();

            prof.Show();
            this.Hide();
        }

        private void btnprog_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }

        private void btnsub_Click(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void btnclass_Click(object sender, EventArgs e)
        {
            Classes cls = new Classes();

            cls.Show();

            this.Hide();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            enrolData.DataSource = db.search_enrollees(searchbox.Texts);
        }

        private void searchbox_Leave(object sender, EventArgs e)
        {
            enrolData.DataSource = db.enrollees();
        }
    }
}
