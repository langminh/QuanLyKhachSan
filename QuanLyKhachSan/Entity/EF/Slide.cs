namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int SlideID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Order { get; set; }

        public int? RoomID { get; set; }
    }
}
