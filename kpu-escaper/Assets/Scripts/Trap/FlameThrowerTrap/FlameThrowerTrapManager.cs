using UnityEngine;
using System.Collections;

public class FlameThrowerTrapManager : RoomPropertyController {

	public GameObject FlamethrowerEffect;
	GameObject[] Trap;
	Vector3 holePos;
	Quaternion angle;

	private AudioSource EsAudio;
	public AudioClip FlameThrower_Sound;

	public override void EnterRoom(){

		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.FlameThrower_Sound;
		this.EsAudio.loop = false;


		Trap = GameObject.FindGameObjectsWithTag("FlameHole");
		StartCoroutine("FlameThrowerTrapRoutine");
	}
	public override void Update(){

	}
	
	public override void LeaveRoom(){
		StopCoroutine("FlameThrowerTrapRoutine");
	}

	IEnumerator FlameThrowerTrapRoutine(){

		while(true)
		{

			for (int i = 0; i < 3; ++i)
			{
				int randomInt = Random.Range(0, Trap.Length);
				//thornPos = new Vector3(Random.Range (-1106, 890),0,Random.Range (-980,1207));
				holePos = new Vector3(Random.Range (-1250, 1510),0,Random.Range (-1470,1370));
				holePos = holePos/1000;
				Trap[randomInt].transform.localPosition = holePos;
				Trap[randomInt].GetComponent<Animation>()["FlameHoleAni"].speed = 1;
				Trap[randomInt].GetComponent<Animation>().Play();

				switch(Trap[randomInt].layer){
				case 8: //bottom
					angle = Quaternion.Euler(270.0f, 0, 0);
					break;
				case 9: //back
					angle = Quaternion.Euler(0, 0, 0);
					break;
				case 10: //top
					angle = Quaternion.Euler(90.0f, 0, 0);
					break;
				case 11: //front
					angle = Quaternion.Euler(180.0f, 0, 0);
					break;
				case 12: //left
					angle = Quaternion.Euler(0, 90.0f, 0);
					break;
				case 13: //right
					angle = Quaternion.Euler(0, 270.0f, 0);
					break;
				}

				Instantiate (FlamethrowerEffect, new Vector3 (Trap[randomInt].transform.position.x,
				                                              Trap[randomInt].transform.position.y,
				                                              Trap[randomInt].transform.position.z), angle);

			}
			yield return new WaitForSeconds(3.1f);
			this.EsAudio.Play ();
			yield return new WaitForSeconds(6.1f);

			
		}


	}
}