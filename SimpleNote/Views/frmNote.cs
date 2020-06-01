using SimpleNote.Controllers;
using SimpleNote.Models;
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
            loadNote();
            //timer1.Enabled = true;
        }
        private void loadNote()
        {
            listView1.Items.Clear();
            List<Note> listNode = NoteController.getListNote();
            foreach (Note note in listNode)
            {
                string text = note.Title;
                ListViewItem n = new ListViewItem(note.ID.ToString());
                n.SubItems.Add(text);
                this.listView1.Items.Add(n);
            }
        }
        private void reset()
        {
            tbTitle.Text = rtbDescreption.Text = "";
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            TaoNoteMoi();
        }




        private void TaoNoteMoi()
        {
            Note note = new Note();
            note.ID = NoteController.getID();
            note.Title = "New note";
            note.Descreption = "";
            if (NoteController.addNote(note) == false)
                MessageBox.Show("kkk");
            loadNote();
            reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Note note = new Note();
            note.ID = int.Parse(listView1.SelectedItems[0].Text);
            note.Title = tbTitle.Text;
            note.Descreption = rtbDescreption.Text;
            if (NoteController.addNote(note) == false)
                MessageBox.Show("kkk");
            loadNote();
            reset();
            btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = true;
            Note note = NoteController.getNote(int.Parse(listView1.SelectedItems[0].Text));
            tbTitle.Text = note.Title;
            rtbDescreption.Text = note.Descreption;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NoteController.deleteNote(int.Parse(listView1.SelectedItems[0].Text));
            loadNote();
            reset();
            btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            Note note = NoteController.getNote(int.Parse(listView1.SelectedItems[0].Text));
            string[] arr = note.Descreption.Split(' ');
            string[] arr1 = note.Descreption.Split('\n');
            int a = arr.Count() + arr1.Count()-1;
            MessageBox.Show(note.Descreption.Length+"character\n"+a+"words\n"+"","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
            btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
        }
    }
}
