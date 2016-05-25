using UnityEngine;
using System.Collections;

public class MedievalTrapManager : RoomPropertyController
{
	GameObject[] Trap;
	Vector3 thornPos;
	
	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Needle;		// 가시 나올때 사운드
	
	public override void EnterRoom()
	{
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Needle;
		this.EsAudio.loop = false;
		
		Trap = GameObject.FindGameObjectsWithTag("MedievalTrap");
		StartCoroutine("MedievalTrapRoutine");
		//Debug.Log("가시 트랩 방에 들어왔습니다.");
	}
	
	public override void Update()
	{
		
	}
	
	public override void LeaveRoom()
	{
		StopCoroutine("MedievalTrapRoutine");
		//Debug.Log("가시 트랩 방에서 나갔습니다.");
	}
	
	IEnumerator MedievalTrapRoutine()
	{
		while(true)
		{
			for (int i = 0; i < 12; ++i)
			{
				int randomInt = Random.Range(0, Trap.Length);
				//thornPos = new Vector3(Random.Range (-1106, 890),0,Random.Range (-980,1207));
				thornPos = new Vector3(Random.Range (-1250, 1510),0,Random.Range (-1470,1370));
				thornPos = thornPos/1000;
				Trap[randomInt].transform.localPosition = thornPos;
				Trap[randomInt].GetComponent<Animation>()["Up Down"].speed = 1;
				Trap[randomInt].GetComponent<Animation>().Play();
				
				this.EsAudio.Play();
				
			}
			yield return new WaitForSeconds(2.0f);
			
		}
	}
}
