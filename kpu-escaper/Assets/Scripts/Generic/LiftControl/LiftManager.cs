using UnityEngine;
using System.Collections;

public class LiftManager : MonoBehaviour {

	public static LiftManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}


	public void SetCurrentPosition(Transform CurrentRoomTransform){
		transform.position = CurrentRoomTransform.transform.position;
	}

	public void TurnOnManager(){
		gameObject.SetActive (true);
	}

	public void TurnOffManager(){
		if(gameObject.transform.FindChild ("MainCharacter"))
			gameObject.transform.FindChild ("MainCharacter").parent = null;
		gameObject.SetActive (false);
	}
}
