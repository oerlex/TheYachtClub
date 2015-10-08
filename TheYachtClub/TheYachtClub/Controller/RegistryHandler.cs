using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YachtClub.Model;

namespace YachtClub.Controller
{
    class RegistryHandler
    {
        //Returns a boat of a specific member
        public Boat getBoat(string personalNumber, string boatName)
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();

            IEnumerable<XElement> boats = (from x in xEle.Elements()
                                           where x.Attribute("name").Value == boatName
                                           select x);
            

            XElement boatElement = boats.First();
            String temp = xEle.Attribute("length").Value;

            temp = boatElement.Attribute("type").Value;
            
            Boat.boats_type t = (Boat.boats_type)Enum.Parse(typeof(Boat.boats_type), temp);

            Boat b = new Boat(boatElement.Attribute("name").Value, Int32.Parse(temp),t) ;
            return b;
        }

        //Returns all boats of the specific member 
        public List<Boat> getMemberBoats(string personalNumber)
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();

            IEnumerable<XElement> boats = (from x in xEle.Elements()
                                           select x);

            List<Boat> memberBoats = new List<Boat>();

            if (boats.Elements("Member").Any()) { 
                foreach(XElement boatElement in boats){
                
                    string length = xEle.Attribute("length").Value;

                    string temp1 = boatElement.Attribute("type").Value;

                    Boat.boats_type type = (Boat.boats_type)Enum.Parse(typeof(Boat.boats_type), temp1);
                    
                    String temp2 = xEle.Attribute("boat_id").Value;
                    Guid boat_id = Guid.Parse(temp2);

                    Boat boat = new Boat(boatElement.Attribute("name").Value, Int32.Parse(length), type, boat_id);
                    memberBoats.Add(boat);
                }
            }
            return memberBoats;
        }

        //Returns a member from xaml file
        public Member getMember(String personalNumber)
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> members = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);
            if (members.Elements().Count() != 0)
            {
                XElement xEle = members.First();
                Member m = new Member();

                m.Personal_id = xEle.Attribute("personal_id").Value;
                m.Last_name = xEle.Attribute("last_name").Value;
                m.First_name = xEle.Attribute("first_name").Value;

                String temp = xEle.Attribute("member_id").Value;
                m.Member_id = Guid.Parse(temp);

                return m;
            }
            else { throw new Exception("Member does not exist."); }
        }

        //Returns a list of all the members in the xaml file
        public List<Member> getAllMembers()
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              select x);

            List<Member> allMembers = new List<Member>();

            foreach(XElement e in elements){
                Member m = new Member();

                m.Personal_id = e.Attribute("personal_id").Value;
                m.Last_name = e.Attribute("last_name").Value;
                m.First_name = e.Attribute("first_name").Value;

                String temp = e.Attribute("member_id").Value;
                Guid guid = Guid.Parse(temp);
                m.Member_id = guid;
               
                allMembers.Add(m);                
            }
            return allMembers;
        }

        //Deletes a member from xaml file
        public void deleteMember(string personalNumber) {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> members = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);
            if (members.Elements().Count() != 0)
            {
                XElement xEle = members.First();
                xEle.Remove();
                doc.Save("..\\..\\Model\\storage.xml");
            }
            else { throw new Exception("Member does not exist."); }
        }

        //Deletes a boat from xaml file
        public void deleteBoat(string personalNumber, string boatName)
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();

            IEnumerable<XElement> boats = (from x in xEle.Elements()
                                           where x.Attribute("name").Value == boatName
                                           select x);

            if (boats.Elements().Count() != 0)
            {
                XElement boatElement = boats.First();
                boatElement.Remove();
                doc.Save("..\\..\\Model\\storage.xml");
            }
            else { throw new Exception("Boat does not exist."); }
        }

        //Adds a member in xaml database file
        public void addMember(string personalNumber, Guid memberID, string firstName, string lastName)
        {
            XDocument myxml = XDocument.Load("..\\..\\Model\\storage.xml");

            IEnumerable<XElement> members = (from x in myxml.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);
            if (members.Elements().Count() == 0)
            {
                XElement element = new XElement("Member");
                myxml.Root.Add(element);
                element.SetAttributeValue("personal_id", personalNumber);
                element.SetAttributeValue("last_name", lastName);
                element.SetAttributeValue("first_name", firstName);
                element.SetAttributeValue("member_id", memberID.ToString());

                Member member = new Member(personalNumber, memberID, firstName, lastName);

                myxml.Save("..\\..\\Model\\storage.xml");
            }
            else { throw new Exception("Member already exists."); }
        }

        //Clears the xaml database file
        public void clear(){
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            doc.Root.RemoveNodes();
        }

        //A method for adding a boat
        public void addBoat(string personalNumber, string name, string type, string length){
            XDocument myxml = XDocument.Load("..\\..\\Model\\storage.xml");
            
            IEnumerable<XElement> elements = (from x in myxml.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement member = elements.First();

            XElement element = new XElement("Boat");

            IEnumerable<XElement> allBoats = (from x in member.Elements()
                                           select x);

            if (allBoats.Elements().Count() != 0)
            {
                createBoat(member, element, name, type, length);
                myxml.Save("..\\..\\Model\\storage.xml");
            }

            IEnumerable<XElement> boats = (from x in member.Elements()
                                           where x.Attribute("name").Value == name
                                           select x);
            if (boats.Elements().Count() == 0)
            {
                createBoat(member, element, name, type, length);
                myxml.Save("..\\..\\Model\\storage.xml");
            }
            else { throw new Exception("Boat already exists.");}
        }

       //Helping method for creating a boat in the database xaml file
        private void createBoat(XElement member, XElement element, string name, string type, string length)
        {
            member.Add(element);
            element.SetAttributeValue("name", name);
            element.SetAttributeValue("length", length);
            element.SetAttributeValue("type", type);
        }
    }
}
