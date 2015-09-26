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
            List<person> outList = new List<person>();

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
                thumb_distal_phalanxes = thumb_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[0].getTrimmedAverage())).ToList();

                thumb_intermediate_phalanxes = thumb_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[1].getTrimmedAverage())).ToList();              
               
                thumb_proximal_phalanxes = thumb_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[2].getTrimmedAverage())).ToList();
               
            //thumb doesnt have metacarpal

            //-------index
                index_distal_phalanxes = index_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[0].getTrimmedAverage())).ToList();
               
                index_intermediate_phalanxes = index_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[1].getTrimmedAverage())).ToList();

                index_proximal_phalanxes = index_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[2].getTrimmedAverage())).ToList();
                
                index_metacarpal_phalanxes = index_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[3].getTrimmedAverage())).ToList();
               
            //-------middle
                middle_distal_phalanxes = middle_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[0].getTrimmedAverage())).ToList(); 
              
                middle_intermediate_phalanxes = middle_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[1].getTrimmedAverage())).ToList(); 
               
                middle_proximal_phalanxes = middle_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[2].getTrimmedAverage())).ToList(); 
                
                middle_metacarpal_phalanxes = middle_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getMiddle().getFingerParts()[3].getTrimmedAverage())).ToList(); 
               
            //-------ring
                ring_distal_phalanxes = ring_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[0].getTrimmedAverage())).ToList(); 

                ring_intermediate_phalanxes = ring_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[1].getTrimmedAverage())).ToList(); 
               
                ring_proximal_phalanxes = ring_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[2].getTrimmedAverage())).ToList(); 

                ring_metacarpal_phalanxes = ring_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getRing().getFingerParts()[3].getTrimmedAverage())).ToList();   
            //-------pinky
                pinky_distal_phalanxes = pinky_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[0].getTrimmedAverage())).ToList(); 

                pinky_intermediate_phalanxes = pinky_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[1].getTrimmedAverage())).ToList();  

                pinky_proximal_phalanxes = pinky_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[2].getTrimmedAverage())).ToList(); 
    
                pinky_metacarpal_phalanxes = pinky_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getPinky().getFingerParts()[3].getTrimmedAverage())).ToList(); 

            //implement scoring

            

            outList = listIn.OrderBy(o => o.knn_score).ToList();             
            return outList;
        }


    }
}
