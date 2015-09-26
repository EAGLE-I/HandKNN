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

    public class ds_finger
    {

    public person owner;
	String fingerName;
	ds_phalanx[] fingerParts;
	int numOfParts;
    String[] prefix = { "DISTAL", "INTERMEDIATE", "PROXIMAL", "METACARPAL" };

	public ds_finger()
	{
		this.setFingerName("UnknownFinger");
		
	}
	
	public ds_finger(String fingerName, int numParts, person owner)
	{
        this.owner = owner;
			this.setFingerName(fingerName);
			this.numOfParts = numParts;
			this.fingerParts = new ds_phalanx[this.numOfParts];
         
			for(int k = 0; k < fingerParts.Length; k++)
				fingerParts[k] = new ds_phalanx(owner);
	}

    public void updateFinger(decimal[] measurements)
	{

		try
        {
		    if(measurements.Length == this.numOfParts)
			    for(int k = 0; k < fingerParts.Length; k++)
			    {
					    fingerParts[k].UpdateMeasurement(measurements[k]);
			    }
		    else
		    {
			    Console.WriteLine("ERROR!");
			    for(int k = 0; k< measurements.Length; k++)
				    Console.WriteLine(measurements[k]);

			    throw new Exception("You are sending ("+ measurements.Length +") which is not the amount of measurements finger " + this.getFingerName() + " expects(" + this.numOfParts +").");
		    }
		}
		catch(NullReferenceException)
		{
			Console.WriteLine("Error Detected with Finger:" + this.getFingerName());
		}
	}

    public void setFingerThumb(decimal[] distalThumbMeasurements, decimal[] intermediateThumbMeasurements, decimal[] proximalThumbMeasurements)
    {
        fingerParts[0].setMeasurements(distalThumbMeasurements[0], distalThumbMeasurements[1], distalThumbMeasurements[2], distalThumbMeasurements[3], distalThumbMeasurements[4]);
        fingerParts[1].setMeasurements(intermediateThumbMeasurements[0], intermediateThumbMeasurements[1], intermediateThumbMeasurements[2], intermediateThumbMeasurements[3], intermediateThumbMeasurements[4]);
        fingerParts[2].setMeasurements(proximalThumbMeasurements[0], proximalThumbMeasurements[1], proximalThumbMeasurements[2], proximalThumbMeasurements[3], proximalThumbMeasurements[4]);
    }

    public void setFingerRest(decimal[] distalMeasurements, decimal[] intermediateMeasurements, decimal[] proximalMeasurements, decimal[] metacarpalMeasurements)
    {
        fingerParts[0].setMeasurements(distalMeasurements[0], distalMeasurements[1], distalMeasurements[2], distalMeasurements[3], distalMeasurements[4]);
        fingerParts[1].setMeasurements(intermediateMeasurements[0], intermediateMeasurements[1], intermediateMeasurements[2], intermediateMeasurements[3], intermediateMeasurements[4]);
        fingerParts[2].setMeasurements(proximalMeasurements[0], proximalMeasurements[1], proximalMeasurements[2], proximalMeasurements[3], proximalMeasurements[4]);
        fingerParts[3].setMeasurements(metacarpalMeasurements[0], metacarpalMeasurements[1], metacarpalMeasurements[2], metacarpalMeasurements[3], metacarpalMeasurements[4]);
    }

	
	public String getFingerName() 
    {
		return fingerName;
	}

	public void setFingerName(String fingerName) 
    {
		this.fingerName = fingerName;
	}
	
	public String toString()
	{
		String msg = "";
        msg = msg + Environment.NewLine + this.getFingerName() + Environment.NewLine;
		for(int k = 0; k < this.fingerParts.Length; k++)
		{
            msg += fingerParts[k].toString(prefix[k]) + Environment.NewLine;
		}
		return msg;	
	}


    public List<String> csvToString()
    {
        List<String> mesg = new List<String>();

        mesg.Add(this.getFingerName());

        for (int u = 0; u < this.fingerParts.Length; u++)
        {
            mesg.AddRange(fingerParts[u].csvToString(prefix[u]));
        }

        return mesg;
    }

	public ds_phalanx[] getFingerParts() 
    {
		return fingerParts; 
	}

        
	
}
}
