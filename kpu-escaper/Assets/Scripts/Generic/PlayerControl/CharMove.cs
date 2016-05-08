﻿using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour {

	bool isLeft = false ;
	bool isRight = false;
	bool isFront = false;
	bool isBack = false;
	bool isJump = false;
	float jumpTime = 0;

	// Use this for initialization
	void Start () {
	
	}

	/*void FixedUpdate ( ) {		
		if (isJump) {
			jumpTime += Time.deltaTime;
			gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 30);
			if(jumpTime > 0.2f){
				isJump = false;	
				jumpTime = 0;
			}
		}		
	}*/

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			GetComponent<Animator> ().SetBool ("isRunning", true); 
		}
		else if(Input.GetKeyUp (KeyCode.LeftShift))
		{
			GetComponent<Animator> ().SetBool ("isRunning", false);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			GetComponent<Animator> ().SetFloat ("speed", 1);
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isFront = true;
		}

		else if (Input.GetKeyUp (KeyCode.W)) {
			isFront = false;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isLeft = true;
		}
		
		else if (Input.GetKeyUp (KeyCode.A)) {
			isLeft = false;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			GetComponent<Animator> ().SetFloat ("speed", -1);
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isBack = true;
		}
		
		else if (Input.GetKeyUp (KeyCode.S)) {
			isBack = false;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			GetComponent<Animator> ().SetBool ("isMoving", true);
			isRight = true;
		}
		
		else if (Input.GetKeyUp (KeyCode.D)) {
			isRight = false;
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			GetComponent<Animator> ().SetBool ("isJumping", true);
		}

		if(!isRight && !isLeft && !isBack && !isFront)
			GetComponent<Animator> ().SetBool ("isMoving", false);

	
		if (Input.GetKey (KeyCode.W)) 
		{

			//transform.Translate(transform.forward * Time.deltaTime * 2, Space.World);
            transform.Translate(transform.forward * Time.deltaTime * 2, Space.World);
			
		}

		if (Input.GetKey (KeyCode.A)) 
		{
            transform.Translate(transform.right * Time.deltaTime * -2, Space.World);
			
		}
		if (Input.GetKey (KeyCode.S)) 
		{
            transform.Translate(transform.forward * Time.deltaTime * -2, Space.World);
			
		}
		if (Input.GetKey (KeyCode.D)) 
		{
            transform.Translate(transform.right * Time.deltaTime * 2, Space.World);			
		}

		/*if (Input.GetKey (KeyCode.Space)) 
		{
			if(!isJump){
				isJump = true;	
			}
		}*/

		
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 7.0f, 0); 
		} 

        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position,-transform.up),out hit))
        {
            if (hit.transform.name == "cubeCol")
            {
                transform.up = hit.normal;
                Physics.gravity = -9.81f * hit.normal;
            }
        }
        
	}
}
