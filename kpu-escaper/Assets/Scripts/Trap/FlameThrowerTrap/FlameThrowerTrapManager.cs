using UnityEngine;
using System.Collections;

public class FlameThrowerTrapManager : RoomPropertyController {

	public override void EnterRoom(){
		StartCoroutine("FlameThrowerTrapRoutine");
	}
	public override void Update(){
		
	}
	
	public override void LeaveRoom(){

	}

	IEnumerator FlameThrowerTrapRoutine(){
		yield return new WaitForSeconds (1);
	}
}