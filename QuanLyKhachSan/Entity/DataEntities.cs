namespace QuanLyKhachSan.Entity
{
    using QuanLyKhachSan.Entity.EF;
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataEntities : DbContext
    {
        public DataEntities()
            : base("name=DataEntities")
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<InOut> InOuts { get; set; }
        public virtual DbSet<InOut_OtherServices> InOut_OtherServices { get; set; }
        public virtual DbSet<OtherService> OtherServices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomServicePrice> RoomServicePrices { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<EF.Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataEntities>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
