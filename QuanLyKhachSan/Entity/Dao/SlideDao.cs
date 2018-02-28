using QuanLyKhachSan.Entity.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Entity.Dao
{
    public class SlideDao
    {
        private DataEntities db;
        public SlideDao()
        {
            db = new DataEntities();
        }
        public List<Slide> GetListSlide()
        {
            List<Slide> list = db.Slides.ToList();
            return list;
        }
    }
}