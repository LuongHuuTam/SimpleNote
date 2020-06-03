using SimpleNote.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNote.Controllers
{
    public class NoteController
    {
        public static int getID()
        {
            using (var _context=new SimpleNoteEntities())
            {
                var id = (from n in _context.Notes
                          select n.NoteID).ToList();
                if (id.Count <= 0 || id[0] != 1)
                    return 1;
                int i;
                for (i = 0; i < id.Count - 1; i++)
                {
                    if (id[i + 1] - id[i] != 1)
                        break;
                }
                return id[i] + 1;
            }
        }
        public static bool addNote(Note note)
        {
            try
            {
                using (var _context = new SimpleNoteEntities())
                {
                    
                    _context.Notes.AddOrUpdate(note);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static List<Note> getListNote()
        {
            using (var _context=new SimpleNoteEntities())
            {
                var node = (from n in _context.Notes.AsEnumerable()
                            select new
                            {
                                id = n.NoteID,
                                title = n.NoteTitle,
                                description = n.NoteDescription,
                                modified=n.NoteModified
                            }).Select(x => new Note
                            {
                                NoteID = x.id,
                                NoteDescription = x.description,
                                NoteTitle = x.title,
                                NoteModified=x.modified
                            }).ToList();
                return node;
            }
        }
        public static Note getNote(int id)
        {
            using (var _context=new SimpleNoteEntities())
            {
                var note = (from n in _context.Notes
                            where n.NoteID == id
                            select n).SingleOrDefault();
                return note;
            }
        }
        public static bool deleteNote(int id)
        {
            try
            {
                using (var _context = new SimpleNoteEntities())
                {
                    var note = (from n in _context.Notes
                                where n.NoteID == id
                                select n).Single();
                    _context.Notes.Remove(note);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static List<Note> getListNote(string str)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var node = (from n in _context.Notes.AsEnumerable()
                            where n.NoteTitle.Contains(str)
                            select new
                            {
                                id = n.NoteID,
                                title = n.NoteTitle,
                                description = n.NoteDescription,
                                modified = n.NoteModified
                            }).Select(x => new Note
                            {
                                NoteID = x.id,
                                NoteDescription = x.description,
                                NoteTitle = x.title,
                                NoteModified = x.modified
                            }).ToList();
                return node;
            }
        }
    }
}
