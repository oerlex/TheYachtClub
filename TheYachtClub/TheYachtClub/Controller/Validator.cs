using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheYachtClub.Controller
{
    /*
     *The validator class takes care that the user inputs are valid 
     */
    class Validator
    {
        /*
        *Validates the member name in terms of length and
        */
        public  bool validateMembername(string name){
           if(name.Length >=20)
                return false;
           return true;
        }

        /*
       *Validates the personal number. The length can't have more or less than 10 digits
       */
        public bool validatePersonalnumber(string personalNumber){
            if (personalNumber.Length != 10)
                return false;
            return true;
        }
    }
}
