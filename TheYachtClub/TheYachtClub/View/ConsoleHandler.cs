using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheYachtClub.Controller;
using YachtClub.Controller;
using YachtClub.Model;

namespace TheYachtClub.View
{
    class ConsoleHandler
    {
        public RegistryHandler handler = new RegistryHandler();
        public Validator validator = new Validator();

		//The main menu where the user can choose which action to perform
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
                    System.Console.WriteLine(" ");
                    new_Member();
                    break;
                case 2:
                    System.Console.WriteLine(" ");
                    member_view();
                    break;
                case 3:
                    System.Console.WriteLine(" ");
                    compact_view();
                    break;
                case 4:
                    System.Console.WriteLine(" ");
                    verbose_view();
                    break;
                case 5:
                    System.Console.WriteLine(" ");
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
                    System.Console.WriteLine("hello, Mrs/Mr." + m.First_name + ", what would you like to edit?");
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
        private void delete_User(string personal_id){
            System.Console.WriteLine("Are you sure you want to leave the best yacht club EUW? (Y/N)");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "y":
                    handler.deleteMember(personal_id);
                    System.Console.WriteLine("GOODBYE");
                    base_Loop();
                    break;

                case "n":
                    Console.WriteLine("Too bad!");
                    base_Loop();
                    break;
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
            System.Console.WriteLine("press 3 to edit a current boat from your registry");
            int intTemp = Convert.ToInt32(Console.ReadLine());

            switch (intTemp)
            {
                case 1:
                    add_Boat(m.Personal_id);
                    break;
                case 2:
                    delete_Boat(m.Personal_id);
                    break;
                case 3:
                    edit_Current_Boat(personal_id);
                    break;
                        
                default:
                    Console.WriteLine("That number was not between the specified contraints");
                    base_Loop();
                    break;
            }
        }

        private void edit_Current_Boat(string personal_id)
        {
            System.Console.WriteLine("What is the name of the boat you would like to edit?");
            string boat_Name = Console.ReadLine();



            handler.getBoat(personal_id, boat_Name);
            handler.deleteBoat(personal_id, boat_Name);



            System.Console.WriteLine("What is the new name of the boat");
            string name = Console.ReadLine();
            System.Console.WriteLine("What is the type of this boat?");
            string type = Console.ReadLine();
            System.Console.WriteLine("How long is the boat? (in meters) ");
            string length = Console.ReadLine();

            handler.addBoat(personal_id, name, type, length);

            System.Console.WriteLine("Thank you " + name + " was added to your List of boats");

            base_Loop();




            base_Loop();
        }


        /*
        * Submenu that gives the user the option of editing the boat
        */
        private void delete_Boat(string personal_id)
        {
            System.Console.WriteLine("What is the name of the boat you would like to delete?");
            String boat_Name = Console.ReadLine();

            handler.deleteBoat(personal_id, boat_Name);

            System.Console.WriteLine("Thank you, " + boat_Name + " was removed from your List of boats");

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
            System.Console.WriteLine("('s' for Sailboat, 'm' for Motorboat, 'k' for kayak, 'o' for other)");
            string type = Console.ReadLine();
            System.Console.WriteLine("How long is the boat? (in meters) ");
            string length = Console.ReadLine();

            handler.addBoat(personalID, name, type, length);

            System.Console.WriteLine("Thank you " + name + " was added to your List of boats");

            base_Loop();
        }

        /*
        * Submenu that gives the user the option of editing the last name
        */
        private void edit_Last_name(string personal_id)
        {
            System.Console.WriteLine("What is the new Last Name?");
            string last_name = Console.ReadLine();

			if (validator.validateMembername (last_name)) {
				Member mem = handler.getMember (personal_id);
				mem.Last_name = last_name;

				handler.deleteMember (personal_id);
				handler.addMember (mem.Personal_id, mem.Member_id, mem.Last_name, mem.First_name);

				System.Console.WriteLine ("thank you " + last_name + " your information has been updated");
				System.Console.WriteLine ("press any key to continue");
				Console.ReadKey ();
				System.Console.WriteLine (" ");
			} else {
				System.Console.WriteLine ("The name can't exceed the limit of 20 characters");
				edit_Last_name(personal_id);
			}
            base_Loop();
        }

        /*
        * Submenu that gives the user the option of editing the firstName
        */
        private void edit_First_name(string personal_id)
        {
            System.Console.WriteLine("What is the new First Name?");
            string first_name = Console.ReadLine();

			if(validator.validateMembername(first_name)){
	            Member mem = handler.getMember(personal_id);
	            mem.First_name = first_name;

	            handler.deleteMember(personal_id);
	            handler.addMember(mem.Personal_id, mem.Member_id, mem.First_name ,mem.Last_name);

	            System.Console.WriteLine("thank you " + first_name + " your information has been updated");
	            System.Console.WriteLine("press any key to continue");
	            Console.ReadKey();
	            System.Console.WriteLine(" ");

			} else {
				System.Console.WriteLine ("The name can't exceed the limit of 20 characters");
				edit_First_name(personal_id);
			}
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
                System.Console.WriteLine(m.Personal_id + " | " + m.Last_name + " | " + m.First_name + " | " + m.Member_id + "|" ) ;
                foreach (Boat b in boats)
                {
                    System.Console.WriteLine(b.Name + " | " + b.Length + " | " + b.Type + " | " + b.BoatID);
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
                StringBuilder memList = new StringBuilder();
                int boats_no = handler.getMemberBoats(m.Personal_id).Count();
                memList.Append(m.Personal_id);
                System.Console.WriteLine(m.Personal_id + " | " + m.Last_name + " | " + m.First_name + " | " + m.Member_id.ToString() + " | " + boats_no);
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
                System.Console.WriteLine( m.First_name + " | " + m.Last_name);
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
           
			if (validator.validateMembername (first_name)) {				
				System.Console.WriteLine ("What is your Last name?");
				string last_name = Console.ReadLine ();
				if (validator.validateMembername (last_name)) {					
					System.Console.WriteLine ("what is your personal number?");
					string personal_number = Console.ReadLine ();
					if (validator.validatePersonalnumber (personal_number)) {						
						Guid rnd = Guid.NewGuid ();

						handler.addMember (personal_number, rnd, first_name, last_name);

						System.Console.WriteLine ("Thank you " + first_name + " for joining our yatch club!");
						System.Console.WriteLine ("Would you like to add a boat " + first_name + "?");


						string input = Console.ReadLine ();
						switch (input.ToLower ()) {
						case "y":
							edit_boat_Info (personal_number);                    
							break;

						case "n":               
							base_Loop ();
							break;
						
						}
					}else {
						System.Console.WriteLine ("The personal number doesn't consist of 10 character. Please check and try again");
						new_Member ();
					}
				}else {
					System.Console.WriteLine ("The name can't exceed the limit of 20 characters");
					new_Member ();
				}
			} else {
				System.Console.WriteLine ("The name can't exceed the limit of 20 characters");
				new_Member ();
			}

            base_Loop();
            
        }
    }

}
