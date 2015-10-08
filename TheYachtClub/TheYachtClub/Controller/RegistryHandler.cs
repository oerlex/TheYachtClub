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

                    string temp = boatElement.Attribute("type").Value;

                    Boat.boats_type t = (Boat.boats_type)Enum.Parse(typeof(Boat.boats_type), temp);

                    Boat b = new Boat(boatElement.Attribute("name").Value, Int32.Parse(length), t);
                    memberBoats.Add(b);
                }
            }
            return memberBoats;
        }

        public Member getMember(String personalNumber)
        {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();
            Member m = new Member();

            m.Personal_id = xEle.Attribute("personal_id").Value;
            m.Last_name = xEle.Attribute("last_name").Value;
            m.First_name = xEle.Attribute("first_name").Value;

            String temp = xEle.Attribute("member_id").Value;
            m.Member_id = Guid.Parse(temp);

            return m;
        }

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

        public void deleteMember(string personalNumber) {
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            IEnumerable<XElement> elements = (from x in doc.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();
            xEle.Remove();
            doc.Save("..\\..\\Model\\storage.xml");
        }

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


            XElement boatElement = boats.First();
            boatElement.Remove();
            doc.Save("..\\..\\Model\\storage.xml");
        }

        public void addMember(string personalID, Guid memberID, string lastName, string firstName )
        {
            XDocument myxml = XDocument.Load("..\\..\\Model\\storage.xml");

            XElement element = new XElement("Member");
            myxml.Root.Add(element);
            element.SetAttributeValue("personal_id", personalID);
            element.SetAttributeValue("last_name",lastName);
            element.SetAttributeValue("first_name", firstName);
            element.SetAttributeValue("member_id", memberID.ToString());

            Member member = new Member(personalID, memberID, firstName, lastName);

            XNode test = new XElement("boats");
            element.Add(test);

            foreach (Boat b in member.Boats)
            {
                Boat newBoat = new Boat();

                XElement xBoat = new XElement(b.Name.ToString());
                xBoat.SetAttributeValue("type", b.Type.GetType());
                xBoat.SetAttributeValue("length", b.Length.ToString());

                test.AddAfterSelf(xBoat);
            }

            myxml.Save("..\\..\\Model\\storage.xml");


        }

        public void clear(){
            XDocument doc = XDocument.Load("..\\..\\Model\\storage.xml");
            doc.Root.RemoveNodes();
        }

        public void addBoat(string personalNumber, string name, string type, string length){
            XDocument myxml = XDocument.Load("..\\..\\Model\\storage.xml");
            
            IEnumerable<XElement> elements = (from x in myxml.Root.Elements()
                                              where x.Attribute("personal_id").Value == personalNumber
                                              select x);

            XElement xEle = elements.First();
            
            XElement element = new XElement("Boat");
            xEle.Add(element);
            element.SetAttributeValue("name", name);
            element.SetAttributeValue("length",length);
            element.SetAttributeValue("type", type);
            myxml.Save("..\\..\\Model\\storage.xml");
        }

    }
}
