using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuanLyKhachSan.ViewModel
{
    #region Parameter
    public class Parameter
    {
        [Required, Display(Name = "Từ ngày")]
        public string FromDate { get; set; }
        [Required, Display(Name = "Đến ngày")]
        public string ToDate { get; set; }
    }
    #endregion
    #region Detail
    public class Detail
    {
        public string Day { get; set; }
        public decimal RoomServiceAmount { get; set; }
        public decimal OtherServiceAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    #endregion
    #region Detail2
    public class Detail2
    {
        public string Day { get; set; }
        public string RoomName { get; set; }
        public int Count { get; set; }
        public decimal RoomServiceAmount { get; set; }
        public List<OtherServiceDetail> OtherServiceDetails { get; set; }
        public decimal TotalAmount { get; set; }
    }
    #endregion
    #region OtherServiceDetail
    public class OtherServiceDetail
    {
        public string OtherServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal OtherServiceAmount { get; set; }
    }
    #endregion
    #region Report
    public class Report
    {
        public Parameter Parameter { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }
    #endregion
    #region ReportDetail
    public class ReportDetail
    {
        public Parameter Parameter { get; set; }
        public IEnumerable<Detail2> Details { get; set; }
    }
    #endregion

}