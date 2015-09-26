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

                int indx_thumb_distal_phalanxes = 0;
                int indx_thumb_intermediate_phalanxes = 0;
                int indx_thumb_proximal_phalanxes = 0;

                int indx_index_distal_phalanxes = 0;
                int indx_index_intermediate_phalanxes = 0;
                int indx_index_proximal_phalanxes = 0;
                int indx_index_metacarpal_phalanxes = 0;

                int indx_middle_distal_phalanxes = 0;
                int indx_middle_intermediate_phalanxes = 0;
                int indx_middle_proximal_phalanxes = 0;
                int indx_middle_metacarpal_phalanxes = 0;

                int indx_pinky_distal_phalanxes = 0;
                int indx_pinky_intermediate_phalanxes = 0;
                int indx_pinky_proximal_phalanxes = 0;
                int indx_pinky_metacarpal_phalanxes = 0;

                int indx_ring_distal_phalanxes = 0;
                int indx_ring_intermediate_phalanxes = 0;
                int indx_ring_proximal_phalanxes = 0;
                int indx_ring_metacarpal_phalanxes = 0;

                

            //-------thumb
                thumb_distal_phalanxes = thumb_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[0].getTrimmedAverage())).ToList();
                foreach( ds_phalanx ph in thumb_distal_phalanxes)
                    if (ph.owner.Equals(thisPerson))
                        indx_thumb_distal_phalanxes = thumb_distal_phalanxes.IndexOf(ph);

                thumb_intermediate_phalanxes = thumb_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[1].getTrimmedAverage())).ToList();              
                foreach(ds_phalanx ph in thumb_intermediate_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_thumb_intermediate_phalanxes = thumb_intermediate_phalanxes.IndexOf(ph);

                thumb_proximal_phalanxes = thumb_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getThumb().getFingerParts()[2].getTrimmedAverage())).ToList();
                foreach(ds_phalanx ph in thumb_proximal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_thumb_proximal_phalanxes = thumb_proximal_phalanxes.IndexOf(ph);

            //thumb doesnt have metacarpal

            //-------index
                index_distal_phalanxes = index_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[0].getTrimmedAverage())).ToList();
                foreach(ds_phalanx ph in index_distal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_index_distal_phalanxes = index_distal_phalanxes.IndexOf(ph);

                index_intermediate_phalanxes = index_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.leftHand.getIndex().getFingerParts()[1].getTrimmedAverage())).ToList();
                foreach(ds_phalanx ph in index_intermediate_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_index_intermediate_phalanxes = index_intermediate_phalanxes.IndexOf(ph);

                index_proximal_phalanxes = index_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList();
                foreach(ds_phalanx ph in index_proximal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_index_proximal_phalanxes = index_proximal_phalanxes.IndexOf(ph);

                index_metacarpal_phalanxes = index_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList();
                foreach(ds_phalanx ph in index_metacarpal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_index_metacarpal_phalanxes = index_metacarpal_phalanxes.IndexOf(ph);
            //-------middle
                middle_distal_phalanxes = middle_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in middle_distal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_middle_distal_phalanxes = middle_distal_phalanxes.IndexOf(ph);

                middle_intermediate_phalanxes = middle_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in middle_intermediate_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_middle_intermediate_phalanxes = middle_intermediate_phalanxes.IndexOf(ph);


                middle_proximal_phalanxes = middle_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in middle_proximal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_middle_proximal_phalanxes = middle_proximal_phalanxes.IndexOf(ph);

                middle_metacarpal_phalanxes = middle_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in middle_metacarpal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_middle_metacarpal_phalanxes = middle_metacarpal_phalanxes.IndexOf(ph);

            //-------ring
                ring_distal_phalanxes = ring_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 

                foreach(ds_phalanx ph in ring_distal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_ring_distal_phalanxes = ring_distal_phalanxes.IndexOf(ph);

                ring_intermediate_phalanxes = ring_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in ring_intermediate_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_ring_intermediate_phalanxes = ring_intermediate_phalanxes.IndexOf(ph);

                ring_proximal_phalanxes = ring_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in ring_proximal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_ring_proximal_phalanxes = ring_proximal_phalanxes.IndexOf(ph);


                ring_metacarpal_phalanxes = ring_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 
                foreach(ds_phalanx ph in ring_metacarpal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_ring_metacarpal_phalanxes = ring_metacarpal_phalanxes.IndexOf(ph);
            //-------pinky
                pinky_distal_phalanxes = pinky_distal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 

                foreach(ds_phalanx ph in pinky_distal_phalanxes)
                    if(ph.owner.Equals(thisPerson))
                        indx_pinky_distal_phalanxes = pinky_distal_phalanxes.IndexOf(ph);

                pinky_intermediate_phalanxes = pinky_intermediate_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 

                foreach (ds_phalanx ph in pinky_intermediate_phalanxes)
                    if (ph.owner.Equals(thisPerson))
                        indx_pinky_intermediate_phalanxes = pinky_intermediate_phalanxes.IndexOf(ph);

                pinky_proximal_phalanxes = pinky_proximal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 

                foreach (ds_phalanx ph in pinky_proximal_phalanxes)
                    if (ph.owner.Equals(thisPerson))
                        indx_pinky_proximal_phalanxes = pinky_proximal_phalanxes.IndexOf(ph);

                pinky_metacarpal_phalanxes = pinky_metacarpal_phalanxes.OrderBy(o => o.compareTrimmedAverage(thisPerson.trimmedAverage)).ToList(); 

                foreach (ds_phalanx ph in pinky_metacarpal_phalanxes)
                    if (ph.owner.Equals(thisPerson))
                        indx_pinky_metacarpal_phalanxes = pinky_metacarpal_phalanxes.IndexOf(ph);

            //implement scoring
            // 1. THUMB $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

            // 1.1. Distal

            decimal difTop, difBot;

            try
            {

                difTop = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[0].getTrimmedAverage() - thumb_distal_phalanxes.ElementAt(indx_thumb_distal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[0].getTrimmedAverage() - thumb_distal_phalanxes.ElementAt(indx_thumb_distal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if(difTop >= difBot)
                    thumb_distal_phalanxes.ElementAt(indx_thumb_distal_phalanxes - 1).owner.knn_score++;
                else
                    thumb_distal_phalanxes.ElementAt(indx_thumb_distal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 1.2. Intermediate
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[1].getTrimmedAverage() - thumb_intermediate_phalanxes.ElementAt(indx_thumb_intermediate_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[1].getTrimmedAverage() - thumb_intermediate_phalanxes.ElementAt(indx_thumb_intermediate_phalanxes - 1).getTrimmedAverage());
                if( difTop > 0 && difBot > 0)
                    if (difTop >= difBot)
                        thumb_intermediate_phalanxes.ElementAt(indx_thumb_intermediate_phalanxes - 1).owner.knn_score++;
                    else
                        thumb_intermediate_phalanxes.ElementAt(indx_thumb_intermediate_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 1.3. Proximal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[2].getTrimmedAverage() - thumb_proximal_phalanxes.ElementAt(indx_thumb_proximal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getThumb().getFingerParts()[2].getTrimmedAverage() - thumb_proximal_phalanxes.ElementAt(indx_thumb_proximal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    thumb_proximal_phalanxes.ElementAt(indx_thumb_proximal_phalanxes - 1).owner.knn_score++;
                else
                    thumb_proximal_phalanxes.ElementAt(indx_thumb_proximal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 2. INDEX $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            // 2.1. Distal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[0].getTrimmedAverage() - index_distal_phalanxes.ElementAt(indx_index_distal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[0].getTrimmedAverage() - index_distal_phalanxes.ElementAt(indx_index_distal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    index_distal_phalanxes.ElementAt(indx_index_distal_phalanxes - 1).owner.knn_score++;
                else
                    index_distal_phalanxes.ElementAt(indx_index_distal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 2.2. Intermediate
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[1].getTrimmedAverage() - index_intermediate_phalanxes.ElementAt(indx_index_intermediate_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[1].getTrimmedAverage() - index_intermediate_phalanxes.ElementAt(indx_index_intermediate_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    index_intermediate_phalanxes.ElementAt(indx_index_intermediate_phalanxes - 1).owner.knn_score++;
                else
                    index_intermediate_phalanxes.ElementAt(indx_index_intermediate_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 2.3. Proximal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[2].getTrimmedAverage() - index_proximal_phalanxes.ElementAt(indx_index_proximal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[2].getTrimmedAverage() - index_proximal_phalanxes.ElementAt(indx_index_proximal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    index_proximal_phalanxes.ElementAt(indx_index_proximal_phalanxes - 1).owner.knn_score++;
                else
                    index_proximal_phalanxes.ElementAt(indx_index_proximal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 2.4. Metacarpal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[3].getTrimmedAverage() - index_metacarpal_phalanxes.ElementAt(indx_index_metacarpal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getIndex().getFingerParts()[3].getTrimmedAverage() - index_metacarpal_phalanxes.ElementAt(indx_index_metacarpal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    index_metacarpal_phalanxes.ElementAt(indx_index_metacarpal_phalanxes - 1).owner.knn_score++;
                else
                    index_metacarpal_phalanxes.ElementAt(indx_index_metacarpal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            // 3. MIDDLE $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            // 3.1. Distal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[0].getTrimmedAverage() - middle_distal_phalanxes.ElementAt(indx_middle_distal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[0].getTrimmedAverage() - middle_distal_phalanxes.ElementAt(indx_middle_distal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    middle_distal_phalanxes.ElementAt(indx_middle_distal_phalanxes - 1).owner.knn_score++;
                else
                    middle_distal_phalanxes.ElementAt(indx_middle_distal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 3.2. Intermediate
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[1].getTrimmedAverage() - middle_intermediate_phalanxes.ElementAt(indx_middle_intermediate_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[1].getTrimmedAverage() - middle_intermediate_phalanxes.ElementAt(indx_middle_intermediate_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    middle_intermediate_phalanxes.ElementAt(indx_middle_intermediate_phalanxes - 1).owner.knn_score++;
                else
                    middle_intermediate_phalanxes.ElementAt(indx_middle_intermediate_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 3.3. Proximal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[2].getTrimmedAverage() - middle_proximal_phalanxes.ElementAt(indx_middle_proximal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[2].getTrimmedAverage() - middle_proximal_phalanxes.ElementAt(indx_middle_proximal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    middle_proximal_phalanxes.ElementAt(indx_middle_proximal_phalanxes - 1).owner.knn_score++;
                else
                    middle_proximal_phalanxes.ElementAt(indx_middle_proximal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 3.4. Metacarpal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[3].getTrimmedAverage() - middle_metacarpal_phalanxes.ElementAt(indx_middle_metacarpal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getMiddle().getFingerParts()[3].getTrimmedAverage() - middle_metacarpal_phalanxes.ElementAt(indx_middle_metacarpal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    middle_metacarpal_phalanxes.ElementAt(indx_middle_metacarpal_phalanxes - 1).owner.knn_score++;
                else
                    middle_metacarpal_phalanxes.ElementAt(indx_middle_metacarpal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 4. RING $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            // 4.1. Distal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[0].getTrimmedAverage() - ring_distal_phalanxes.ElementAt(indx_ring_distal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[0].getTrimmedAverage() - ring_distal_phalanxes.ElementAt(indx_ring_distal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    ring_distal_phalanxes.ElementAt(indx_ring_distal_phalanxes - 1).owner.knn_score++;
                else
                    ring_distal_phalanxes.ElementAt(indx_ring_distal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 4.2. Intermediate
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[1].getTrimmedAverage() - ring_intermediate_phalanxes.ElementAt(indx_ring_intermediate_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[1].getTrimmedAverage() - ring_intermediate_phalanxes.ElementAt(indx_ring_intermediate_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    ring_intermediate_phalanxes.ElementAt(indx_ring_intermediate_phalanxes - 1).owner.knn_score++;
                else
                    ring_intermediate_phalanxes.ElementAt(indx_ring_intermediate_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 4.3. Proximal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[2].getTrimmedAverage() - ring_proximal_phalanxes.ElementAt(indx_ring_proximal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[2].getTrimmedAverage() - ring_proximal_phalanxes.ElementAt(indx_ring_proximal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    ring_proximal_phalanxes.ElementAt(indx_ring_proximal_phalanxes - 1).owner.knn_score++;
                else
                    ring_proximal_phalanxes.ElementAt(indx_ring_proximal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 4.4. Metacarpal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[3].getTrimmedAverage() - ring_metacarpal_phalanxes.ElementAt(indx_ring_metacarpal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getRing().getFingerParts()[3].getTrimmedAverage() - ring_metacarpal_phalanxes.ElementAt(indx_ring_metacarpal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    ring_metacarpal_phalanxes.ElementAt(indx_ring_metacarpal_phalanxes - 1).owner.knn_score++;
                else
                    ring_metacarpal_phalanxes.ElementAt(indx_ring_metacarpal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 5. PINKY $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            // 5.1. Distal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[0].getTrimmedAverage() - pinky_distal_phalanxes.ElementAt(indx_pinky_distal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[0].getTrimmedAverage() - pinky_distal_phalanxes.ElementAt(indx_pinky_distal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    pinky_distal_phalanxes.ElementAt(indx_pinky_distal_phalanxes - 1).owner.knn_score++;
                else
                    pinky_distal_phalanxes.ElementAt(indx_pinky_distal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 5.2. Intermediate
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[1].getTrimmedAverage() - pinky_intermediate_phalanxes.ElementAt(indx_pinky_intermediate_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[1].getTrimmedAverage() - pinky_intermediate_phalanxes.ElementAt(indx_pinky_intermediate_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    pinky_intermediate_phalanxes.ElementAt(indx_pinky_intermediate_phalanxes - 1).owner.knn_score++;
                else
                    pinky_intermediate_phalanxes.ElementAt(indx_pinky_intermediate_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            // 5.3. Proximal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[2].getTrimmedAverage() - pinky_proximal_phalanxes.ElementAt(indx_pinky_proximal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[2].getTrimmedAverage() - pinky_proximal_phalanxes.ElementAt(indx_pinky_proximal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    pinky_proximal_phalanxes.ElementAt(indx_pinky_proximal_phalanxes - 1).owner.knn_score++;
                else
                    pinky_proximal_phalanxes.ElementAt(indx_pinky_proximal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }



            // 5.4. Metacarpal
            try
            {
                difTop = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[3].getTrimmedAverage() - pinky_metacarpal_phalanxes.ElementAt(indx_pinky_metacarpal_phalanxes + 1).getTrimmedAverage());
                difBot = Math.Abs(thisPerson.leftHand.getPinky().getFingerParts()[3].getTrimmedAverage() - pinky_metacarpal_phalanxes.ElementAt(indx_pinky_metacarpal_phalanxes - 1).getTrimmedAverage());
                if (difTop > 0 && difBot > 0)
                if (difTop >= difBot)
                    pinky_metacarpal_phalanxes.ElementAt(indx_pinky_metacarpal_phalanxes - 1).owner.knn_score++;
                else
                    pinky_metacarpal_phalanxes.ElementAt(indx_pinky_metacarpal_phalanxes + 1).owner.knn_score++;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

             
                //string thisPersonName = thisPerson.getUserName();

            outList = listIn.OrderBy(o => o.knn_score).ToList();             
            return outList;
        }


    }
}
