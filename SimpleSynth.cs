/* 
 * class SimpleSynth 
 * derived class of Synthesizer
 * contains one oscillator and an envelope
 * 
 * by F. Vriezenga, March 2018
 */ 

using UnityEngine;

//TODO - change envelope to controllrate instead of audiorate 
/* we could use a different clock, one for audio, one for controll.*/

public class SimpleSynth : Synth {
	private Sine sine;
	private Envelope envelope;

	//____________ CONSTRUCTOR ____________ 
	public SimpleSynth(Clock clock, double samplerate, Adsr adsr) : base(clock, samplerate) {
		sine = new Sine (clock, samplerate, 440, 0);
		envelope = new Envelope (clock, samplerate, adsr); 
		}

	//____________ PUBLIC METHODS ____________ 
	public void playNote(){
		envelope.run ();
	}

	protected override void updateFrequency() {
		//update sine frequency
		sine.Frequency = (double)frequency;
	
	}

	//____________ PROTECTED METHODS ____________ 
	//calculates the next sample
	protected override void calculateSample() {
		sample = sine.Sample * envelope.Sample;
	}



}
