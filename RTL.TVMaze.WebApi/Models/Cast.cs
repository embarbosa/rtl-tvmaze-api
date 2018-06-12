using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TVMaze.WebApi.Models
{
    [Table("Cast")]
    public class Cast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }        
        public int TVShowId { get; set; }
        [ForeignKey("TVShowId")]
        public TVShow TVShow { get; set; }
    }
}