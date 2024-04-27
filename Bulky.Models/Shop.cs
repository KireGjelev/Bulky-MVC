using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "geometry(Point)")]
        public Point Location { get; set; }

        public Shop()
        {
            Location = new Point(0, 0);
        }
    }
}
