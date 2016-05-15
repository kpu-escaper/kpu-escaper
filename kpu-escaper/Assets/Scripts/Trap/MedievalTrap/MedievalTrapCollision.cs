using UnityEngine;
using System.Collections;

public class MedievalTrapCollision : MonoBehaviour {
	
	private AudioSource EsAudio;	// 오디오 플레이어
	public AudioClip Needle_Hit;		// 가시 쳐맞을때 사운드
	
	void OnCollisionEnter(Collision col)
	{
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.Needle_Hit;
		this.EsAudio.loop = false;
		
		if (col.gameObject.CompareTag("Player"))
		{
			HPManager.instance.GetDamage(5);
			HPManager.instance.SetBloodColor("Red");
			
			
			
			
		}
	}
}
