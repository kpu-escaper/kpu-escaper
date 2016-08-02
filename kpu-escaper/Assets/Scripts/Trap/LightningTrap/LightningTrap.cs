using UnityEngine;
using System.Collections;

public class LightningTrap : RoomPropertyController
{
	public GameObject LightningEffect;	// 번개 이펙트
	public GameObject Foot_Circle;		// 번개칠 자리에 표시해주는 이펙트
	public GameObject Char;				// 캐릭터
	private Vector3 FootCirclePos1;		// 번개칠 위치
	private Quaternion FootCirclePos2;	// 번개칠 위치의 각도

	private AudioSource EsAudio;		// 오디오 플레이어
	private AudioSource EsAudio2;		// 오디오 플레이어

	public AudioClip Lightning_Sound;	// 번개 소리
	public AudioClip Needle_Hit;		// 번개 쳐맞을때 소리

	
	
	public override void EnterRoom()
	{


	}
	
	public override void Update()
	{
	}
	
	public override void LeaveRoom()
	{
		Debug.Log ("나갔습니다");
		
	}
	
	void Awake()
	{

		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Needle_Hit;
		this.EsAudio.loop = false;
		
		this.EsAudio2 = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio2.clip = this.Lightning_Sound;
		this.EsAudio.loop = false;

		StartCoroutine("FootCircle");
	}
	
	
	IEnumerator FootCircle()
	{	
		yield return new WaitForSeconds(1.5f);
		
		// 번개 떨어질 자리에 동그라미 표시
		Instantiate ( Foot_Circle, Char.transform.position, Char.transform.rotation  );
		
		FootCirclePos1 = Char.transform.position ;
		FootCirclePos2 = Char.transform.rotation;
		
		StartCoroutine ("Lightning");
	}
	
	
	IEnumerator Lightning()
	{
		yield return new WaitForSeconds(0.5f);
		
		// 번개 떨어짐
		//Instantiate (LightningEffect, Char.transform.position, Char.transform.rotation);
		
		Instantiate (LightningEffect, FootCirclePos1, FootCirclePos2 );
		this.EsAudio2.Play ();

		StartCoroutine ("FootCircle");
	}
	
	
	
	
}
