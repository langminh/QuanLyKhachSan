namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InOut")]
    public partial class InOut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InOut()
        {
            InOut_OtherServices = new HashSet<InOut_OtherServices>();
        }

        public long InOutId { get; set; }

        public int? RoomId { get; set; }

        public int PersonNumber { get; set; }

        public DateTime CheckinTime { get; set; }

        public DateTime CheckOutTime { get; set; }

        public DateTime PayDate { get; set; }

        public decimal LengthStay { get; set; }

        public int RoomServiceType { get; set; }

        public decimal RoomServiceAmount { get; set; }

        public decimal OtherServiceAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Note { get; set; }

        public int Status { get; set; }

        public bool IsDouble { get; set; }

        public virtual Room Room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InOut_OtherServices> InOut_OtherServices { get; set; }
    }
}
