using System;
using System.Windows.Forms;
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
        string msg;
        int thisPersonIndex;

        public K_NN()
        {

        }

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
            

        }

        public List<person> getNearestNeighbors(person thisPerson, List<person> listIn)
        {

            //MessageBox.Show(Convert.ToString(listIn[0].leftHand.getThumb().getFingerParts()[1].trimmedAverage)); 

            List<ds_phalanx> thumb_distal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> thumb_intermediate_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> thumb_proximal_phalanxes = new List<ds_phalanx>();

            List<ds_phalanx> index_distal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> index_intermediate_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> index_proximal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> index_metacarpal_phalanxes = new List<ds_phalanx>();

            List<ds_phalanx> middle_distal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> middle_intermediate_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> middle_proximal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> middle_metacarpal_phalanxes = new List<ds_phalanx>();

            List<ds_phalanx> ring_distal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> ring_intermediate_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> ring_proximal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> ring_metacarpal_phalanxes = new List<ds_phalanx>();

            List<ds_phalanx> pinky_distal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> pinky_intermediate_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> pinky_proximal_phalanxes = new List<ds_phalanx>();
            List<ds_phalanx> pinky_metacarpal_phalanxes = new List<ds_phalanx>();

                foreach(person p in listIn)
                {
                    p.knn_score = 0;

                    thumb_distal_phalanxes.Add(p.leftHand.getThumb().getFingerParts()[0]);
                    thumb_intermediate_phalanxes.Add(p.leftHand.getThumb().getFingerParts()[1]);
                    thumb_proximal_phalanxes.Add(p.leftHand.getThumb().getFingerParts()[2]);

                    index_distal_phalanxes.Add(p.leftHand.getIndex().getFingerParts()[0]);
                    index_intermediate_phalanxes.Add(p.leftHand.getIndex().getFingerParts()[1]);
                    index_proximal_phalanxes.Add(p.leftHand.getIndex().getFingerParts()[2]);
                    index_metacarpal_phalanxes.Add(p.leftHand.getIndex().getFingerParts()[3]);

                    middle_distal_phalanxes.Add(p.leftHand.getMiddle().getFingerParts()[0]);
                    middle_intermediate_phalanxes.Add(p.leftHand.getMiddle().getFingerParts()[1]);
                    middle_proximal_phalanxes.Add(p.leftHand.getMiddle().getFingerParts()[2]);
                    middle_metacarpal_phalanxes.Add(p.leftHand.getMiddle().getFingerParts()[3]);

                    ring_distal_phalanxes.Add(p.leftHand.getRing().getFingerParts()[0]);
                    ring_intermediate_phalanxes.Add(p.leftHand.getRing().getFingerParts()[1]);
                    ring_proximal_phalanxes.Add(p.leftHand.getRing().getFingerParts()[2]);
                    ring_metacarpal_phalanxes.Add(p.leftHand.getRing().getFingerParts()[3]);

                    pinky_distal_phalanxes.Add(p.leftHand.getPinky().getFingerParts()[0]);
                    pinky_intermediate_phalanxes.Add(p.leftHand.getPinky().getFingerParts()[1]);
                    pinky_proximal_phalanxes.Add(p.leftHand.getPinky().getFingerParts()[2]);
                    pinky_metacarpal_phalanxes.Add(p.leftHand.getPinky().getFingerParts()[3]);
                }

              //-------thumb
              /*  MessageBox.Show("THIS PERSON:\t" + thisPerson.leftHand.getThumb().getFingerParts()[0].trimmedAverage + Environment.NewLine + 
                    thumb_distal_phalanxes[4].owner.getName()+ ":\t" + thumb_distal_phalanxes[0].trimmedAverage+ 
                    Environment.NewLine + "RESULT\t:" + thisPerson.leftHand.getThumb().getFingerParts()[0].compareTrimmedAverage(thumb_distal_phalanxes[4].trimmedAverage).ToString());
                */
                thumb_distal_phalanxes = thumb_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[0].trimmedAverage)).ToList();
                thumb_intermediate_phalanxes = thumb_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[1].trimmedAverage)).ToList();              
                thumb_proximal_phalanxes = thumb_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[2].trimmedAverage)).ToList();
              
            //thumb doesnt have metacarpal

            //-------index
                index_distal_phalanxes = index_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[0].trimmedAverage)).ToList();
                index_intermediate_phalanxes = index_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[1].trimmedAverage)).ToList();
                index_proximal_phalanxes = index_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[2].trimmedAverage)).ToList();
                index_metacarpal_phalanxes = index_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[3].trimmedAverage)).ToList();
               
            //-------middle
                middle_distal_phalanxes = middle_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[0].trimmedAverage)).ToList(); 
                middle_intermediate_phalanxes = middle_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[1].trimmedAverage)).ToList(); 
                middle_proximal_phalanxes = middle_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[2].trimmedAverage)).ToList(); 
                middle_metacarpal_phalanxes = middle_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[3].trimmedAverage)).ToList(); 
               
            //-------ring
                ring_distal_phalanxes = ring_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[0].trimmedAverage)).ToList(); 
                ring_intermediate_phalanxes = ring_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[1].trimmedAverage)).ToList();   
                ring_proximal_phalanxes = ring_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[2].trimmedAverage)).ToList(); 
                ring_metacarpal_phalanxes = ring_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[3].trimmedAverage)).ToList();   
            
            //-------pinky
                pinky_distal_phalanxes = pinky_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[0].trimmedAverage)).ToList(); 
                pinky_intermediate_phalanxes = pinky_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[1].trimmedAverage)).ToList();  
                pinky_proximal_phalanxes = pinky_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[2].trimmedAverage)).ToList(); 
                pinky_metacarpal_phalanxes = pinky_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[3].trimmedAverage)).ToList();

            //implement scoring

                /*
                string msg = "";
                foreach (ds_phalanx p in thumb_distal_phalanxes)
                    msg += p.owner.getUserName() + "\t" + p.trimmedAverage + Environment.NewLine;

                MessageBox.Show(msg);*/

                for (int k = 0; k < listIn.Count; k++)
                {
                    int score = listIn.Count - k;
                    thumb_distal_phalanxes.ElementAt(k).owner.knn_score += score;
                    thumb_intermediate_phalanxes.ElementAt(k).owner.knn_score += score;
                    thumb_proximal_phalanxes.ElementAt(k).owner.knn_score += score;

                    index_distal_phalanxes.ElementAt(k).owner.knn_score += score;
                    index_intermediate_phalanxes.ElementAt(k).owner.knn_score += score;
                    index_proximal_phalanxes.ElementAt(k).owner.knn_score += score;
                    index_metacarpal_phalanxes.ElementAt(k).owner.knn_score += score;

                     middle_distal_phalanxes.ElementAt(k).owner.knn_score += score;
                     middle_intermediate_phalanxes.ElementAt(k).owner.knn_score += score;
                     middle_proximal_phalanxes.ElementAt(k).owner.knn_score += score;
                     middle_metacarpal_phalanxes.ElementAt(k).owner.knn_score += score;

                     ring_distal_phalanxes.ElementAt(k).owner.knn_score += score;
                     ring_intermediate_phalanxes.ElementAt(k).owner.knn_score += score;
                     ring_proximal_phalanxes.ElementAt(k).owner.knn_score += score;
                     ring_metacarpal_phalanxes.ElementAt(k).owner.knn_score += score;

                     pinky_distal_phalanxes.ElementAt(k).owner.knn_score += score;
                     pinky_intermediate_phalanxes.ElementAt(k).owner.knn_score += score;
                     pinky_proximal_phalanxes.ElementAt(k).owner.knn_score += score;
                     pinky_metacarpal_phalanxes.ElementAt(k).owner.knn_score += score;
                }

                List<person> outList = listIn.OrderByDescending(o => o.knn_score).ToList();

                int firstScore = 0, secondScore = 0, thirdScore = 0, total = 0;

                try
                {
                    firstScore = outList[0].knn_score;
                    secondScore = outList[1].knn_score;
                    thirdScore = outList[2].knn_score;
                    total = firstScore + secondScore + thirdScore;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                double persentageOne = 0, persentageTwo = 0, persentageThree = 0;
                try
                {
                    persentageOne = (double)firstScore / total * 100;
                    persentageTwo = (double)secondScore / total * 100;
                    persentageThree = (double)thirdScore / total * 100;
                }
                catch (Exception i)
                {
                    Console.WriteLine(i.Message);
                }


                msg += outList[0].getName() + Environment.NewLine + 
                        "\t K-NN Score" + "\t Persentage score" + Environment.NewLine + 
                        "\t" + outList[0].knn_score + "\t\t" + String.Format("{0:#,#.##}", persentageOne) + "%" + Environment.NewLine;
                msg += outList[1].getName() + Environment.NewLine + 
                        "\t K-NN Score" + "\t Persentage score" + Environment.NewLine + 
                        "\t" + outList[1].knn_score + "\t\t" + String.Format("{0:#,#.##}", persentageTwo) + "%" + Environment.NewLine;
                msg += outList[2].getName() + Environment.NewLine + 
                        "\t K-NN Score" + "\t Persentage score" + Environment.NewLine + 
                        "\t" + outList[2].knn_score + "\t\t" + String.Format("{0:#,#.##}", persentageThree) + "%" + Environment.NewLine;
                
                return outList;
        }

        public string getMessage()
        {
            return this.msg;
        }


    }
}
