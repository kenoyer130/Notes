using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace notes.Models
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public int NoteID { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        public int CategoryID { get; set; }

        [DataMember]
        [StringLength(8000)]
        public string Description { get; set; }
        
        [DataMember]
        public Bucket Bucket { get; set; }
    }
}