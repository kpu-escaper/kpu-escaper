using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public static DoorController instance;

	bool isCollision = false;
	bool isKeyDown = false;
	public bool block = false; //임시로 추가


	
	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			isCollision = true;
		}    	
	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			isCollision = false;
			isKeyDown = false;
		}   
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			isKeyDown = true;
		}
		if (isCollision && !block && isKeyDown) {
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0.6f, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, -0.6f, 0), Time.deltaTime * 1.2f);
		} else {
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			RoomController.instance.UnBlockTheDoor();
			LiftManager.instance.TurnOnManager();
		}
	}
}
