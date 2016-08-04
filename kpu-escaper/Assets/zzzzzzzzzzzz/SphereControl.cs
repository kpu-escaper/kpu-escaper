using UnityEngine;
using System.Collections;

public class SphereControl : MonoBehaviour {

	float size = 0.0f;
	bool max = false;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if(!max){
			size += 0.02f;
			if(size > 0.7f){
				max = true;
			}
		}else{
			size -= 0.02f;
			if(size < 0.0f){
				Destroy(gameObject);
			}
		}

		gameObject.transform.localScale = new Vector3 (size, size, size);
	
	}
}
