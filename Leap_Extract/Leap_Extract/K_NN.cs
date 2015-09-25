using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap_Extract.Data_Structure;
using Leap;
using System.IO;

namespace Leap_Extract
{
    class K_NN
    {
        List<person> allPersons = new List<person>();
        person thisPerson;
        int thisPersonIndex;

        


        public K_NN (List<person> listIn, person tmpPerson)
        {
            this.allPersons = listIn;
            thisPerson = tmpPerson;
            thisPersonIndex = allPersons.IndexOf(thisPerson);
            
        }
        
        public String getUname(person cryU)
        {
            String thisUName = cryU.getUserName();

            return thisUName;

        }

        public String getName(person cryN)
        {

            String thisName = cryN.getName();

            return thisName;

        }

        public void comparePersons()
        {
            decimal difference = 0;

            foreach (person listPerson in allPersons)
            {
                
            }

        }


    }
}
