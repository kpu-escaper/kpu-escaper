using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public static CameraShake instance;

	bool isShake = false;
	float mTime;
	Vector2 shakeRange;
	public float shakePower = 0.25f;
	public float shakeTime = 0.5f;
	Vector3 originPos;

	// Use this for initialization
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Input.GetKeyDown (KeyCode.N)) {
			originPos = gameObject.transform.localPosition;
			isShake = true;
		}*/

		if (isShake) {
			mTime += Time.deltaTime;
			shakeRange = Random.insideUnitCircle;
			gameObject.transform.localPosition = new Vector3 (shakeRange.x * shakePower, shakeRange.y * shakePower, originPos.z);
			if(mTime > shakeTime){
				mTime = 0;
				isShake = false;
				gameObject.transform.localPosition = originPos;
			}

		}
	}

	public void ShakeOn(){
		originPos = gameObject.transform.localPosition;
		isShake = true;
	}
}