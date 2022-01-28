/* 
 * class Synthesizer 
 * base class for different types of synthesizers
 * 
 * by F. Vriezenga, March 2018
 */ 

using UnityEngine;

public class Synth : Generator {
	private float midiPitch = 0f;
	//use Unity's default samplerate
	protected float frequency = 0f;
	protected float amplitude = 0.5f;


	//____________ CONSTRUCTOR ____________ 
	public Synth(Clock clock, double samplerate) : base(clock, samplerate) {}


	//____________ PUBLIC METHODS ____________ 
	//is called by the clock (Generator base class functionality)
	public override void Tick()
	{			
		//calculate the next sample
		calculateSample();
	}


	//____________ PROTECTED METHODS ____________
	//calculates the next sample
	protected virtual void calculateSample() {}

	protected void setFrequency(float frequency)
	{
		//do not except frequencies below 0 and above nyquist
		if(frequency > 0 || frequency < 0.5 * samplerate) {
			this.frequency = frequency;
			//call updateFrequency method to allow derived classes to update frequency
			updateFrequency();
		}
		else {
			Debug.Log("Synth.setFrequency - values above nyquist aren't allowed.");
		}
	}

	/* virtual method that needs to be implemented by derived classes
	 * to allow individual handling a new frequency */
	protected virtual void updateFrequency() {}

	protected float mtof(float midiPitch) {
		return Mathf.Pow(2.0f,(midiPitch-69.0f)/12.0f) * 440.0f;
	}
		

	//____________ GETTERS AND SETTERS ____________
	public float MidiPitch {
		get { return midiPitch; } 
		set { 
			//if midiPitch changes less then 1 cents, do not update the midiPitch
			if (value < (midiPitch - 0.005) || value > (midiPitch + 0.005)) {				
				midiPitch = value;
				setFrequency (mtof (midiPitch));
			} //end if
			else {
				Debug.Log ("Synth.setMidiPitch - new midiPitch is same pitch (rounded at cents)");
				Debug.Log ("  current Pitch: " + midiPitch);
				Debug.Log ("  new pitch: " + value);
			}
		}
	}
}
