using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YachtClub.Controller;
using YachtClub.Model;

namespace TheYachtClub.View
{
    class ConsoleHandler
    {
        public RegistryHandler handler = new RegistryHandler();

        public void base_Loop()
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Press 1 to create a new member");
            System.Console.WriteLine("Press 2 to view the member List");
            System.Console.WriteLine("Press 3 to view the Compact List");
            System.Console.WriteLine("Press 4 to view the Verbose List");
            System.Console.WriteLine("Press 5 to edit an existing member");

            int intTemp = Convert.ToInt32(Console.ReadLine());

            switch (intTemp)
            {
                case 1:
                    new_Member();
                    break;
                case 2:
                    member_view();
                    break;
                case 3:
                    compact_view();
                    break;
                case 4:
                    verbose_view();
                    break;
                case 5:
                    member_edit();
                    break;

                default:
                    Console.WriteLine("That number was not between the specified contraints");
                    base_Loop();
                    break;
            }
        }

        /*
         * Method that gives the user the option about what to edit
         */
        private void member_edit()
        {
            System.Console.WriteLine("Please enter your personal number to edit your personal information");
            string personal_Number = Console.ReadLine();

            foreach (Member m in handler.getAllMembers())
            {
                if (m.Personal_id.Equals(personal_Number))
                {
                    System.Console.WriteLine("hello, " + m.First_name + ", what would you like to edit?");
                    System.Console.WriteLine("press 1 to edit first name");
                    System.Console.WriteLine("press 2 to edit Last name");
                    System.Console.WriteLine("press 3 to edit boat information");
                    System.Console.WriteLine("press 4 to delete user");
                    int intTemp = Convert.ToInt32(Console.ReadLine());

                    switch (intTemp)
                    {
                        case 1:
                            edit_First_name(m.Personal_id);
                            break;
                        case 2:
                            edit_Last_name(m.Personal_id);
                            break;
                        case 3:
                            edit_boat_Info(m.Personal_id);
                            break;
                        case 4:
                            delete_User(m.Personal_id);
                            break;
                        default:
                            Console.WriteLine("That number was not between the specified contraints");
                            base_Loop();
                            break;
                    }
                }
            }
            Console.Error.WriteLine("Im sorry, but your personal number was not recognized in our current list of members");
        }


        /*
         * Method that gives the user the option to remove a user
         */
        private void delete_User(string personal_id)
        {

            System.Console.WriteLine("Are you sure you want to leave the best yacht club EUW? (Y/N)");
            if ((Console.ReadKey().Equals("Y")) || (Console.ReadKey().Equals("y")))
            {
                handler.deleteMember(personal_id);
                System.Console.WriteLine("GOODBYE");
                base_Loop();
            }
            else
            {
                base_Loop();
            }
        }


        /*
        * Submenu that gives the user the option of editing the boat
        */
        private void edit_boat_Info(string personal_id)
        {
            // need to grab all the members information
            Member m = handler.getMember(personal_id);

            System.Console.WriteLine("hello, " + m.First_name + " what would u like to do?");
            System.Console.WriteLine("press 1 to add a Boat to your registry");
            System.Console.WriteLine("press 2 to delete a boat from your registry");

            int intTemp = Convert.ToInt32(Console.ReadLine());

            switch (intTemp)
            {
                case 1:
                    add_Boat(m.Personal_id);
                    break;
                case 2:
                    delete_Boat(m.Personal_id);
                    break;
                default:
                    Console.WriteLine("That number was not between the specified contraints");
                    base_Loop();
                    break;
            }
        }


        /*
        * Submenu that gives the user the option of editing the boat
        */
        private void delete_Boat(string personal_id)
        {
            System.Console.WriteLine("What is the name of the boat you would like to delete?");
            String boat_Name = Console.ReadLine();

            Member m = handler.getMember(personal_id);

            handler.deleteBoat(personal_id, boat_Name);
            base_Loop();
        }

