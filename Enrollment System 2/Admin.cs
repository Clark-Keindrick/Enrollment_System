using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaceRecognition;

namespace Enrollment_System_2
{
    public partial class Admin : Form
    {
        public int id = user.CurrentUser.User.id;
        enrollmentDataContext db = new enrollmentDataContext();
        SqlConnection conn = new SqlConnection(@"Data Source = LAPTOP-7VJGOGAD\SQLEXPRESS; Initial Catalog = ENROLLMENT_DB; Integrated security=True;");
        private Timer timer;
        public Admin()
        {
            InitializeComponent();
            submitBTN.Visible = false;
            detectBTN.Visible = false;
            Details();

            string Datenow = DateTime.Now.Date.ToString("D");
            date.Text = Datenow;
        }
        FaceRec fc = new FaceRec();

        private void InitializeTimer()
        {
            // Create a timer with an interval of 1000 milliseconds (1 second)
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;

            // Start the timer
            timer.Start();
        } 
        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("h:mm:ss tt");
        }
        private void Details()
        {
            var resUser = db.select_Userdetail(id);

            foreach (var item in resUser)
            {
                fname.Texts = item.Admin_Fname;
                lname.Texts = item.Admin_Lname;
                mname.Texts = item.Admin_Midname;
                phone.Texts = item.Admin_Contact;
                userna.Texts = item.Admin_Username;
                password.Texts = item.Admin_Pass;
                cp.Texts = item.Admin_Pass;
                

            }
        }
        private void btndashboard_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            dash.Hide();
        }

        private void btnstudent_Click(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

            this.Hide();
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

        private void Admin_Load(object sender, EventArgs e)
        {
            admnData.DataSource = db.ad_view();
        }

        private void opencamBTN_Click(object sender, EventArgs e)
        {
            fc.openCamera(pictureBox8, pictureBox9);
            submitBTN.Visible = true;
        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            Regex regphnum = new Regex(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            string first = fname.Texts;
            string last = lname.Texts;
            string middle = mname.Texts;
            string contact = phone.Texts;

            // Validate phone number
            if (!regphnum.IsMatch(contact))
            {
                MessageBox.Show("Invalid phone number format", "Try Again");
                return;
            }

            string usern = userna.Texts;
            string pass = password.Texts;
            string confirm = cp.Texts;

            if (pass != confirm)
            {
                MessageBox.Show("Password not match", "Try Again");
            }

            db.ad_save(first, last, middle, contact, usern, pass);
            MessageBox.Show("Successfully Save", "Save");
            fc.Save_IMAGE(userna.Texts);
            Clear();
            detectBTN.Visible = true;

            admnData.DataSource = db.ad_view();
        }




        private void Clear()
        {
            fname.Texts = "";
            lname.Texts = "";
            mname.Texts = "";
            phone.Texts = "";
            userna.Texts = "";
            password.Texts = "";
            cp.Texts = "";
        }

        private void detectBTN_Click(object sender, EventArgs e)
        {
            fc.isTrained = true;
            
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            admnData.DataSource = db.ad_search(searchbox.Texts);
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            Regex regphnum = new Regex(@"^[0-9]{11}$");
            string newfirst = fname.Texts;
            string newlast = lname.Texts;
            string newmiddle = mname.Texts;
            string newcontact = phone.Texts;
            if (!regphnum.IsMatch(newcontact))
            {
                MessageBox.Show("Invalid phone number format", "Try Again");
                return;
            }
            // Check for negative numbers
            if (int.TryParse(newcontact, out int phoneNumber) && phoneNumber < 0)
            {
                MessageBox.Show("Phone number cannot be negative", "Try Again");
                return;
            }
            string newusern = userna.Texts;
            string newpass = password.Texts;
            string newconfirm = cp.Texts;

            if (newpass != newconfirm)
            {
                MessageBox.Show("Password not match", "Try Again");
            }

            db.ad_update(id,newfirst, newlast, newmiddle, newcontact, newusern, newpass);
            MessageBox.Show("Successfully Updated", "Update");
            fc.Save_IMAGE(userna.Texts);
            admnData.DataSource = db.ad_view();
            Clear();
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != 0)
                {
                    db.ad_delete(id);
                    MessageBox.Show("Successfully deleted", "Delete");
                    admnData.DataSource = db.ad_view();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Please select a record to delete", "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error");
            }
        }

        private void admnData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(admnData.CurrentRow.Cells[0].Value.ToString());
            fname.Texts = admnData.CurrentRow.Cells[1].Value.ToString();
            lname.Texts = admnData.CurrentRow.Cells[2].Value.ToString();
            mname.Texts = admnData.CurrentRow.Cells[3].Value.ToString();
            phone.Texts = admnData.CurrentRow.Cells[4].Value.ToString();
            userna.Texts = admnData.CurrentRow.Cells[5].Value.ToString();
            password.Texts = admnData.CurrentRow.Cells[6].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Personal personalForm = new Personal();
            personalForm.Show();
            this.Hide();
        }

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
               
                e.Handled = true;
            }
        }
    }
}
