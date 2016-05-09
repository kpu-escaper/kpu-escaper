using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {
	string direction;
	bool isCollision = false;

	public static LiftController instance;
	
	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Update () {
		if(Input.GetKey (KeyCode.R)){
			direction = "lift";
		}

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
		case "lift":{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0, -3.786f, 0), Time.deltaTime*2);
		}break;
		default:{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime*2);
		}break;
		}

	}

	void OnLeftClick(GameObject obj)
	{
		if (obj.name == "Lfront") {
			direction = "front";
		} else if (obj.name == "Lback") {
			direction = "back";
		} else if (obj.name == "Lleft") {
			direction = "left";
		} else if (obj.name == "Lright") {
			direction = "right";
		} else if (obj.name == "Ltop") {
			direction = "top";
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			RayEvent.OnLeftClick += OnLeftClick;
			col.transform.SetParent(GameObject.Find("LiftParent").transform);
			isCollision = true;
		}    	
	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{			
			RayEvent.OnLeftClick -= OnLeftClick;
			col.transform.parent = null;
			isCollision = false;
			direction = "null";
		}   
	}

	public void SetInitPos(){
		transform.localPosition = new Vector3 (0, 7.666f, 0);
	}

}
