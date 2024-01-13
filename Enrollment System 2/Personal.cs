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

namespace Enrollment_System_2
{
    public partial class Personal : Form
    {
        public int id = user.CurrentUser.User.id;
        enrollmentDataContext db = new enrollmentDataContext();
        private Timer timer;

        public Personal()
        {
            InitializeComponent();
            userdetails();
           string Datenow = DateTime.Now.Date.ToString("D");
            date.Text = Datenow;
            InitializeTimer();

        }
        private void InitializeTimer()
        {
            // Create a timer with an interval of 1000 milliseconds (1 second)
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick_1;

            // Start the timer
            timer.Start();
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("h:mm:ss tt");
        }
        private void Personal_Load(object sender, EventArgs e)
        {
           
        }
       

        private void userdetails()
        {
            var resUser = db.select_Userdetail(id);

            foreach (var item in resUser)
            {
                fname.Texts = item.Admin_Fname;
                lname.Texts = item.Admin_Lname;
                mname.Texts = item.Admin_Midname;
                phone.Texts = item.Admin_Contact;
                usern.Texts = item.Admin_Username;
            }
        }

        private void addnewBTN_Click(object sender, EventArgs e)
        {
            Admin adm = new Admin();
            adm.Show();
            this.Hide();
        }

        private void btndashboard_Click_1(object sender, EventArgs e)
        {
            Dashboard da = new Dashboard();
            da.Show();
            da.Hide();
        }

        private void btnstudent_Click_1(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

            this.Hide();
        }

        private void btnprof_Click_1(object sender, EventArgs e)
        {
            Professor prof = new Professor();
            prof.Show();
            this.Hide();
        }

        private void btnprog_Click_1(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }

        private void btnsub_Click_1(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void btnclass_Click_1(object sender, EventArgs e)
        {
            Classes cls = new Classes();

            cls.Show();

            this.Hide();
        }

        private void btnEnroll_Click_1(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
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

        private void updateBTN_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
            this.Hide();
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            Personal personalForm = new Personal();
            personalForm.Show();
            this.Hide();
        }

        
    }
}
