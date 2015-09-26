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
    
    public class ds_hand
    {
        public person owner;
	    ds_finger[] fingers;
	    bool iLeft;
	
	    public ds_hand (bool left, person owner)
	    {
            this.owner = owner;
		    String prefix;
		
		    if(left)
		    {
			    this.setLeft(true);
			    prefix = "Left";
			    fingers = new ds_finger[5];
			    fingers[0] = new ds_finger(prefix + "Thumb",3,owner);
                fingers[1] = new ds_finger(prefix + "Index", 4, owner);
                fingers[2] = new ds_finger(prefix + "Middle", 4, owner);
                fingers[3] = new ds_finger(prefix + "Ring", 4, owner);
                fingers[4] = new ds_finger(prefix + "Pinky", 4, owner);

		    }
		    else
		    {
                this.setLeft(false);
			    prefix = "Right";
			    fingers = new ds_finger[5];
                fingers[0] = new ds_finger(prefix + "Thumb", 3, owner);
                fingers[1] = new ds_finger(prefix + "Index", 4, owner);
                fingers[2] = new ds_finger(prefix + "Middle", 4, owner);
                fingers[3] = new ds_finger(prefix + "Ring", 4, owner);
                fingers[4] = new ds_finger(prefix + "Pinky", 4, owner);
		    }
	    }
		
	    public bool isLeft() {
		    return iLeft;
	    }
	    public void setLeft(bool isLeft) {
		    this.iLeft = isLeft;
	    }

        public void UpdateHandMeasurements(decimal[] thumbMeasurements, decimal[] indexMeasurements,
                                            decimal[] middleMeasurements, decimal[] ringMeasurements,
                                            decimal[] pinkyMeasurements)
	    {
		    fingers[0].updateFinger(thumbMeasurements);
		    fingers[1].updateFinger(indexMeasurements);
		    fingers[2].updateFinger(middleMeasurements);
		    fingers[3].updateFinger(ringMeasurements);
		    fingers[4].updateFinger(pinkyMeasurements);
	    }

        public void SetHandMeasurements(decimal[] distalThumbMeasurements, decimal[] intermediateThumbMeasurements, decimal[] proximalThumbMeasurements, decimal[] distalPinkyMeasurements, decimal[] intermediatePinkyMeasurements,
                                        decimal[] proximalPinkyMeasurements, decimal[] metacarpalPinkyMeasurements, decimal[] distalIndexMeasurements, decimal[] intermediateIndexMeasurements, decimal[] proximalIndexMeasurements,
                                        decimal[] metacarpalIndexMeasurements, decimal[] distalMiddleMeasurements, decimal[] intermediateMiddleMeasurements, decimal[] proximalMiddleMeasurements, decimal[] metacarpalMiddleMeasurements,
                                        decimal[] distalRingMeasurements, decimal[] intermediateRingMeasurements, decimal[] proximalRingMeasurements, decimal[] metacarpalRingMeasurements)
        {

            fingers[0].setFingerThumb(distalThumbMeasurements, intermediateThumbMeasurements, proximalThumbMeasurements);

            fingers[1].setFingerRest(distalIndexMeasurements, intermediateIndexMeasurements, proximalIndexMeasurements, metacarpalIndexMeasurements);
            fingers[2].setFingerRest(distalMiddleMeasurements, intermediateMiddleMeasurements, proximalMiddleMeasurements, metacarpalMiddleMeasurements);
            fingers[3].setFingerRest(distalRingMeasurements, intermediateRingMeasurements, proximalRingMeasurements, metacarpalRingMeasurements);
            fingers[4].setFingerRest(distalPinkyMeasurements, intermediatePinkyMeasurements, proximalPinkyMeasurements, metacarpalPinkyMeasurements);
        }
	
	    public ds_finger getThumb()
	    {
		    return fingers[0];
	    }

	    public ds_finger getIndex()
	    {
		    return fingers[1];
	    }

	    public ds_finger getMiddle()
	    {
		    return fingers[2];
	    }

	    public ds_finger getRing()
	    {
		    return fingers[3];
	    }

	    public ds_finger getPinky()
	    {
		    return fingers[4];
	    }

	    public String myToString()
	    {
		    String msg;
		    if(this.iLeft)
			    msg = "LEFT HAND: " + Environment.NewLine;
		    else
			    msg = "RIGHT HAND:" + Environment.NewLine;
		
		    for(int k = 0; k < fingers.Length; k++)
		    {
			    msg += fingers[k].toString();
		    }
		    return msg;
	    }

        public List<String> csvToString()
        {
            List<String> mesg = new List<String>();

            if (this.iLeft)
            {
                mesg.Add("LEFT HAND:");
            }
            else
                mesg.Add("RIGHT HAND:");

            for (int r = 0; r < fingers.Length; r++ )
            {
                mesg.AddRange(fingers[r].csvToString());
            }

            return mesg;
        }

	    public ds_finger[] getFingers() 
        {
		    return fingers;
	    }
    }

}