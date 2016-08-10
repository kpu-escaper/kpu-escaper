using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour {
	
	bool isLeft = false ;
	bool isRight = false;
	bool isFront = false;
	bool isBack = false;

	//
	public GameObject mCamera;
	
	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Walk_Sound;		// 걷는소리
	public AudioClip Jump_Sound;		// 점프소리
	// Use this for initialization
	void Start () {
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
	}
	
	
	// Update is called once per frame
	void Update () {



		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			GetComponent<Animator> ().SetBool ("isRunning", true); 
		}
		else if(Input.GetKeyUp (KeyCode.LeftShift))
		{
			GetComponent<Animator> ().SetBool ("isRunning", false);
		}

		if(Input.GetKeyDown (KeyCode.JoystickButton5)){
		//if (Input.GetKeyDown (KeyCode.W)) {
			
			this.EsAudio.clip = this.Walk_Sound;
			this.EsAudio.loop = true;
			
			this.EsAudio.Play();
			
			GetComponent<Animator> ().SetFloat ("speed", 1);
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isFront = true;
		}

		else if(Input.GetKeyUp (KeyCode.JoystickButton5)){
		//else if (Input.GetKeyUp (KeyCode.W)) {
			isFront = false;
			this.EsAudio.loop = false;
		}

		if(Input.GetKeyDown (KeyCode.JoystickButton7)){
		//if (Input.GetKeyDown (KeyCode.A)) {
			
			this.EsAudio.loop = true;
			this.EsAudio.Play();
			
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isLeft = true;
		}

		else if(Input.GetKeyUp (KeyCode.JoystickButton7)){
		//else if (Input.GetKeyUp (KeyCode.A)) {
			isLeft = false;
			this.EsAudio.loop = false;
			
		}

		if(Input.GetKeyDown (KeyCode.JoystickButton6)){
		//if (Input.GetKeyDown (KeyCode.S)) {
			
			this.EsAudio.loop = true;
			this.EsAudio.Play();
			
			GetComponent<Animator> ().SetFloat ("speed", -1);
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isBack = true;
		}

		else if(Input.GetKeyUp (KeyCode.JoystickButton6)){
		//else if (Input.GetKeyUp (KeyCode.S)) {
			isBack = false;
			this.EsAudio.loop = false;
			
		}

		if(Input.GetKeyDown (KeyCode.JoystickButton8)){
		//if (Input.GetKeyDown (KeyCode.D)) {
			
			this.EsAudio.loop = true;
			this.EsAudio.Play();
			
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isRight = true;
		}

		else if(Input.GetKeyUp (KeyCode.JoystickButton8)){
		//else if (Input.GetKeyUp (KeyCode.D)) {
			isRight = false;
			this.EsAudio.loop = false;
			
		}
		
		if (Input.GetKeyDown (KeyCode.Q)) {
			
			GetComponent<Animator> ().SetBool ("isJumping", true);
		}
		
		if(!isRight && !isLeft && !isBack && !isFront)
			GetComponent<Animator> ().SetBool ("isMoving", false);
		

		if (Input.GetAxis("Vertical")>0 || Input.GetKey (KeyCode.W))
		//if (Input.GetKey (KeyCode.W)) 
		{
			//transform.Translate(transform.forward * Time.deltaTime * 2, Space.World);
			transform.Translate((Vector3.Cross(mCamera.transform.right, transform.up).normalized * Time.deltaTime * 2)
			                    , Space.World);
			
		}
		if (Input.GetAxis("Horizontal")<0 || Input.GetKey (KeyCode.A))
		//if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate(mCamera.transform.right * Time.deltaTime * -2, Space.World);
			
		}
		if (Input.GetAxis("Vertical")<0 || Input.GetKey (KeyCode.S))
		//if (Input.GetKey (KeyCode.S)) 
		{
			//transform.Translate(transform.forward * Time.deltaTime * -2, Space.World);
			transform.Translate((Vector3.Cross(mCamera.transform.right, transform.up).normalized * Time.deltaTime * -2)
			                    , Space.World);

		}
		if (Input.GetAxis("Horizontal")>0 || Input.GetKey (KeyCode.D))
		//if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate(mCamera.transform.right * Time.deltaTime * 2, Space.World);			
		}
		
		
		if (Input.GetKeyDown(KeyCode.LeftAlt)) 
		{
			this.EsAudio.clip = this.Jump_Sound;
			this.EsAudio.loop = false;
			
			this.EsAudio.Play();
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 7.0f, 0); 
		} 
		
		RaycastHit hit;
		/* if(Physics.Raycast(new Ray(transform.position,-transform.up),out hit))
        {
            if (hit.transform.name == "cubeCol")
            {
                transform.up = hit.normal;
                Physics.gravity = -9.81f * hit.normal;
            }
        }*/
		
	}
}
