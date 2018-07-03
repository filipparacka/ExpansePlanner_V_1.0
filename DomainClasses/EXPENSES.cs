namespace DomainClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYSTEM.EXPENSES")]
    public partial class EXPENSES
    {
        [Key]
        public int ID { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRICE { get; set; }

        public DateTime? Date { get; set; }

        public int USERS_ID { get; set; }

        public int CATEGORY_ID { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
