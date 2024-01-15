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
using System.Data.SqlClient;

namespace Enrollment_System_2
{
    public partial class Student : Form
    {
        int id;
        int program;
        string gender, gender2, status, yrlvl;
        enrollmentDataContext db = new enrollmentDataContext();
        SqlConnection conn = new SqlConnection(@"Data Source = LAPTOP-7VJGOGAD\SQLEXPRESS; Initial Catalog = ENROLLMENT_DB; Integrated security=True;");
        public Point mouseLocation;

        public Student()
        {
            InitializeComponent();
            bdate.MaxDate = DateTime.Now;
            studentData.DataSource = db.stud_view();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
            fillComboBox();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            studentData.DataSource = db.stud_view();
            timer1.Start();
            fillComboBox();
        }

        private void fillComboBox()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT PROGRAM_ID FROM PROGRAM", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CBprog.Items.Add(dr["PROGRAM_ID"].ToString());
            }
            conn.Close();
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

        private void txtphnum__TextChanged(object sender, EventArgs e)
        {
            Regexp(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", txtphnum, pcbox1, phnumValid, "Phone Number is");
        }

        private void txtemail__TextChanged(object sender, EventArgs e)
        {
            Regexp(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", txtemail, pcbox2, emailValid, "Email is");
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
                        if (RBMale.Checked == true || RBFemale.Checked == true)
                        {
                            if (RBMale.Checked == true)
                            {
                                gender = "Male";
                            }
                            else
                            {
                                gender = "Female";
                            }

                            db.stud_save(firstname, lastname, midname, gender, phonenum, bd, age, address, emailad, yrlvl, status, datenow, program);
                            MessageBox.Show("Save Successfully!", "OK");
                            studentData.DataSource = db.stud_view();
                            clear();
                            fillComboBox();
                            updateBTN.Enabled = false;
                            deleteBTN.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Please choose a gender!", "OK");
                        }

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
            CBprog.Texts = "CHOOSE PROGRAM";
            CByrlvl.Texts = "YEAR LEVEL";
            CBstat.Texts = "STATUS";
            bdate.Text = string.Empty;
            emailValid.Text = string.Empty;
            pcbox1.Image = new Bitmap(pcbox1.Width, pcbox1.Height);
            phnumValid.Text = string.Empty;
            pcbox2.Image = new Bitmap(pcbox2.Width, pcbox2.Height);
            RBMale.Checked = false;
            RBFemale.Checked = false;
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
                Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
                Regex regphnum = new Regex(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                if (regemail.IsMatch(txtemail.Texts))
                {
                    if (regphnum.IsMatch(txtphnum.Texts))
                    {
                        if (RBMale.Checked == true)
                        {
                            gender = "Male";
                        }
                        else
                        {
                            gender = "Female";
                        }

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

                        db.stud_update(id, firstname, lastname, midname, gender, phonenum, bd, age, address, emailad, yrlvl, status, program);
                        MessageBox.Show("Update Successfully!", "OK");
                        studentData.DataSource = db.stud_view();
                        clear();
                        fillComboBox();
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
                db.stud_delete(id);
                MessageBox.Show("Successfully Deleted!", "OK");
                studentData.DataSource = db.stud_view();
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
            studentData.DataSource = db.stud_search(searchbox.Texts);
        }

        private void studentData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (studentData.CurrentRow != null)
            {
                updateBTN.Enabled = true;
                deleteBTN.Enabled = true;
                id = int.Parse(studentData.CurrentRow.Cells[0].Value.ToString());
                txtfname.Texts = studentData.CurrentRow.Cells[1].Value.ToString();
                txtlname.Texts = studentData.CurrentRow.Cells[2].Value.ToString();
                txtmidname.Texts = studentData.CurrentRow.Cells[3].Value.ToString();
                gender2 = studentData.CurrentRow.Cells[4].Value.ToString();
                txtphnum.Texts = studentData.CurrentRow.Cells[5].Value.ToString();
                bdate.Text = ((DateTime)studentData.CurrentRow.Cells[6].Value).ToString();
                txtaddress.Texts = studentData.CurrentRow.Cells[8].Value.ToString();
                txtemail.Texts = studentData.CurrentRow.Cells[9].Value.ToString();
                CByrlvl.Texts = yrlvl = studentData.CurrentRow.Cells[10].Value.ToString();
                CBstat.Texts = status = studentData.CurrentRow.Cells[11].Value.ToString();
                CBprog.Texts = studentData.CurrentRow.Cells[13].Value.ToString();

                program = int.Parse(studentData.CurrentRow.Cells[13].Value.ToString());

                if (gender2 == "Male")
                {
                    RBMale.Checked = true;
                }
                else
                {
                    RBFemale.Checked = true;
                }

            }
            else
            {
                MessageBox.Show("Row is empty!", "OK");
            }
        }

        private void btnProf_Click(object sender, EventArgs e)
        {
            Professor prof = new Professor();

            prof.Show();
            this.Hide();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();

            dash.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }

        private void CByrlvl_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            yrlvl = CByrlvl.SelectedItem.ToString();
        }

        private void CBprog_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            program = int.Parse(CBprog.SelectedItem.ToString());
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            Classes cls = new Classes();

            cls.Show();

            this.Hide();
        }

        private void searchbox_Leave(object sender, EventArgs e)
        {
            studentData.DataSource = db.stud_view();
        }

        private void CBstat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            status = CBstat.SelectedItem.ToString();
        }

        private void Student_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Student_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            Enroll enrl = new Enroll();

            enrl.Show();

            this.Hide();
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            Subjects sub = new Subjects();

            sub.Show();
            this.Hide();
        }

        private void btnProg_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }
    }
}
