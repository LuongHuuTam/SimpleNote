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
        private bool flag;
        private int i;
        public frmNote()
        {
            InitializeComponent();
        }
        private void frmNote_Load(object sender, EventArgs e)
        {
            flag = true;
            loadNote();
        }      
        private void loadNote()
        {
            if (flag)
            {
                listView1.Items.Clear();
                List<Note> listNode = NoteController.getListNote();
                foreach (Note note in listNode)
                {
                    string text = note.NoteTitle;
                    ListViewItem n = new ListViewItem(note.NoteID.ToString());
                    n.SubItems.Add(text);
                    this.listView1.Items.Add(n);
                }
            }
            else
            {
                listView1.Items.Clear();
                List<Trash> listTrash = TrashConntroller.getListTrash();
                foreach (Trash trash in listTrash)
                {
                    string text = trash.TrashTitle;
                    ListViewItem n = new ListViewItem(trash.TrashID.ToString());
                    n.SubItems.Add(text);
                    this.listView1.Items.Add(n);
                }
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
                flag = false;
                this.btnSelect.Text = Text = "Trash";
                this.btnSave.Visible = false;
                this.btnDelete.Text = "Delete Forever";
                this.btnInfo.Text = "Restore Note";
                this.btnNew.Enabled = false;
                loadNote();
            }
            else
            {
                flag = true;
                this.btnSelect.Text = Text = "All Note";
                this.btnSave.Visible = true;
                this.btnDelete.Text = "Delete";
                this.btnInfo.Text = "Info";
                this.btnNew.Enabled = true;
                loadNote();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Note note = new Note();
            note.NoteID = NoteController.getID();
            note.NoteTitle = "New note";
            note.NoteDescription = "";
            note.NoteModified = DateTime.Now;
            if (NoteController.addNote(note) == false)
                MessageBox.Show("kkk");
            loadNote();
            reset();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Note note = new Note();
                note.NoteID = int.Parse(listView1.SelectedItems[0].Text);
                note.NoteTitle = tbTitle.Text;
                note.NoteDescription = rtbDescreption.Text;
                note.NoteModified = DateTime.Now;
                if (NoteController.addNote(note) == false)
                    MessageBox.Show("kkk");
                loadNote();
                reset();
                btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
            }
            else
            {
                return;
            }
        }
        private void listView1_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = true;
            i = int.Parse(listView1.SelectedItems[0].Text);
            if (flag)
            {
                Note note = NoteController.getNote(int.Parse(listView1.SelectedItems[0].Text));
                tbTitle.Text = note.NoteTitle;
                rtbDescreption.Text = note.NoteDescription;
            }
            else
            {
                Trash trash = TrashConntroller.GetTrash(int.Parse(listView1.SelectedItems[0].SubItems[0].Text));
                tbTitle.Text = trash.TrashTitle;
                rtbDescreption.Text = trash.TrashDescription;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Note note = NoteController.getNote(int.Parse(listView1.SelectedItems[0].Text));
                Trash trash = new Trash() {TrashID=TrashConntroller.getID(), TrashTitle = note.NoteTitle, TrashDescription = note.NoteDescription };
                NoteController.deleteNote(int.Parse(listView1.SelectedItems[0].Text));
                TrashConntroller.addTrash(trash);
                loadNote();
                reset();
                btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
            }
            else
            {
                TrashConntroller.deleteTrash(int.Parse(listView1.SelectedItems[0].SubItems[0].Text));
                loadNote();
                reset();
                btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
            }
        }
        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Note note = NoteController.getNote(int.Parse(listView1.SelectedItems[0].Text));
                string[] arr = note.NoteDescription.Split(' ');
                string[] arr1 = note.NoteDescription.Split('\n');
                int a = arr.Count() + arr1.Count() - 1;
                MessageBox.Show(note.NoteDescription.Length + "character\n" + a + "words\n" + "Modified: " + note.NoteModified.Value.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
            }
            else
            {
                Trash trash = TrashConntroller.GetTrash(int.Parse(listView1.SelectedItems[0].Text));
                Note note = new Note();
                note.NoteID = NoteController.getID();
                note.NoteTitle = trash.TrashTitle;
                note.NoteDescription = trash.TrashDescription;
                note.NoteModified = DateTime.Now;
                TrashConntroller.deleteTrash(int.Parse(listView1.SelectedItems[0].Text));
                NoteController.addNote(note);
                loadNote();
                reset();
                btnDelete.Enabled = btnInfo.Enabled = btnSave.Enabled = false;
            }
        }

        private void tbSearchNote_TextChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                listView1.Items.Clear();
                List<Note> listNode = NoteController.getListNote(tbSearchNote.Text);
                foreach (Note note in listNode)
                {
                    string text = note.NoteTitle;
                    ListViewItem n = new ListViewItem(note.NoteID.ToString());
                    n.SubItems.Add(text);
                    this.listView1.Items.Add(n);
                }
            }
            else
            {
                listView1.Items.Clear();
                List<Trash> listTrash = TrashConntroller.getListTrash(tbSearchNote.Text);
                foreach (Trash trash in listTrash)
                {
                    string text = trash.TrashTitle;
                    ListViewItem n = new ListViewItem("T");
                    n.SubItems.Add(text);
                    this.listView1.Items.Add(n);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Note note = new Note();
                note.NoteID = NoteController.getID();
                note.NoteTitle = "New note";
                note.NoteDescription = "";
                if (NoteController.addNote(note) == false)
                    MessageBox.Show("kkk");
                loadNote();
                reset();
            }
            else
            {
                return;
            }
        }

       
    }
}
