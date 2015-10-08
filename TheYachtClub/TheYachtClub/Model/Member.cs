using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YachtClub.Model
{
    class Member
    {
        private string personal_id;
        private Guid member_id;
        private string first_name;
        private string last_name;
    
        public Member() { }

        public Member(string personal_id, Guid member_id, string first_name, string last_name)
        {
            this.personal_id = personal_id;
            this.member_id = member_id;
            this.first_name = first_name;
            this.last_name = last_name;
        }

        public string Personal_id { get { return personal_id; } set { personal_id = value; } }
        public Guid Member_id { get { return member_id; } set { member_id = value; } }
        public string First_name { get { return first_name; } set { first_name = value; } }
        public string Last_name { get { return last_name; } set { last_name = value; } }

    }
}
