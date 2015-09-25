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
    /*public class ds_HandCompare
    {

        ds_hand handOne;
        ds_hand handTwo;
        double similarityScore;
        int simCriteria;
        int penalty;


        public ds_HandCompare(ds_hand h1, ds_hand h2, int simCrit)
        {
            this.handOne = h1;
            this.handTwo = h2;
            if (simCrit > 0 && simCrit <= 100)
            {
                this.simCriteria = simCrit;

                if (simCrit > 80)
                    penalty = 4;
                else if (simCrit > 60)
                    penalty = 3;
                else if (simCrit > 40)
                    penalty = 2;
                else
                    penalty = 1;
            }
            else
                throw new Exception("Similarity Criteria value must be between 1 and 100");
        }


        public double getSimilarityScore()
        {
            if (!handOne.Equals(handTwo))
            {
                similarityScore = 100;
                //calculate similarity
                //compare thumbs
                for (int k = 0; k < handOne.getThumb().getFingerParts().Length; k++)
                {
                    if (this.getRatio(handOne.getThumb().getFingerParts()[k].min,
                                     handTwo.getThumb().getFingerParts()[k].min) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getThumb().getFingerParts()[k].max,
                             handTwo.getThumb().getFingerParts()[k].max) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getThumb().getFingerParts()[k].avg,
                             handTwo.getThumb().getFingerParts()[k].avg) < simCriteria)
                        similarityScore -= penalty;
                }
                //compare indexes
                for (int k = 0; k < handOne.getIndex().getFingerParts().Length; k++)
                {
                    if (this.getRatio(handOne.getIndex().getFingerParts()[k].min,
                                     handTwo.getIndex().getFingerParts()[k].min) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getIndex().getFingerParts()[k].max,
                             handTwo.getIndex().getFingerParts()[k].max) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getIndex().getFingerParts()[k].avg,
                             handTwo.getIndex().getFingerParts()[k].avg) < simCriteria)
                        similarityScore -= penalty;
                }
                //compare middles
                for (int k = 0; k < handOne.getMiddle().getFingerParts().Length; k++)
                {
                    if (this.getRatio(handOne.getMiddle().getFingerParts()[k].min,
                                     handTwo.getMiddle().getFingerParts()[k].min) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getMiddle().getFingerParts()[k].max,
                             handTwo.getMiddle().getFingerParts()[k].max) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getMiddle().getFingerParts()[k].avg,
                             handTwo.getMiddle().getFingerParts()[k].avg) < simCriteria)
                        similarityScore -= penalty;
                }
                //compare rings
                for (int k = 0; k < handOne.getRing().getFingerParts().Length; k++)
                {
                    if (this.getRatio(handOne.getRing().getFingerParts()[k].min,
                                     handTwo.getRing().getFingerParts()[k].min) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getRing().getFingerParts()[k].max,
                             handTwo.getRing().getFingerParts()[k].max) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getRing().getFingerParts()[k].avg,
                             handTwo.getRing().getFingerParts()[k].avg) < simCriteria)
                        similarityScore -= penalty;
                }
                //compare pinkies
                for (int k = 0; k < handOne.getPinky().getFingerParts().Length; k++)
                {
                    if (this.getRatio(handOne.getPinky().getFingerParts()[k].min,
                                     handTwo.getPinky().getFingerParts()[k].min) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getPinky().getFingerParts()[k].max,
                             handTwo.getPinky().getFingerParts()[k].max) < simCriteria)
                        similarityScore -= penalty;

                    if (this.getRatio(handOne.getPinky().getFingerParts()[k].avg,
                             handTwo.getPinky().getFingerParts()[k].avg) < simCriteria)
                        similarityScore -= penalty;
                }
                if (similarityScore >= 0)
                    return similarityScore;
                else
                    return 0;
            }
            else return 100;
        }

        public double getRatio(double f1, double f2)
        {
            if (f1 >= f2)
            {
                return (f2 * 100) / f1;
            }
            else
            {
                return (f1 * 100) / f2;
            }
        }
    }*/
}
	