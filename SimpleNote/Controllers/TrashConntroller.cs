using SimpleNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNote.Controllers
{
    public class TrashConntroller
    {
        public static List<Trash> getListTrash()
        {
            using (var _context = new SimpleNoteEntities())
            {
                var t = (from n in _context.Trashes.AsEnumerable()
                         select new
                         {
                             title = n.TrashTitle,
                             descreption = n.TrashDecreption
                         }).Select(x => new Trash
                         {
                             TrashDecreption = x.descreption,
                             TrashTitle = x.title
                         }).ToList();
                return t;
            }
        }
        public static List<Trash> getListTrash(string str)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var t = (from n in _context.Trashes.AsEnumerable()
                         where n.TrashTitle.Contains(str)
                         select new
                         {
                             title = n.TrashTitle,
                             descreption = n.TrashDecreption
                         }).Select(x => new Trash
                         {
                             TrashDecreption = x.descreption,
                             TrashTitle = x.title
                         }).ToList();
                return t;
            }
        }
        public static void addTrash(Trash trash)
        {
            using (var _context = new SimpleNoteEntities())
            {
                _context.Trashes.Add(trash);
                _context.SaveChanges();
            }
        }
        public static Trash GetTrash(string str)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var t = (from tr in _context.Trashes
                         where tr.TrashTitle == str
                         select tr).SingleOrDefault();
                return t;
            }
        }
        public static void deleteTrash(string str)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var tr = (from t in _context.Trashes
                          where t.TrashTitle == str
                          select t).Single();
                _context.Trashes.Remove(tr);
                _context.SaveChanges();
            }
        }
    }
}
