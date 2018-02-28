namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Type")]
    public partial class Type
    {
        public int TypeID { get; set; }

        [StringLength(250)]
        public string TypeName { get; set; }
    }
}
