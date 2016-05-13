using UnityEngine;
using System.Collections;

public class LaserCubeRotate : MonoBehaviour {
	
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position - gameObject.transform.position;
        //float RotateAmount = Time.deltaTime * 10;
		//transform.Rotate(RotateAmount, RotateAmount, RotateAmount);
		//transform.forward = pos.normalized;
		//Debug.Log (pos);
		transform.forward = Vector3.Lerp (transform.forward, pos.normalized, Time.deltaTime * 2);
	}
}
