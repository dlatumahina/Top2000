namespace Top2000GOED.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Song")]
    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            Lijst = new HashSet<Lijst>();
        }

        public int songid { get; set; }

        public int artiestid { get; set; }

        [Required]
        [StringLength(100)]
        public string titel { get; set; }

        public int jaar { get; set; }

        public virtual Artiest Artiest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lijst> Lijst { get; set; }
    }
}
