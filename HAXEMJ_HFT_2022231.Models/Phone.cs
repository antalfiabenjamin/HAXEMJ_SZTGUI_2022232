using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace HAXEMJ_HFT_2022231.Models
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        public string Color { get; set; }

        public string ManufacturerID { get; set; }

        public int UserID { get; set; }

        public double AvgScreenTime { get; set; }

        public string UserName { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual User User { get; set; }

        public Phone()
        {

        }

        public Phone(string data)
        {
            string[] temp = data.Split('#');
            Id = int.Parse(temp[0]);
            Name = temp[1];
            Color = temp[2];
            ManufacturerID = temp[3];
            UserID = int.Parse(temp[4]);
            AvgScreenTime = double.Parse(temp[5], CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            Phone p = obj as Phone;
            if (p != null && this.Name == p.Name &&
                this.Id == p.Id) return true;

            if (p == null) return false;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

