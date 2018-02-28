namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomServicePrice")]
    public partial class RoomServicePrice
    {
        public int RoomServicePriceId { get; set; }

        public int RoomServiceType { get; set; }

        public int Minutes { get; set; }

        public decimal Price { get; set; }
    }
}
