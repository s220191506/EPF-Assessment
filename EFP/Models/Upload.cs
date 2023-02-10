using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFP.Models
{
    public class Upload
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public DateTime DateUploaded { get; set; }
        public Byte[] data { get; set; }
        public int UserId { get; set; }
        public Customer User {get;set;}
    }
}
