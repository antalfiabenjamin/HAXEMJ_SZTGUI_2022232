using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HAXEMJ_HFT_2022231.Models
{
    public class User 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        public int PhoneCount { get; set; }
        public double TotalAvgScrTime { get; set; }

        [JsonIgnore]
        public virtual ICollection<Phone> OwnedPhones { get; set; }

        public User()
        {

        }

        public User(string data)
        {
            string[] temp = data.Split('#');
            Id = int.Parse(temp[0]);
            Name = temp[1];
        }

        public override bool Equals(object obj)
        {
            User u = obj as User;
            if (u != null && this.Id == u.Id &&
                this.Name == u.Name &&
                this.TotalAvgScrTime == u.TotalAvgScrTime &&
                this.PhoneCount == u.PhoneCount) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

