using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YachtClub.Model
{
    class Boat
    {
        public enum boats_type
        {
            Sailboat,
            Motorsailer,
            kayak,
            Other
        };
        private string name;
        private int length;
        private boats_type type;
        private Guid boat_id;

        public Boat() { }

        public Boat(string name, int length, boats_type type)
        {
            this.name = name;
            this.length = length;
            this.type = type;
            boat_id = new Guid();
        }

        public Boat(string name, int length, boats_type type, Guid boat_id)
        {
            this.name = name;
            this.length = length;
            this.type = type;
            this.boat_id = boat_id;
        }

        public string Name { get { return name; } set { name = value; } }
        public int Length { get { return length; } set { length = value; } }
        public boats_type Type { get { return type; } set { type = value; } }
        public Guid BoatID { get { return boat_id; } set { boat_id = value; } }
    }
}
