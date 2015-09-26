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


namespace Leap_Extract
{

    public partial class frmControl : Form, ILeapEventDelegate
    {
        
        TextWriter myWriter;
        person currentPerson;
        person singlePerson;

        private Controller controller = new Controller();
        private LeapEventListener listener;

        private decimal[] thumbMeasurements = new decimal[3];
        private decimal[] pinkyMeasurements = new decimal[4];
        private decimal[] indexMeasurements = new decimal[4];
        private decimal[] middleMeasurements = new decimal[4];
        private decimal[] ringMeasurements = new decimal[4];

        List<person> allPersons = new List<person>();// List to serialize
        List<person> knnList = new List<person>();

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        int scanCount;

        // Return the List of objects created in the frmControl Class
        public List<person> getPersonList()
        {
            return allPersons;
        }

        
       public void readCSV()
        {
            

            decimal[] distalThumbMeasurements = new decimal[5];
            decimal[] intermediateThumbMeasurements = new decimal[5];
            decimal[] proximalThumbMeasurements = new decimal[5];

            decimal[] distalIndexMeasurements = new decimal[5];
            decimal[] intermediateIndexMeasurements = new decimal[5];
            decimal[] proximalIndexMeasurements = new decimal[5];
            decimal[] metacarpalIndexMeasurements = new decimal[5];

            decimal[] distalMiddleMeasurements = new decimal[5];
            decimal[] intermediateMiddleMeasurements = new decimal[5];
            decimal[] proximalMiddleMeasurements = new decimal[5];
            decimal[] metacarpalMiddleMeasurements = new decimal[5];

            decimal[] distalRingMeasurements = new decimal[5];
            decimal[] intermediateRingMeasurements = new decimal[5];
            decimal[] proximalRingMeasurements = new decimal[5];
            decimal[] metacarpalRingMeasurements = new decimal[5];

            decimal[] distalPinkyMeasurements = new decimal[5];
            decimal[] intermediatePinkyMeasurements = new decimal[5];
            decimal[] proximalPinkyMeasurements = new decimal[5];
            decimal[] metacarpalPinkyMeasurements = new decimal[5];

            try
            {
                myWriter.Flush();
                myWriter.Close();
            }
            catch (Exception) { }
            
            
            TextReader myReader = File.OpenText("F:\\VISUAL STUDIO PROJECTS\\LEAP EXTRACT\\Leap_Extract\\DataStructures\\People_Data.csv");

            var parser = new CsvParser(myReader);

            bool flag3 = true;

            while (flag3)
            {
                var csvRow = parser.Read();

                if (csvRow == null)
                {
                    flag3 = false;
                }
                else
                {
                    String[] temp = csvRow;
                    String pName = temp[0];
                    String pUname = temp[1];
                    int pAge = Convert.ToInt32(temp[2]);
                    char pGender = Convert.ToChar(temp[3]);

                    int counter = 1;
                    foreach (String item in temp)
                    {
                        txtDisplay.AppendText(Convert.ToString(counter) + ": "  + item + Environment.NewLine);
                        counter++;
                    }

                    try
                    {
                        // Assign all the stored decimal values directly to the arrays.
                        // First the thumb measurements
                        distalThumbMeasurements[0] = Convert.ToDecimal(temp[7]);
                        distalThumbMeasurements[1] = Convert.ToDecimal(temp[8]);
                        distalThumbMeasurements[2] = Convert.ToDecimal(temp[9]);
                        distalThumbMeasurements[3] = Convert.ToDecimal(temp[10]);

                        intermediateThumbMeasurements[0] = Convert.ToDecimal(temp[12]);
                        intermediateThumbMeasurements[1] = Convert.ToDecimal(temp[13]);
                        intermediateThumbMeasurements[2] = Convert.ToDecimal(temp[14]);
                        intermediateThumbMeasurements[3] = Convert.ToDecimal(temp[15]);

                        proximalThumbMeasurements[0] = Convert.ToDecimal(temp[17]);
                        proximalThumbMeasurements[1] = Convert.ToDecimal(temp[18]);
                        proximalThumbMeasurements[2] = Convert.ToDecimal(temp[19]);
                        proximalThumbMeasurements[3] = Convert.ToDecimal(temp[20]);

                        //Then the Index
                        distalIndexMeasurements[0] = Convert.ToDecimal(temp[23]);
                        distalIndexMeasurements[1] = Convert.ToDecimal(temp[24]);
                        distalIndexMeasurements[2] = Convert.ToDecimal(temp[25]);
                        distalIndexMeasurements[3] = Convert.ToDecimal(temp[26]);

                        intermediateIndexMeasurements[0] = Convert.ToDecimal(temp[28]);
                        intermediateIndexMeasurements[1] = Convert.ToDecimal(temp[29]);
                        intermediateIndexMeasurements[2] = Convert.ToDecimal(temp[30]);
                        intermediateIndexMeasurements[3] = Convert.ToDecimal(temp[31]);

                        proximalIndexMeasurements[0] = Convert.ToDecimal(temp[33]);
                        proximalIndexMeasurements[1] = Convert.ToDecimal(temp[34]);
                        proximalIndexMeasurements[2] = Convert.ToDecimal(temp[35]);
                        proximalIndexMeasurements[3] = Convert.ToDecimal(temp[36]);

                        metacarpalIndexMeasurements[0] = Convert.ToDecimal(temp[38]);
                        metacarpalIndexMeasurements[1] = Convert.ToDecimal(temp[39]);
                        metacarpalIndexMeasurements[2] = Convert.ToDecimal(temp[40]);
                        metacarpalIndexMeasurements[3] = Convert.ToDecimal(temp[41]);

                        //Then the Middle
                        distalMiddleMeasurements[0] = Convert.ToDecimal(temp[44]);
                        distalMiddleMeasurements[1] = Convert.ToDecimal(temp[45]);
                        distalMiddleMeasurements[2] = Convert.ToDecimal(temp[46]);
                        distalMiddleMeasurements[3] = Convert.ToDecimal(temp[47]);

                        intermediateMiddleMeasurements[0] = Convert.ToDecimal(temp[49]);
                        intermediateMiddleMeasurements[1] = Convert.ToDecimal(temp[50]);
                        intermediateMiddleMeasurements[2] = Convert.ToDecimal(temp[51]);
                        intermediateMiddleMeasurements[3] = Convert.ToDecimal(temp[52]);

                        proximalMiddleMeasurements[0] = Convert.ToDecimal(temp[54]);
                        proximalMiddleMeasurements[1] = Convert.ToDecimal(temp[55]);
                        proximalMiddleMeasurements[2] = Convert.ToDecimal(temp[56]);
                        proximalMiddleMeasurements[3] = Convert.ToDecimal(temp[57]);

                        metacarpalMiddleMeasurements[0] = Convert.ToDecimal(temp[59]);
                        metacarpalMiddleMeasurements[1] = Convert.ToDecimal(temp[60]);
                        metacarpalMiddleMeasurements[2] = Convert.ToDecimal(temp[61]);
                        metacarpalMiddleMeasurements[3] = Convert.ToDecimal(temp[62]);

                        // Then the Ring
                        distalRingMeasurements[0] = Convert.ToDecimal(temp[65]);
                        distalRingMeasurements[1] = Convert.ToDecimal(temp[66]);
                        distalRingMeasurements[2] = Convert.ToDecimal(temp[67]);
                        distalRingMeasurements[3] = Convert.ToDecimal(temp[68]);

                        intermediateRingMeasurements[0] = Convert.ToDecimal(temp[70]);
                        intermediateRingMeasurements[1] = Convert.ToDecimal(temp[71]);
                        intermediateRingMeasurements[2] = Convert.ToDecimal(temp[72]);
                        intermediateRingMeasurements[3] = Convert.ToDecimal(temp[73]);

                        proximalRingMeasurements[0] = Convert.ToDecimal(temp[75]);
                        proximalRingMeasurements[1] = Convert.ToDecimal(temp[76]);
                        proximalRingMeasurements[2] = Convert.ToDecimal(temp[77]);
                        proximalRingMeasurements[3] = Convert.ToDecimal(temp[78]);

                        metacarpalRingMeasurements[0] = Convert.ToDecimal(temp[80]);
                        metacarpalRingMeasurements[1] = Convert.ToDecimal(temp[81]);
                        metacarpalRingMeasurements[2] = Convert.ToDecimal(temp[82]);
                        metacarpalRingMeasurements[3] = Convert.ToDecimal(temp[83]);

                        // Then the pinky
                        distalPinkyMeasurements[0] = Convert.ToDecimal(temp[86]);
                        distalPinkyMeasurements[1] = Convert.ToDecimal(temp[87]);
                        distalPinkyMeasurements[2] = Convert.ToDecimal(temp[88]);
                        distalPinkyMeasurements[3] = Convert.ToDecimal(temp[89]);

                        intermediatePinkyMeasurements[0] = Convert.ToDecimal(temp[91]);
                        intermediatePinkyMeasurements[1] = Convert.ToDecimal(temp[92]);
                        intermediatePinkyMeasurements[2] = Convert.ToDecimal(temp[93]);
                        intermediatePinkyMeasurements[3] = Convert.ToDecimal(temp[94]);

                        proximalPinkyMeasurements[0] = Convert.ToDecimal(temp[96]);
                        proximalPinkyMeasurements[1] = Convert.ToDecimal(temp[97]);
                        proximalPinkyMeasurements[2] = Convert.ToDecimal(temp[98]);
                        proximalPinkyMeasurements[3] = Convert.ToDecimal(temp[99]);

                        metacarpalPinkyMeasurements[0] = Convert.ToDecimal(temp[101]);
                        metacarpalPinkyMeasurements[1] = Convert.ToDecimal(temp[102]);
                        metacarpalPinkyMeasurements[2] = Convert.ToDecimal(temp[103]);
                        metacarpalPinkyMeasurements[3] = Convert.ToDecimal(temp[104]);
                    }
                    catch (Exception) { }

                        person personFromCSV = new person(pName, pUname, pAge, pGender);
                        try
                        {
                            personFromCSV.leftHand.SetHandMeasurements(distalThumbMeasurements, intermediateThumbMeasurements, proximalThumbMeasurements, distalPinkyMeasurements, intermediatePinkyMeasurements,
                                                                            proximalPinkyMeasurements, metacarpalPinkyMeasurements, distalIndexMeasurements, intermediateIndexMeasurements, proximalIndexMeasurements,
                                                                            metacarpalIndexMeasurements, distalMiddleMeasurements, intermediateMiddleMeasurements, proximalMiddleMeasurements, metacarpalMiddleMeasurements,
                                                                            distalRingMeasurements, intermediateRingMeasurements, proximalRingMeasurements, metacarpalRingMeasurements);
                        }
                        catch (Exception) { }   
                    
                    
                    try
                    {
                        allPersons.Add(personFromCSV);
                    }
                    catch (Exception) { }

                }
            }
            myReader.Close();

        } 
        
