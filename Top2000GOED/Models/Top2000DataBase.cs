namespace Top2000GOED.Models
{
    using System.Data.Entity;

    public partial class Top2000DataBase : DbContext
    {
        public Top2000DataBase()
            : base("name=Top2000DataBase")
        {
        }
		
		// Hier zie je dat Artiest, Lijst en Song uit de database zijn ingesteld
        public virtual DbSet<Artiest> Artiest { get; set; }
        public virtual DbSet<Lijst> Lijst { get; set; }
        public virtual DbSet<Song> Song { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artiest>()
                .HasMany(e => e.Song)
                .WithRequired(e => e.Artiest)
                .WillCascadeOnDelete(false);
	 
	         modelBuilder.Entity<Song>()
                .HasMany(e => e.Lijst)
                .WithRequired(e => e.Song)
                .WillCascadeOnDelete(false);
        }
    }
}
