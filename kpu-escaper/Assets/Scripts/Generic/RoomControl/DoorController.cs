using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	bool check = false;
	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			check = true;
		}    	
	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			check = false;
		}   
	}

	void Update () {
		if (check) {
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0.6f, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, -0.6f, 0), Time.deltaTime * 1.2f);
		} else {
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
		}
	}
}
