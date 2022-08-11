using System.ComponentModel.DataAnnotations;

namespace JustPlay.Data.Models
{
    public class Videogame
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string SoftwareHouse { get; set; }
        public string Publisher { get; set; }
        public string Synopsis { get; set; }
        public string Cover { get; set; }
        public string? Trailer { get; set; }

        public override bool Equals(object obj)
        {
            var objectToCompare = obj as Videogame;

            if (objectToCompare == null)
            {
                return false;
            }
            if (objectToCompare.Title == this.Title && 
                objectToCompare.Year == this.Year &&
                objectToCompare.Genre == this.Genre &&
                objectToCompare.SoftwareHouse == this.SoftwareHouse &&
                objectToCompare.Publisher == this.Publisher &&
                objectToCompare.Synopsis == this.Synopsis &&
                objectToCompare.Cover == this.Cover)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ID;
        }

    }
}
