/* 
 * class Oscillator 
 * contains oscillator functionality 
 * 
 * by F. Vriezenga, March 2018
 */ 

public class Oscillator : Generator {
	protected const double TWO_PI = 6.28318530717959;
	private double frequency; 
	protected double phase = 0;


	//____________ CONSTRUCTOR ____________ 
	public Oscillator(Clock clock, double samplerate, double frequency, double phase) : 
		base(clock, samplerate) {
		this.frequency = frequency;
		this.phase = phase;
	}


	//____________ PUBLIC METHODS ____________ 
	public override void Tick()
	{
		//incremet phase
		phase += frequency / samplerate;
		//wrap phase from 0 to 1
		if(phase >= 1) phase = phase - 1;
		//calculate new sample
		calculateSample();
	}


	//____________ PROTECTED METHODS ____________ 
	//calculates the next sample
	protected virtual void calculateSample() {}


	//____________ GETTERS AND SETTERS ____________
	public double Frequency {
		get { return frequency; } 
		set { frequency = value; }
	}
	public double Phase {
		get { return phase; } 
		set { phase = value; }
	}


}