        public frmControl()
        {
            InitializeComponent();

            readCSV();
        }

        delegate void LeapEventDelegate(string EventName);

        public void LeapEventNotification(string EventName)
        {
            if (!this.InvokeRequired)
            {
                switch (EventName)
                {
                    case "onInit":
                        Debug.WriteLine("Init");
                        break;
                    case "onConnect":
                        this.connectHandler();
                        break;
                    case "onFrame":
                        if(!this.Disposing) 
                            this.newFrameHandler(this.controller.Frame());
                        break;
                }
            }
            else
            {
                BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        void connectHandler()
        {
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.Config.SetFloat("Gesture.Circle.MinRadius", 40.0f);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        }

        // Once the Listener is created for this session, the newFrameHandler method is called with each new frame
        void newFrameHandler(Frame frame)
        {

            
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    try
                    {
                        this.controller.RemoveListener(this.listener);
                        this.controller.Dispose();
                    }
                    catch (Exception)
                    { }
                    
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = progressBar1.Minimum;
            string name = "";
            string uName = "";
            
            // Ask person X to input their name
            while (name == "")
            {
                InputBox.Show("Name and Surname", "Please Enter Name and Surname", ref name);
            }

                bool uniqueFound = false;

                while(!uniqueFound)
                {
                    if(uName == "")
                        InputBox.Show("Email","Please enter a unique username/email", ref uName);

                    List<string> unames = new List<string>();

                    foreach(person p in  allPersons)
                        unames.Add(p.getUserName());
                    

                    if(!unames.Contains(uName))
                        uniqueFound = true;   
                    else
                        InputBox.Show("Email", uName + " is already registered. Please enter a unique username/email", ref uName);
                }
            
            // Input the age of person X
            // Initialize age as -1 to ensuer the execution of the while-loop
            int age = -1;
            while(age < 0)
            {
                var strage = "";
                InputBox.Show("Age", "Please Enter Age", ref strage);
                try
                {
                    age = Int32.Parse(strage);
                }
                catch (Exception) { }
            }

            // Input the gender of person X
            // Initialize the gender of person as 'Q' to ensure the while is executed
            char gender = 'Q';
            bool flag = true;
            while(flag)
            {
                var strgender = "";
                InputBox.Show("Gender", "Please Enter Gender ('M' or 'F')", ref strgender);
                try
                {
                    gender = char.Parse(strgender);
                }
                catch (Exception) { }

                if (gender == 'M' || gender == 'F')
                    flag = false;
            }
            // Create a new person object of the person class with the details gathered from the inputboxes
            // Create three person objects, each will be different by name alone
            person tmpPerson1 = new person(name + " 1", uName, age, gender);
            person tmpPerson2 = new person(name + " 2", uName, age, gender);
            person tmpPerson3 = new person(name + " 3", uName, age, gender);

            currentPerson = tmpPerson1;

            scanCount = 0;
            MessageBox.Show("Please place your left hand over the controller, and press Enter to start the scans.");
            long startTime = CurrentTimeMillis();
            
            // Create first object of same person
            bool flag1 = true;
            double timeElapsed;

            for (int a = 1; a <= 3; a++ )
            {
                if (a == 1)
                {
                    while (flag1)
                    {

                        timeElapsed = CurrentTimeMillis() - startTime;

                        if (timeElapsed > 10000)
                        {

                            MessageBox.Show("Finished first scan! Number of scans made: " + scanCount);

                            try
                            {
                                allPersons.Add(currentPerson);
                                CsvWrite();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                            finally
                            {
                                flag1 = false;
                                progressBar1.Value = progressBar1.Maximum;
                            }
                        }
                        else
                        {
                            frameGetExtractUpdate();
                            Thread.Sleep(50);
                        }

                    } // end of first object of first person
                } // end of a == 1
                else if (a == 2)
                {
                    flag1 = true;
                    currentPerson = tmpPerson2;
                    scanCount = 0;
                    startTime = CurrentTimeMillis();
                    progressBar1.Value = progressBar1.Minimum;

                    while (flag1)
                    {

                        timeElapsed = CurrentTimeMillis() - startTime;

                        if (timeElapsed > 10000)
                        {

                            MessageBox.Show("Finished second scan! Number of scans made: " + scanCount);

                            try
                            {
                                allPersons.Add(currentPerson);
                                CsvWrite();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                            finally
                            {
                                flag1 = false;
                                progressBar1.Value = progressBar1.Maximum;
                            }
                        }
                        else
                        {
                            frameGetExtractUpdate();
                            Thread.Sleep(50);
                        }

                    }

                }
                else if (a == 3)
                {
                    flag1 = true;
                    currentPerson = tmpPerson3;
                    scanCount = 0;
                    startTime = CurrentTimeMillis();
                    progressBar1.Value = progressBar1.Minimum;

                    while (flag1)
                    {

                        timeElapsed = CurrentTimeMillis() - startTime;

                        if (timeElapsed > 10000)
                        {

                            MessageBox.Show("Finished third scan! Number of scans made: " + scanCount);

                            try
                            {
                                allPersons.Add(currentPerson);
                                CsvWrite();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                            finally
                            {
                                flag1 = false;
                                progressBar1.Value = progressBar1.Maximum;
                            }
                        }
                        else
                        {
                            frameGetExtractUpdate();
                            Thread.Sleep(50);
                        }

                    }

                }
                
            }

            


        }

        // Method to write all the current Person object to the CSV file
        // Refer to the read method, in which all of the existing persons in the CSV file will be loaded 
        // into the allPersons list from the start of the application
        private void CsvWrite()
        {
          
            myWriter = File.AppendText("F:\\VISUAL STUDIO PROJECTS\\LEAP EXTRACT\\Leap_Extract\\DataStructures\\People_Data.csv");
            List<String> csvMesg = new List<String>();

            var csv = new CsvHelper.CsvWriter(myWriter);

            csvMesg.AddRange(currentPerson.csvToString());
            csvMesg.AddRange(currentPerson.leftHand.csvToString());

            foreach (String field in csvMesg)
            {
                try
                {
                    csv.WriteField(field);
                }
                catch (Exception g)
                {
                    Console.WriteLine(g.ToString());
                }
                
            }
            csv.NextRecord();

            myWriter.Flush();
            myWriter.Close();
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                myWriter.Flush();
                myWriter.Close();
            }
            catch (Exception) { }

            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String name = txtName.Text;
            txtDisplay.Clear();

            if (name == "")
            {
                txtDisplay.Clear();
            }
            else try
            {
                
                foreach (person personItem in allPersons)
                {
                    if (personItem.getName() == name)
                    {
                        txtDisplay.AppendText("Name: " + name + Environment.NewLine);
                        txtDisplay.AppendText("Username: " + personItem.getUserName() + Environment.NewLine);
                        txtDisplay.AppendText("Age: " + Convert.ToString(personItem.getAge()) + Environment.NewLine);
                        txtDisplay.AppendText("Gender: " + Convert.ToString(personItem.getGender()) + Environment.NewLine + Environment.NewLine);

                        txtDisplay.AppendText("Measurements: " + Environment.NewLine + personItem.leftHand.myToString() + Environment.NewLine);

                        break;
                    }
                }
            }
                catch (Exception t) 
                { 
                    Console.WriteLine(t.ToString()); 
                }

        }
      
        public void frameGetExtractUpdate()
    {
            scanCount++;
            Frame frame = controller.Frame();

            foreach (Hand hand in frame.Hands)
            {
                
                String handtype = hand.IsLeft ? "Left Hand" : "Right Hand";

                foreach (Finger finger in frame.Fingers)
                {
                    String ftype = finger.Type.ToString();
                    int fingerID = 0;

                    switch (ftype)
                    {
                        case "TYPE_INDEX":
                            fingerID = 1;
                            break;
                        case "TYPE_MIDDLE":
                            fingerID = 2;
                            break;
                        case "TYPE_RING":
                            fingerID = 3;
                            break;
                        case "TYPE_PINKY":
                            fingerID = 4;
                            break;
                        case "TYPE_THUMB":
                            fingerID = 5;
                            break;
                    }

                    int countBones = 0;

                    foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                    {
                        String bstringType = boneType.GetType().ToString();
                        Bone boneLength = finger.Bone(boneType);

                        switch (fingerID)
                        {
                            case 1:
                                indexMeasurements[countBones] = (decimal)boneLength.Length; 
                                break;
                            case 2:
                                middleMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 3:
                                ringMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 4:
                                pinkyMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 5:
                                {
                                    if (countBones > 0 && countBones < 4)
                                    {
                                        thumbMeasurements[countBones - 1] = (decimal)boneLength.Length;
                                    }
                                    else { }
                                    break;
                                }
                        }
                        countBones++;
                    } // Bones for

                } // End Fingers for

            } // End Hands for

            currentPerson.leftHand.UpdateHandMeasurements(thumbMeasurements, indexMeasurements, middleMeasurements, ringMeasurements, pinkyMeasurements);
            progressBar1.Increment(1);
        }

        private void frmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                myWriter.Flush();
                myWriter.Close();
            }
            catch (Exception) { }
        }

        

        public void getFrameSinglePerson()
        {
            scanCount2++;
            decimal[] singleThumbMeasurements = new decimal[3];
            decimal[] singlePinkyMeasurements = new decimal[4];
            decimal[] singleIndexMeasurements = new decimal[4];
            decimal[] singleMiddleMeasurements = new decimal[4];
            decimal[] singleRingMeasurements = new decimal[4];

            Frame frame3 = controller.Frame();

            foreach (Hand hand in frame3.Hands)
            {

                String handtype = hand.IsLeft ? "Left Hand" : "Right Hand";

                foreach (Finger finger in frame3.Fingers)
                {
                    String ftype = finger.Type.ToString();
                    int fingerID = 0;

                    switch (ftype)
                    {
                        case "TYPE_INDEX":
                            fingerID = 1;
                            break;
                        case "TYPE_MIDDLE":
                            fingerID = 2;
                            break;
                        case "TYPE_RING":
                            fingerID = 3;
                            break;
                        case "TYPE_PINKY":
                            fingerID = 4;
                            break;
                        case "TYPE_THUMB":
                            fingerID = 5;
                            break;
                    }

                    int countBones = 0;

                    foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                    {
                        String bstringType = boneType.GetType().ToString();
                        Bone boneLength = finger.Bone(boneType);

                        switch (fingerID)
                        {
                            case 1:
                                singleIndexMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 2:
                                singleMiddleMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 3:
                                singleRingMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 4:
                                singlePinkyMeasurements[countBones] = (decimal)boneLength.Length;
                                break;
                            case 5:
                                {
                                    if (countBones > 0 && countBones < 4)
                                    {
                                        singleThumbMeasurements[countBones - 1] = (decimal)boneLength.Length;
                                    }
                                    else { }
                                    break;
                                }
                        }
                        countBones++;
                    } // Bones for

                } // End Fingers for

            } // End Hands for

            singlePerson.leftHand.UpdateHandMeasurements(singleThumbMeasurements, singleIndexMeasurements, singleMiddleMeasurements, singleRingMeasurements, singlePinkyMeasurements);
        }

        int scanCount2;

        private void button1_Click_1(object sender, EventArgs e)
        {
            long startTime2 = CurrentTimeMillis();
            long timeElapsed2;
            bool flag4 = true;
            scanCount2 = 0;

            K_NN scanHand;

            while (flag4)
            {

                timeElapsed2 = CurrentTimeMillis() - startTime2;

                if (timeElapsed2 > 60000)
                {

                    MessageBox.Show("Fineshed the real time scan. Number of scans in 1 minute: " + scanCount2);
                    flag4 = false;

                }
                else
                {
                    getFrameSinglePerson();
                    try
                    {
                        scanHand = new K_NN();
                        knnList = scanHand.getNearestNeighbors(singlePerson, allPersons);
                        
                        txtDisplay.Clear();
                        txtDisplay.AppendText(knnList[0].getName());
                        txtDisplay.AppendText(knnList[1].getName());
                        txtDisplay.AppendText(knnList[2].getName());

                    }
                    catch (Exception h) { Console.WriteLine(h.ToString()); }

                    Thread.Sleep(3000);
                }
            }
        
        }

 }

    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }

    public class LeapEventListener : Listener
    {
        ILeapEventDelegate eventDelegate;

        public LeapEventListener(ILeapEventDelegate delegateObject)
        {
            this.eventDelegate = delegateObject;
        }
        public override void OnInit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onConnect");
        }
        public override void OnFrame(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onDisconnect");
        }
    }


}
