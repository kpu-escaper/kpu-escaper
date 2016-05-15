using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public static Laser instance;

	void Awake(){
		if (instance == null)
			instance = this;
	}
	

	private LineRenderer _Laser;
	float maxLength = 5.5f;
	float laserLength = 0.5f;
	bool isLaunch = false;
	
	RaycastHit Hit;

	// Use this for initialization
	void Start () {
		_Laser = GetComponent<LineRenderer> ();	
	}

	public void Launch(){
		isLaunch = true;
	}

	public void UnLaunch(){
		isLaunch = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(isLaunch){
			if(laserLength < maxLength){
				_Laser.SetPosition (1, new Vector3 (0, 0, laserLength));
				laserLength += Time.deltaTime * 2.0f;
			}
			else if (Physics.Raycast (transform.position, transform.forward, out Hit)) {
				if (Hit.collider) {
					_Laser.SetPosition (1, new Vector3 (0, 0, Hit.distance));
				}
				if(Hit.transform.tag == "Player"){
					HPManager.instance.GetDamage(30);
				}
			}
		}	
	}
}
