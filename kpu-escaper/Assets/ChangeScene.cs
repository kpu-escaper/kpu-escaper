using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void PlayScene() {
		Application.LoadLevel ("play");
	}

	public void StartScene() {
		Application.LoadLevel ("Start");
	}
	
	public void ExitScene() {
		Application.Quit ();
	}
}
