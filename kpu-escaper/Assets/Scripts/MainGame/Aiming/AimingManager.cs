using UnityEngine;
using System.Collections;

public class AimingManager : MainRoomPropertyController {

	public Material BoundaryMaterial;
	public GameObject Sphere;

	public GameObject Right_Effect;

	bool isBright = false;
	float fAlpha = 0.0f;
	float mTime = 0.0f;

	void Awake(){

	}

	public override void EnterRoom(){
		isBright = true;
		RayEvent.OnLeftClick += OnLeftClick;
		StartCoroutine ("AimingGame");
	}
	public override void Update(){

		base.Update ();


	}
	public override void LeaveRoom(){
		StopCoroutine ("BeBright");
	}

	public override void Clear(){
		StopCoroutine ("AimingGame");
		StartCoroutine ("BeBright");

	}

	void OnLeftClick(GameObject obj)
	{
		if(obj.CompareTag("Sphere"))
		{
			GameObject NewEffect = Instantiate (Right_Effect, obj.transform.position, Quaternion.identity) as GameObject;
			NewEffect.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			Destroy(obj);
		}
	}

	IEnumerator AimingGame(){
		while (isBright) {
			if (fAlpha < 1) {				
				fAlpha += 0.01f;
				BoundaryMaterial.SetColor ("_Color", new Color (0, 0, 0, fAlpha));
			}else{
				isBright = false;
			}
			yield return null;
		}
		
		while (!isBright) {
			mTime += 0.04f;
			if (mTime > 1) {
				GameObject NewSphere = Instantiate (Sphere, new Vector3 (0,-1000,0),
				                                    Quaternion.identity) as GameObject;
				NewSphere.transform.parent = gameObject.transform;
				NewSphere.transform.localPosition = new Vector3 (Random.Range (-3,3),
				                                                 Random.Range (-3,3),
				                                                 Random.Range (-3,3));
				
				mTime = 0;
			}
			yield return null;
		}
	}

	IEnumerator BeBright(){
		while (!isBright) {
			if (fAlpha > 0) {
				fAlpha -= 0.01f;
				BoundaryMaterial.SetColor ("_Color", new Color (0, 0, 0, fAlpha));
			}else{
				isBright = true;
			}
			yield return null;
		}

		gameObject.SetActive (false);


	}
}
