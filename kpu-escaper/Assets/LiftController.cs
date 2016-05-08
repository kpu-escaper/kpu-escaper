using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {
	string direction;
	bool isCollision = false;
	GameObject mc;

	// Use this for initialization
	void Start () {
		mc = GameObject.Find("MainCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		switch(direction){
		case "front":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0, -0.586f, 3.193f), Time.deltaTime*2);
		}break;
		case "back":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0, -0.586f, -3.193f), Time.deltaTime*2);
		}break;
		case "left":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (-3.193f, -0.586f, 0), Time.deltaTime*2);
		}break;
		case "right":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (3.193f, -0.586f, 0), Time.deltaTime*2);
		}break;
		case "top":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0, 2.83f, 0), Time.deltaTime*2);
		}break;
		default:{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3(0, -3.786f, 0), Time.deltaTime*2);
		}break;
		}

		if(isCollision){
			if(Input.GetKey (KeyCode.R)){
				mc.transform.SetParent(GameObject.Find("LiftParent").transform);
				RayEvent.OnLeftClick += OnLeftClick;
			}
		}
		else{
			RayEvent.OnLeftClick -= OnLeftClick;
		}

	}

	void OnLeftClick(GameObject obj)
	{
		if (obj.name == "Lfront")
		{
			direction = "front";
		}
		else if (obj.name == "Lback")
		{
			direction = "back";
		}
		else if (obj.name == "Lleft")
		{
			direction = "left";
		}
		else if (obj.name == "Lright")
		{
			direction = "right";

		}
		else if (obj.name == "Ltop")
		{
			direction = "top";

		}
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
			mc.transform.parent = null;
			isCollision = false;
			direction = "null";
		}   
	}

}
