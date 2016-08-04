using UnityEngine;
using System.Collections;

public class AimingManager : MonoBehaviour {

	public Material BoundaryMaterial;
	public GameObject Sphere;
	float fAlpha = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			//Instantiate (Sphere, Sphere.transform.position, Quaternion.identity);
			Instantiate (Sphere, new Vector3(Random.Range (0, 3),Random.Range (0, 3),Random.Range (0, 3)), Quaternion.identity);
		}


		if(fAlpha < 1)
			fAlpha += 0.005f;


		BoundaryMaterial.SetColor("_Color", new Color (0, 0, 0, fAlpha));
	
	}
}
