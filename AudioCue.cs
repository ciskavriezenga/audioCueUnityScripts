/* 
 * class AudioCue 
 * controls a synth to allow live synthesis
 * 
 * by F. Vriezenga, March 2018
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour {	
	private Clock clock;
	private SimpleSynth synth;
    private float testEx = 0.0f;
    private float fullDistance;
    private Vector3 fPos;
    public GameObject goal;
    public GameObject fGoal;
    public GameObject pGoal;

	//variables needed to trigger synth
	private double samplerate = 48000;
	//the frequency to play a note
	private double triggerFrequency = 0;
	//phase used to allow triggering synth at regular interval
	private double phase = 0;
	private double phaseIncrement = 0;
	//these values determine the triggering frequency and pitch
	private float minTriggerFreq = 0.5f;
	private float maxTrigerFreq = 35f;
	private float minPitch = 56f;
	private float maxPitch = 68f;

	//____________ CONSTRUCTOR ____________ 
	public AudioCue() {
		//generate adsr
		Adsr adsr = new Adsr();
		adsr.ampValues = new double[3]{ 0, 1.0, 0 };
		adsr.durValues = new double[2]{ 0.01, 0.01 };
		adsr.numValues = 3;
		//generate audioClock
		clock = new Clock ();

		//generate synth
		synth = new SimpleSynth (clock, samplerate, adsr);
		//set triger Frequency
		setNormalizedDistance(1);

	}


	//____________ PUBLIC METHODS ____________ 
	//build into unity -> audiosource attached to object, this will work
	void OnAudioFilterRead(float [] data, int channels)
	{
		//go through audio buffer (=data)
		for(int i = 0; i < data.Length; i += channels) {
			//increase phase, to allow triggering the synth
			phase += phaseIncrement;
			//do we need to retrigger the synth? 
			if (phase > 1) {
				//trigger synth
				synth.playNote ();
				//wrap phase, max = 1
				phase -= 1;
			}
			
			data [i] = (float) synth.Sample;
			if (channels == 2) {
				data [i + 1] = data [i];
			}
			clock.Tick();
		}
	}


	//TODO create exponential translate method
	protected float translateLin(float inMin, float inMax, float outMin, float outMax, float value){
		float inDelta = inMax - inMin;

		float outDelta = outMax - outMin;
		return outMax + (value - inMax) / inDelta * outDelta;
	}
		
	//____________ GETTERS AND SETTERS ____________

	public double TriggerFrequency {
		set {
			triggerFrequency = value; 
			phaseIncrement = 1 / (samplerate / triggerFrequency);
		}
	}

	//allows you to set the normalized distance, used by the audio cue
	public void setNormalizedDistance(float normDistance) {
        //normDistance = testEx;
		if (normDistance <= 0)
			normDistance = 0.001f;
		//transform exponentional -> better result
		normDistance = Mathf.Pow(normDistance, 0.5f);
		//translate normalized distance to trigger frequency and pitch
		TriggerFrequency = (double) translateLin (1f, 0f, minTriggerFreq, maxTrigerFreq, normDistance);
		synth.MidiPitch = translateLin (1f, 0f, minPitch, maxPitch, normDistance);		 		
	}

    public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
    {

        //get vector from point on line to point in space
        Vector3 linePointToPoint = point - linePoint;

        float t = Vector3.Dot(linePointToPoint, lineVec);

        return linePoint + lineVec * t;
    }

    private void Start()
    {
        //fPos = transform.position;

        fullDistance = Vector3.Distance(fGoal.transform.position, pGoal.transform.position);
        
        
        
    }

    void Update()
    {
        float distance;

        fullDistance = Vector3.Distance(fGoal.transform.position, pGoal.transform.position);

        distance = Vector3.Distance(goal.transform.position, transform.position);

        setNormalizedDistance(distance / fullDistance);


    }

    


}
