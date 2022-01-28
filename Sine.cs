/* 
 * class Sine
 * Inherits from Oscillator 
 * Generates sinewave
 * 
 * by F. Vriezenga, March 2018
 */ 

public class Sine : Oscillator {

	//____________ CONSTRUCTOR ____________ 
	public Sine(Clock clock, double samplerate, double frequency, double phase) : 
	base(clock, samplerate, frequency, phase) {}


	//____________ PROTECTED METHODS ____________ 
	//calculates the next sample
	protected override void calculateSample() {			
		//NOTE: fast sine calculation, not a perfect sinewave, but efficient
		//translate phase to -PI..PI		 
		double tempPhase = (phase - 0.5) * TWO_PI;
		//choose polynomial according to phase
		if (tempPhase < 0)
			sample =  1.27323954 * tempPhase + .405284735 * tempPhase * tempPhase;
		else
			sample = 1.27323954 * tempPhase - 0.405284735 * tempPhase * tempPhase;		
	}

}
