using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models.AjaxModel
{
    public class CheckIn
    {
        public int RoomId { get; set; }
        public int PersonNumber { get; set; }
        public DateTime CheckinTime { get; set; }
        public int RoomServiceType { get; set; }
        public decimal OtherServiceAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }

        public List<OtherService> OtherServices { get; set; }
    }
    public class CheckOut : CheckIn
    {
        public DateTime CheckOutTime { get; set; }
        public string CheckInTimeStr { get; set; }
        public decimal LengthStay { get; set; }
        public bool IsNotCheckOut { get; set; }
    }
    public class OtherService
    {
        public int OtherServiceId { get; set; }
        public int Quantity { get; set; }
        public string ServiceName { get; set; }
        public decimal Amount { get; set; }
        public bool IsDel { get; set; }
    }

    public class FloorJS
    {
        public int floorID { get; set; }
        public string roomName { get; set; }
        public bool status { get; set; }
        public bool isDouble { get; set; }
    }
}