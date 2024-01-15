using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enrollment_System_2.CJcontrols;

namespace Enrollment_System_2
{
    public partial class Professor : Form
    {
        public Point mouseLocation;
        int id;
        enrollmentDataContext db = new enrollmentDataContext();
        public Professor()
        {
            InitializeComponent();
            bdate.MaxDate = DateTime.Now;
            profData.DataSource = db.prof_view();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void Professor_Load(object sender, EventArgs e)
        {
            profData.DataSource = db.prof_view();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
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

        private void txtemail__TextChanged(object sender, EventArgs e)
        {
            Regexp(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", txtemail, pcbox2, emailValid, "Email is");
        }

        private void txtphnum__TextChanged(object sender, EventArgs e)
        {
            Regexp(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", txtphnum, pcbox1, phnumValid, "Phone Number is");
        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string firstname = CapitalizeWords(txtfname.Texts);
                string lastname = CapitalizeWords(txtlname.Texts);
                string phonenum = txtphnum.Texts;
                string address = CapitalizeWords(txtaddress.Texts);
                string emailad = txtemail.Texts;
                string midname = CapitalizeWords(txtmidname.Texts);
                DateTime bd = DateTime.Parse(bdate.Text);
                DateTime datenow = DateTime.Now;
                TimeSpan myage = datenow - bd;
                short age = (short)(myage.TotalDays / 365.25);

                Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
                Regex regphnum = new Regex(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                if (regemail.IsMatch(emailad))
                {
                    if (regphnum.IsMatch(phonenum))
                    {
                        db.prof_save(firstname, lastname, midname, phonenum, bd, address, emailad, age, datenow);
                        MessageBox.Show("Save Successfully!", "OK");
                        profData.DataSource = db.prof_view();
                        clear();
                        updateBTN.Enabled = false;
                        deleteBTN.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Phone number is Invalid!", "OK");
                    }
                }
                else
                {
                    MessageBox.Show("Email is Invalid!", "OK");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clear()
        {
            txtfname.Texts = string.Empty;
            txtlname.Texts = string.Empty;
            txtaddress.Texts = string.Empty;
            txtphnum.Texts = string.Empty;
            txtemail.Texts = string.Empty;
            txtmidname.Texts = string.Empty;
            bdate.Text = string.Empty;
            emailValid.Text = string.Empty;
            pcbox1.Image = new Bitmap(pcbox1.Width, pcbox1.Height);
            phnumValid.Text = string.Empty;
            pcbox2.Image = new Bitmap(pcbox2.Width, pcbox2.Height);
        }

        public void Regexp(string re, CJRoundTextbox tb, PictureBox pc, Label lbl, string s)
        {
            Regex regex = new Regex(re);

            if (regex.IsMatch(tb.Texts))
            {
                pc.Image = Properties.Resources.valid;
                lbl.ForeColor = Color.Green;
                lbl.Text = s + " valid";
            }
            else
            {
                pc.Image = Properties.Resources.invalid;
                lbl.ForeColor = Color.Red;
                lbl.Text = s + " invalid";
            }
        }

        public static string CapitalizeWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            string[] words = input.Split(' ');

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i]);
            }

            return string.Join(" ", words);
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string firstname = CapitalizeWords(txtfname.Texts);
                string lastname = CapitalizeWords(txtlname.Texts);
                string phonenum = txtphnum.Texts;
                string address = CapitalizeWords(txtaddress.Texts);
                string emailad = txtemail.Texts;
                string midname = CapitalizeWords(txtmidname.Texts);

                DateTime bd = DateTime.Parse(bdate.Text);
                DateTime datenow = DateTime.Now;
                TimeSpan myage = datenow - bd;
                short age = (short)(myage.TotalDays / 365.25);

                Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
                Regex regphnum = new Regex(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                if (regemail.IsMatch(txtemail.Texts))
                {
                    if (regphnum.IsMatch(txtphnum.Texts))
                    {

                        db.prof_update(id, firstname, lastname, midname, phonenum, bd, address, emailad, age);
                        MessageBox.Show("Update Successfully!", "OK");
                        profData.DataSource = db.prof_view();
                        clear();
                        updateBTN.Enabled = false;
                        deleteBTN.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Phone number is Invalid!", "OK");
                    }
                }
                else
                {
                    MessageBox.Show("Email is Invalid!", "OK");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            try
            {
                db.prof_delete(id);
                MessageBox.Show("Successfully Deleted!", "OK");
                profData.DataSource = db.prof_view();
                clear();
                updateBTN.Enabled = false;
                deleteBTN.Enabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            profData.DataSource = db.prof_search(searchbox.Texts);
        }

        private void profData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (profData.CurrentRow != null)
            {
                updateBTN.Enabled = true;
                deleteBTN.Enabled = true;
                id = int.Parse(profData.CurrentRow.Cells[0].Value.ToString());
                txtfname.Texts = profData.CurrentRow.Cells[1].Value.ToString();
                txtlname.Texts = profData.CurrentRow.Cells[2].Value.ToString();
                txtmidname.Texts = profData.CurrentRow.Cells[3].Value.ToString();
                txtphnum.Texts = profData.CurrentRow.Cells[4].Value.ToString();
                bdate.Text = ((DateTime)profData.CurrentRow.Cells[5].Value).ToString();
                txtaddress.Texts = profData.CurrentRow.Cells[6].Value.ToString();
                txtemail.Texts = profData.CurrentRow.Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show("Row is empty!", "OK");
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

        private void btnProg_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
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

        private void searchbox_Leave(object sender, EventArgs e)
        {
            profData.DataSource = db.prof_view();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void Professor_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Professor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Personal personalForm = new Personal();
            personalForm.Show();
            this.Hide();
        }
    }
}
