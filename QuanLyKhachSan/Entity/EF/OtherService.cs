namespace QuanLyKhachSan.Entity.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherService")]
    public partial class OtherService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OtherService()
        {
            InOut_OtherServices = new HashSet<InOut_OtherServices>();
        }

        public int OtherServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InOut_OtherServices> InOut_OtherServices { get; set; }
    }
}
