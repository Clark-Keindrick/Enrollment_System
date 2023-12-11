using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Enrollment_System_2
{
    public partial class Subjects : Form
    {
        string subcode,year, semester, desc;
        decimal unit;
        enrollmentDataContext db = new enrollmentDataContext();
        public Subjects()
        {
            InitializeComponent();
            subData.DataSource = db.sub_view();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        private void Subjects_Load(object sender, EventArgs e)
        {
            subData.DataSource = db.sub_view();
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

        private void btnProg_Click(object sender, EventArgs e)
        {
            Programs prog = new Programs();

            prog.Show();

            this.Hide();
        }

        private void btnProf_Click(object sender, EventArgs e)
        {
            Professor prof = new Professor();

            prof.Show();
            this.Hide();
        }

        private void btnStud_Click(object sender, EventArgs e)
        {
            Student stud = new Student();

            stud.Show();

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

        private void submitBTN_Click(object sender, EventArgs e)
        {
            try
            {
                unit = decimal.Parse(tbUNIT.Texts);

                db.sub_save(tbSUB.Texts, tbDESC.Texts, year, semester, unit);
                MessageBox.Show("Save Successfully!", "OK");
                subData.DataSource = db.sub_view();
                clear();
                updateBTN.Enabled = false;
                deleteBTN.Enabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                unit = decimal.Parse(tbUNIT.Texts);

                db.sub_update(tbSUB.Texts, desc, year, semester, unit);
                MessageBox.Show("Update Successfully!", "OK");
                subData.DataSource = db.sub_view();
                clear();
                updateBTN.Enabled = false;
                deleteBTN.Enabled = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exit Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            db.sub_delete(subcode);
            MessageBox.Show("Successfully Deleted!", "OK");
            subData.DataSource = db.sub_view();
            clear();
            updateBTN.Enabled = false;
            deleteBTN.Enabled = false;
        }

        public void clear()
        {
            tbSUB.Texts = string.Empty;
            tbDESC.Texts = string.Empty;
            tbUNIT.Texts = string.Empty;
            CByear.Texts = "CHOOSE YEAR";
            CBsem.Texts = "SELECT SEMESTER";
        }

        private void searchbox__TextChanged(object sender, EventArgs e)
        {
            subData.DataSource = db.sub_search(searchbox.Texts);
        }

        private void tbUNIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if ((e.KeyChar == '.') && ((sender as CJcontrols.CJTextbox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void subData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (subData.CurrentRow != null)
            {
                updateBTN.Enabled = true;
                deleteBTN.Enabled = true;
                subcode = tbSUB.Texts = subData.CurrentRow.Cells[0].Value.ToString();
                desc = tbDESC.Texts = subData.CurrentRow.Cells[1].Value.ToString();
                year = CByear.Texts = subData.CurrentRow.Cells[2].Value.ToString();
                semester = CBsem.Texts = subData.CurrentRow.Cells[3].Value.ToString();
                tbUNIT.Texts = subData.CurrentRow.Cells[4].Value.ToString();

                unit = decimal.Parse(subData.CurrentRow.Cells[4].Value.ToString());

            }
            else
            {
                MessageBox.Show("Row is empty!", "OK");
            }
        }

        private void btnClass_Click(object sender, EventArgs e)
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

        private void searchbox_Leave(object sender, EventArgs e)
        {
            subData.DataSource = db.sub_view();
        }

        private void CBsem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            semester = CBsem.SelectedItem.ToString();
        }

        private void CByear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            year = CByear.SelectedItem.ToString();
        }
    }
}
