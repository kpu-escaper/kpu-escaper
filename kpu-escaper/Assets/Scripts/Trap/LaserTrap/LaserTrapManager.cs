using UnityEngine;
using System.Collections;

public class LaserTrapManager : RoomPropertyController {

	Laser Laser;

	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Laser_Sound;		// 레이저 사운드

	public override void EnterRoom()
	{
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Laser_Sound;
		this.EsAudio.loop = true;

		EsAudio.volume -= 0.7f;

		Laser = transform.FindChild ("LaserCube").transform.FindChild ("Laser").GetComponent<Laser> ();
		Laser.instance.Launch ();

		this.EsAudio.Play ();

		Debug.Log("레이저 트랩 방에 들어왔습니다.");
	}
	
	public override void Update()
	{
	}
	
	public override void LeaveRoom()
	{
		Laser.instance.UnLaunch ();
		Debug.Log("레이저 트랩 방에서 나갔습니다.");
	}
}