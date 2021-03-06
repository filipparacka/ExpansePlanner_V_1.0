namespace DomainClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EXPENSES")]
    public partial class Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        
        public decimal? Price { get; set; }

        public DateTime Date { get; set; }

        public int Users_ID { get; set; }

        public int Category_ID { get; set; }

        public virtual ExpenseCategory Category { get; set; }

        public virtual Users Users { get; set; }
    }
}
