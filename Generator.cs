/* 
 * class Generator 
 * base class, contains the basic audio generator funcionality
 * 
 * by F. Vriezenga, March 2018
 */ 

public class Generator{
	//allows automatically update of phase
	private Clock clock;

	//default samplerate Unity = 48000
	protected double samplerate = 48000.0;
	protected double sample = 0;


	//____________ CONSTRUCTOR ____________ 
	public Generator(Clock clock, double samplerate) {
		this.samplerate = samplerate;
		//connect to clock 
		this.clock = clock;
		clock.TickHandler += new TichEventHandler (this.Tick);
	}


	//____________ PUBLIC METHODS ____________ 
	//simulates a clock tick -> phase += increment
	public virtual void Tick() {}


	//____________ GETTERS AND SETTERS ____________
	public double Samplerate { 
		get { return samplerate; } 
		set { samplerate = value; }
	}
	//don't need a public setter for the sample
	public double Sample {
		get { return sample; } 
	}
	

}
