using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HAXEMJ_HFT_2022231.Models
{
    public class Manufacturer 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Phone> Phones { get; set; }

        public int ColorPhoneCount { get; set; }

        public string Location { get; set; }

        public Manufacturer()
        {
            
        }

        public Manufacturer(string data)
        {
            string[] temp = data.Split('#');
            Id = temp[0];
            Name = temp[1];
            Location = temp[2];
        }

        public override bool Equals(object obj)
        {
            Manufacturer m = obj as Manufacturer;
            if (m != null && this.Id == m.Id &&
                this.Name == m.Name &&
                this.ColorPhoneCount == m.ColorPhoneCount) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

