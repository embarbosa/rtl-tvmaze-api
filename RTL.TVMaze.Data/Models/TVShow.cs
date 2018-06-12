﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TVMaze.Data.Models
{
    [Table("TVShow")]
    public class TVShow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public virtual ICollection<Cast> Cast { get; set; }
        public DateTime Updated { get; set; }
    }
}