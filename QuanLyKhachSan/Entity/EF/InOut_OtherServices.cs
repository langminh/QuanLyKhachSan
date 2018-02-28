namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InOut_OtherServices
    {
        public long Id { get; set; }

        public long InOutId { get; set; }

        public int OtherServiceId { get; set; }

        public int Quantity { get; set; }

        public virtual InOut InOut { get; set; }

        public virtual OtherService OtherService { get; set; }
    }
}
