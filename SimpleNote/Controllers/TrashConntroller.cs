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
        public static int getID()
        {
            using (var _context = new SimpleNoteEntities())
            {
                var id = (from n in _context.Trashes
                          select n.TrashID).ToList();
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
        public static List<Trash> getListTrash()
        {
            using (var _context = new SimpleNoteEntities())
            {
                var t = (from n in _context.Trashes.AsEnumerable()
                         select new
                         {
                             id=n.TrashID,
                             title = n.TrashTitle,
                             descreption = n.TrashDescription
                         }).Select(x => new Trash
                         {
                             TrashID=x.id,
                             TrashDescription = x.descreption,
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
                             descreption = n.TrashDescription
                         }).Select(x => new Trash
                         {
                             TrashDescription = x.descreption,
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
        public static Trash GetTrash(int id)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var t = (from tr in _context.Trashes
                         where tr.TrashID == id
                         select tr).SingleOrDefault();
                return t;
            }
        }
        public static void deleteTrash(int id)
        {
            using (var _context = new SimpleNoteEntities())
            {
                var tr = (from t in _context.Trashes
                          where t.TrashID == id
                          select t).Single();
                _context.Trashes.Remove(tr);
                _context.SaveChanges();
            }
        }
    }
}
