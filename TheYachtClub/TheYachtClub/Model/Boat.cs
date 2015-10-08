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

        public Boat() { }

        public Boat(string name, int length, boats_type type)
        {
            this.name = name;
            this.length = length;
            this.type = type;
        }

        public string Name { get { return name; } set { name = value; } }
        public int Length { get { return length; } set { length = value; } }
        public boats_type Type { get { return type; } set { type = value; } }
    }
}
