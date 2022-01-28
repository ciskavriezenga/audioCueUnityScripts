/* 
 * class Clock 
 * used by Generator objects, to update phase (calls tick)
 * 
 * by F. Vriezenga, March 2018
 */ 


public delegate void TichEventHandler();


public class Clock {
	public event TichEventHandler TickHandler;

	protected virtual void OnTick() {
		if (TickHandler != null) TickHandler();
	}

	public void Tick() {
		OnTick();
	}

}
