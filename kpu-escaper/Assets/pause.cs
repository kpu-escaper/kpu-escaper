using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {

	bool isPause = false;
	public static pause instance;    

	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pause(){
		isPause = !isPause;
		if(isPause == true) {
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}
	}

}
