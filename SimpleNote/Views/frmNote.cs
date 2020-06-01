using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNote.Views
{
    public partial class frmNote : Form
    {
        public frmNote()
        {
            InitializeComponent();
        }
        private void frmNote_Load(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (btnSelect.Text == "All Note")
            {
                this.btnSelect.Text = Text = "Trash";
                this.btnSave.Visible = false;
                this.btnDelete.Text = "Delete Forever";
                this.btnInfo.Text = "Restore Note";
                this.btnNew.Enabled = false;
                //shdbfhsdfbhsfb
            }
            else
            {
                this.btnSelect.Text = Text = "All Note";
                this.btnSave.Visible = true;
                this.btnDelete.Text = "Delete";
                this.btnInfo.Text = "Info";
                this.btnNew.Enabled = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
