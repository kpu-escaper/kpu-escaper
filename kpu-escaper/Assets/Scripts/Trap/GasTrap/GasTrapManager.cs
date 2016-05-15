using UnityEngine;
using System.Collections;
//using UnityEngine.AudioSource.volume;

public class GasTrapManager : RoomPropertyController
{
	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Gas_Breathing;		// 가시 나올때 사운드
	static public bool trap_bool;
	
	public override void EnterRoom()
	{
		bool trap_bool = false;
		
		BlurController.instance.EnterGas ();
		
		StartCoroutine("GasTrapRoutine");
		//Debug.Log("독가스 트랩 방에 들어왔습니다.");
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Gas_Breathing;
		this.EsAudio.loop = true;
		
		//UnityEngine.AudioSource.volume = AudioSource.volume + 0.5;
		
		this.EsAudio.Play ();
		
	}
	
	public override void Update()
	{
		
	}
	
	public override void LeaveRoom()
	{
		trap_bool = true;
		
		BlurController.instance.ExitGas ();
		
		StopCoroutine("GasTrapRoutine");
		
		CameraShake.instance.shakePower = 0.25f;
		
		HPManager.instance.SetBloodColor("Red");
		//Debug.Log("독가스 트랩 방에서 나갔습니다.");
		
		
	}
	
	IEnumerator GasTrapRoutine()
	{
		CameraShake.instance.shakePower = 0.04f;
		
		int Damage = 0;
		while (true)
		{
			HPManager.instance.GetDamage(Damage);
			HPManager.instance.SetBloodColor("Green");
			Damage++;
			
			yield return new WaitForSeconds(1);
		}
	}
}
