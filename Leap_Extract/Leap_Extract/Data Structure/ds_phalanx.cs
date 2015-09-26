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
   
   public class ds_phalanx 
   {
        public person owner;
	    public decimal min,max,avg;
        public decimal sum, trimmedAverage, totalTrimmedAverage;
        public decimal variance, standardDeviation, standardDeviationSum;
	    public int measurements, countTrimmedAverages;

       // We impliment a trimmed average in which we keep the last 20 values. On this list we
       // will calculate a 20% trimmed or truncated mean. This means that 20% of the bottom 
       // values of the sorted array will be omitted during the calculation of the trimmed mean,
       // as well as 20% of the top of the set.
        public decimal[] twentyMeasurements = new decimal[20];
      
	    public ds_phalanx(person owner)
	    {
            this.owner = owner;
		    this.measurements = 0;
		    this.sum = 0;
            this.trimmedAverage = 0;
            this.totalTrimmedAverage = 0;
            this.countTrimmedAverages = 0;
		    this.avg = 0;
		    this.min = 200;
		    this.max = 0;
            this.variance = 0;
            this.standardDeviation = 0;
            this.uniqueID = Guid.NewGuid().ToString();
	    }

        public void UpdateMeasurement(decimal measurement)
	    {
		    if(measurement <= min)
			    min = measurement;

		    if(measurement >= max)
			        max = measurement;
		    
            // calculate the total sum
		    sum += measurement;

            // calculate the trimmed average
            calculateTrimmedAverage(measurement);

            // determine the number of measurements
		    measurements++;
            
            // calculate the normal average
		    calculateAvg();

            // calculate the variance
            calculateVariance(measurement);

            // calculate standard deviation
            calculateStandardDeviation(measurement);
	    }


        public void setMeasurements(decimal setTrimAvg, decimal varianced, decimal standardD, decimal setMin, decimal setMax)
        {
            this.trimmedAverage = setTrimAvg;
            this.variance = varianced;
            this.standardDeviation = standardD;
            this.min = setMin;
            this.max = setMax;
        }


       public void calculateStandardDeviation(decimal zMeasure)
        {
            decimal temp = zMeasure - avg;

            decimal standardDeviationPart = Convert.ToDecimal(Math.Pow((double)temp, 2));

            standardDeviationSum += standardDeviationPart;

            standardDeviation = standardDeviationSum/measurements;
            
        }

       public decimal getStandardDeviation()
       {
           return Convert.ToDecimal(Math.Sqrt((double)standardDeviation));
       }

       public void calculateVariance(decimal xMeasure)
       {
           decimal temp = xMeasure - avg;
           
           decimal variancePart = Convert.ToDecimal(Math.Pow((double)temp, 2));

           variance += variancePart;
       }

       public decimal getVariance()
       {
           return variance / measurements;
       }


        public void calculateTrimmedAverage(decimal theMeasurement)
        {
            trimmedAverage = 0;
            decimal[] tempArray = new decimal[19];

            // only start calculating the trimmed average when enough of previous data is captured. 20 scans in this case
            if (measurements > 19)
            {
                Array.Sort(twentyMeasurements);

                for (int i = 0; i < 12; i++)
                {
                    trimmedAverage += twentyMeasurements[i + 4];
                }
                // determine the trimmed average of this current set
                trimmedAverage = trimmedAverage / 12;

                // add to the total trimmed average
                totalTrimmedAverage += trimmedAverage;
                countTrimmedAverages++;

                for (int l = 0; l < 20; l++ )
                {
                    if (l < 19)
                    {
                        tempArray[l] = twentyMeasurements[l + 1];
                    }
                    else if (l == 19)
                    {
                        for (int y = 0; y < 19; y++)
                        {
                            twentyMeasurements[y] = tempArray[y];
                        }

                        twentyMeasurements[l] = theMeasurement;
                    }
                }

            }
            else if (measurements < 20)
            {
                twentyMeasurements[measurements] = theMeasurement;
            }

        }
	
	    public void calculateAvg() 
	    {
		    this.avg = sum/measurements;
	    }

        public decimal getTrimmedAverage()
        {
            return totalTrimmedAverage/countTrimmedAverages;
        }

        public decimal getMin()
        {
		    return min;
	    }

        public decimal getMax()
        {
		    return max;
	    }


        public decimal getAvg()
        {
		    return avg;
	    }

	    public String toString(String prefix)
	    {
            decimal getV = getVariance(); 
            return "TYPE " + prefix + " --> Trimmed Avg: " + String.Format("{0:#,#.#####}",trimmedAverage) + Environment.NewLine +
                                            "Variance: " + String.Format("{0:#,#.########}", getV) + Environment.NewLine +
                                            "Standard deviation: " + String.Format("{0:#,#.########}", getStandardDeviation()) + Environment.NewLine +
                                            "Min: " + String.Format("{0:#,#.#####}", min) + Environment.NewLine +
                                            "Max: " + String.Format("{0:#,#.#####}", max);		
	    }	

       public List<String> csvToString(String csvPrefix)
        {
           List<String> mesg = new List<String>();

           decimal getTA = this.getTrimmedAverage();
           decimal getV = getVariance(); 

           String boneType = "TYPE: " + csvPrefix;

           String trimAverage = String.Format("{0:#,#.#####}", getTA);
           String svariance = String.Format("{0:#,#.########}", getV);
           String sdeviation = String.Format("{0:#,#.########}", getStandardDeviation());

           String minimum = String.Format("{0:#,#.#####}", min);
           String maximum = String.Format("{0:#,#.#####}", max);

           mesg.Add(boneType);
           mesg.Add(trimAverage);
           mesg.Add(svariance);
           mesg.Add(sdeviation);
           mesg.Add(minimum);
           mesg.Add(maximum);

           return mesg;
        }
 
    }
}
