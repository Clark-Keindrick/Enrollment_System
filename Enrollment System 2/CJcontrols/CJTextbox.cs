using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment_System_2.CJcontrols
{
    [DefaultEvent("_TextChanged")]
    public partial class CJTextbox : UserControl
    {
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private Color borderFocusColor = Color.HotPink;
        private bool isFocused = false;

        public event EventHandler _TextChanged;

        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;

        public CJTextbox()
        {
            InitializeComponent();
        }

        [Category("Clark Code Advance")]
        public Color BorderColor 
        {
            get 
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Clark Code Advance")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Clark Code Advance")]
        public bool UnderlinedStyle
        {
            get
            {
                return underlinedStyle;
            }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("Clark Code Advance")]
        public Color BorderFocusColor
        {
            get
            {
                return borderFocusColor;
            }
            set
            {
                borderFocusColor = value;
            }
        }

        public bool IsFocused
        {
            get
            {
                return isFocused;
            }
            set
            {
                isFocused = value;
                this.Invalidate();
            }
        }

        [Category("Clark Code Advance")]
        public bool PasswordChar
        {
            get { return isPasswordChar; }
            set
            {
                isPasswordChar = value;
                if (!isPlaceholder)
                    textBox1.UseSystemPasswordChar = value;
            }
        }

        [Category("Clark Code Advance")]
        public bool Multiline
        {
            get { return textBox1.Multiline; }
            set { textBox1.Multiline = value; }
        }

        [Category("Clark Code Advance")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        [Category("Clark Code Advance")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        [Category("Clark Code Advance")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (this.DesignMode)
                    UpdateControlHeight();
            }
        }

        [Category("Clark Code Advance")]
        public string Texts
        {
            get
            {
                if (isPlaceholder) return textBox1.Text;
                else return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
                SetPlaceholder();
            }
        }

        [Category("Clark Code Advance")]
        public Color PlaceholderColor
        {
            get { return placeholderColor; }
            set
            {
                placeholderColor = value;
                if (isPlaceholder)
                    textBox1.ForeColor = value;
            }
        }

        [Category("Clark Code Advance")]
        public string PlaceholderText
        {
            get { return placeholderText; }
            set
            {
                placeholderText = value;
                textBox1.Text = "";
                SetPlaceholder();
            }
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && placeholderText != "")
            {
                isPlaceholder = true;
                textBox1.Text = placeholderText;
                textBox1.ForeColor = placeholderColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = false;
            }
        }
        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textBox1.Text = "";
                textBox1.ForeColor = this.ForeColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            //Draw border
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                if (isFocused) penBorder.Color = borderFocusColor;//Set Border color in focus. Otherwise, normal border color
                if (underlinedStyle) //Line Style
                    graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                else //Normal Style
                    graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;
                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
            RemovePlaceholder();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
            SetPlaceholder();
        }
    }
}