        /*
        * Submenu that gives the user the option of adding a boat
        */
        private void add_Boat(string personalID)
        {
            //show list of boats
            System.Console.WriteLine("Current Boats ");
            System.Console.WriteLine("What is the name of the boat");
            string name = Console.ReadLine();
            System.Console.WriteLine("What type of boat would you like to add?");
            string type = Console.ReadLine();
            System.Console.WriteLine("How long is the boat?");
            string length = Console.ReadLine();

            handler.addBoat(personalID, name, type, length);

            base_Loop();
        }

        /*
        * Submenu that gives the user the option of editing the last name
        */
        private void edit_Last_name(string personal_id)
        {
            System.Console.WriteLine("What is the new Last Name?");
            string last_name = Console.ReadLine();

            Member mem = handler.getMember(personal_id);
            mem.Last_name = last_name;

            handler.deleteMember(personal_id);
            handler.addMember(mem.Personal_id, mem.Member_id, mem.Last_name, mem.First_name);

            System.Console.WriteLine("thank you " + last_name + " your information has been updated");
            System.Console.WriteLine("press any key to continue");
            Console.ReadKey();
            System.Console.WriteLine(" ");
            base_Loop();
        }

        /*
        * Submenu that gives the user the option of editing the firstName
        */
        private void edit_First_name(string personal_id)
        {
            System.Console.WriteLine("What is the new First Name?");
            string first_name = Console.ReadLine();

            Member mem = handler.getMember(personal_id);
            mem.First_name = first_name;

            handler.deleteMember(personal_id);
            handler.addMember(mem.Personal_id, mem.Member_id, mem.Last_name, mem.First_name);

            System.Console.WriteLine("thank you " + first_name + " your information has been updated");
            System.Console.WriteLine("press any key to continue");
            Console.ReadKey();
            System.Console.WriteLine(" ");
            base_Loop();
        }

        /*
        * showing the verbose view
        */
        private void verbose_view()
        {
            // i need 
            // member name, personal number, member id and boats with boat information
            RegistryHandler handler = new RegistryHandler();
            foreach (Member m in handler.getAllMembers())
            {
                List<Boat> boats = handler.getMemberBoats(m.Personal_id);
                System.Console.WriteLine(m.Personal_id + " | " + m.Last_name + " | " + m.First_name + " | " + m.Member_id);
                foreach (Boat b in boats)
                {
                    System.Console.WriteLine(b.Name + " | " + b.Length + " | " + b.Type);
                }
            }
            System.Console.WriteLine("press any key to return to Home");
            Console.ReadKey();

            System.Console.WriteLine(" ");
            base_Loop();
        }

        /*
        * showing the compact view
        */
        private void compact_view()
        {
            // name, member id and number of boats

            foreach (Member m in handler.getAllMembers())
            {
                System.Console.WriteLine(m.Personal_id + " | " + m.Last_name + " | " + m.First_name + " | " + m.Member_id.ToString() + "|" + m.Boats);
            }

            System.Console.WriteLine("press any key to return to Home");
            Console.ReadKey();

            System.Console.WriteLine(" ");
            base_Loop();
        }

        /*
        * showing the member view
        */
        private void member_view()
        {
            //List of members

            foreach (Member m in handler.getAllMembers())
            {
                System.Console.WriteLine(m.Personal_id + " | " + m.Last_name + " | " + m.First_name + " | " + m.Member_id.ToString());
            }

            System.Console.WriteLine("press any key to return to Home");
            Console.ReadKey();

            System.Console.WriteLine(" ");
            base_Loop();
        }


        /*
        * Dialog for adding a new member
        */
        private void new_Member()
        {
            System.Console.WriteLine("Hello new Member, what is your First name?");
            string first_name = Console.ReadLine();
            System.Console.WriteLine("What is your Last name?");
            string last_name = Console.ReadLine();
            System.Console.WriteLine("what is your personal number?");
            string personal_number = Console.ReadLine();
            Guid rnd = Guid.NewGuid();

            handler.addMember(personal_number, rnd, first_name, last_name);

            System.Console.WriteLine("Thank you " + first_name + " for joining our yatch club!");
            System.Console.WriteLine("Do you have any Boats to register with us?   (Y/N)");
            if ((Console.ReadKey().Equals("Y")) || (Console.ReadKey().Equals("y")))
            {
                edit_boat_Info(personal_number);
            }
            else
            {
                base_Loop();
            }
        }
    }

}
