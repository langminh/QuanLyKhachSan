using QuanLyKhachSan.Entity.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Entity.Dao
{
    public class RoomDao
    {
        private DataEntities db = new DataEntities();
        public RoomDao()
        {
            db = new DataEntities();
        }

        public List<Room> GetListHotRoom()
        {
            return db.Rooms.Where(x => x.TypeID == 2).ToList();
        }

        public List<Room> GetListVipRom()
        {
            return db.Rooms.Where(x => x.TypeID == 1).ToList();
        }

        public List<Room> GetListSaleRoom()
        {
            return db.Rooms.Where(x => x.TypeID == 3).ToList();
        }

        public List<Room> GetListNormalRoom()
        {
            return db.Rooms.Where(x => x.TypeID == 4).ToList();
        }
    }
}