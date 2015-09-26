using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Leap;
using Leap_Extract.Data_Structure;
using System.Xml.Serialization;
using System.IO;
using System.Timers;
using CsvHelper;

namespace Leap_Extract.Data_Structure
{
    
    public class person
    {
        public int knn_score = 0;
        public ds_hand leftHand;

        //public ds_hand rightHand;
        string nameSur;
        string username;
        int age;
        char gender;

        public person()
        {
            this.nameSur = "";
            this.username = "";
            this.age = 0;
            this.gender = 'M';
        }

        public person(string name, string uname, int age, char gender)
        {
            leftHand = new ds_hand(true,this);
            //rightHand = new ds_hand(false);

            this.nameSur = name;
            this.username = uname;
            this.age = age;

            if(gender == 'M' || gender == 'F')
                this.gender = gender;
        }

        public string getName()
        {
            return this.nameSur;
        }

        public string getUserName()
        {
            return this.username;
        }

        public int getAge()
        {
            return age;

        }

        public char getGender()
        {
            return this.gender;
        }

        public List<String> csvToString()
        {
            List<String> mesg = new List<String>();

            mesg.Add(getName());
            mesg.Add(getUserName());
            mesg.Add(Convert.ToString(getAge()));
            mesg.Add(Convert.ToString(getGender()));

            return mesg;
        }
    }
}
