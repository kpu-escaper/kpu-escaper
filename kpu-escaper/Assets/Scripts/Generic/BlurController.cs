using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurController : MonoBehaviour {

	public static BlurController instance;

	public Camera MainCam;	
	BlurOptimized Blur;	
	float mTime = 0.0f;
	bool inGasRoom = false;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void EnterGas(){
		Blur = MainCam.GetComponent<BlurOptimized> ();
		Blur.enabled = true;
		inGasRoom = true;
	}

	public void ExitGas(){
		inGasRoom = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (Blur.enabled) {
			if (inGasRoom) {
				mTime += Time.deltaTime;
				if (mTime > 4.0f)
					Blur.blurIterations = 2;

				if(Blur.blurSize < 3.0f)
					Blur.blurSize += Time.deltaTime * 0.3f;

			} else {
				Blur.blurSize -= Time.deltaTime * 0.5f;
				if (Blur.blurSize < 0.0f)
					Blur.enabled = false;
			}
		}
	}
}
