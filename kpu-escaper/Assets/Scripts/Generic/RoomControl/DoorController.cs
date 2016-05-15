using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	
	public static DoorController instance;
	
	bool isCollision = false;
	bool isKeyDown = false;
	public bool block = false; //임시로 추가
	
	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Door_Open;		// 문열리는 사운드
	
	
	
	void Awake()
	{
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Door_Open;
		this.EsAudio.loop = false;
		
		EsAudio.volume -= 0.95f;
		
		if (instance == null)
			instance = this;
		
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
			isCollision = false;
			isKeyDown = false;
		}   
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			
			this.EsAudio.Play ();
			isKeyDown = true;
		}
		
		if (Input.GetKeyDown (KeyCode.M)) {
			
			//this.EsAudio.Play ();
			RoomController.instance.UnBlockTheDoor();
			LiftManager.instance.TurnOnManager();
		}
		
		
		if (isCollision && !block && isKeyDown) 
		{
			this.EsAudio.Play ();
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0.6f, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, -0.6f, 0), Time.deltaTime * 1.2f);
		}
		
		else 
		{
			transform.FindChild ("Box002").localPosition = Vector3.Lerp (transform.FindChild ("Box002").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
			transform.FindChild ("Box003").localPosition = Vector3.Lerp (transform.FindChild ("Box003").localPosition, new Vector3 (0, 0, 0), Time.deltaTime * 1.2f);
			
			
		}
		
		
	}
}
