using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HamburgerManager : MainRoomPropertyController{
    public GameObject BlockPrefab;
    public GameObject BombPrefab;
    public GameObject Explosion;

	public GameObject Wrong_Explosion;
	public GameObject Right_Effect;

    public Material Block_Red;
    public Material Block_Blue;
    public Material Block_Green;

	private AudioSource EsAudio;
	private AudioSource EsAudio2;
	private AudioSource EsAudio3;
	private AudioSource EsAudio4;
	private AudioSource EsAudio5;

	public AudioClip WrongSound;		// 틀림
	public AudioClip RightSound;		// 맞춤
	public AudioClip Dynamite;			// 다이나마이트 폭발
	public AudioClip HambergerInsert;	// 햄버거 올라오는소리
	public AudioClip Ham_Clear;			// 클리어소리

	int a = 1;

    float mTime = 0;
    List<GameObject> BlockList = new List<GameObject>();
   
    public override void EnterRoom()
    {
        //StartCoroutine("HamburgerGame");
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio.clip = this.RightSound;
		this.EsAudio.loop = false;

		this.EsAudio2 = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio2.clip = this.WrongSound;
		this.EsAudio2.loop = false;

		this.EsAudio3 = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio3.clip = this.HambergerInsert;
		this.EsAudio3.loop = false;

		this.EsAudio4 = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio4.clip = this.Dynamite;
		this.EsAudio4.loop = false;

		this.EsAudio5 = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio5.clip = this.Ham_Clear;
		this.EsAudio5.loop = false;


    }

    public override void Update()
    {
		base.Update ();
				
		// 스페이스를 누르면 섞이기 시작
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (a > 0) {
				StartCoroutine ("HamburgerGame");
				a--;
			}
		}
				

		if (!_isClearConditionCompleted) {
			mTime += Time.deltaTime;
			if (mTime > 25) {
				this.EsAudio5.Play();
				_isClearConditionCompleted = true;  // 승리처리
				RoomController.instance.UnBlockTheDoor ();

			}

			if (BlockList.Count > 1) {
				if (Input.GetKeyDown (KeyCode.Alpha2)) {
					//GetComponent<ParticleSystem>().Play()

					if (BlockList [BlockList.Count - 2].name == "Block_Blue") {
						this.EsAudio.Play ();	// 맞춘소리

						Instantiate (Right_Effect, new Vector3 (BlockList [BlockList.Count - 2].transform.position.x,
						                                        BlockList [BlockList.Count - 2].transform.position.y,
						                                        BlockList [BlockList.Count - 2].transform.position.z - 2.0f),
						            							BlockList [BlockList.Count - 2].transform.rotation);

						Destroy (BlockList [BlockList.Count - 2]);
						BlockList.RemoveAt (BlockList.Count - 2);
						for (int i = 0; i < BlockList.Count; ++i) {
							BlockList [i].transform.localPosition = new Vector3 (0, -3.5f + 0.4f * i, 0);
						}

					} else {
						this.EsAudio2.Play ();	// 틀린소리

						GameObject NewBlock = Instantiate (BlockPrefab, new Vector3 (0, -1000, 0), BlockPrefab.transform.rotation) as GameObject;
						NewBlock.transform.parent = gameObject.transform;
					
						switch (Random.Range (0, 3)) {
						case 0:
							NewBlock.name = "Block_Blue";
							NewBlock.GetComponent<Renderer> ().material = Block_Blue;
							break;
						case 1:
							NewBlock.name = "Block_Red";
							NewBlock.GetComponent<Renderer> ().material = Block_Red;
							break;
						case 2:
							NewBlock.name = "Block_Green";
							NewBlock.GetComponent<Renderer> ().material = Block_Green;
							break;
						}
						
						BlockList.Insert (0, NewBlock);
						this.EsAudio3.Play ();
					
					}
						
				} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
					if (BlockList [BlockList.Count - 2].name == "Block_Red") {
						this.EsAudio.Play ();
						Instantiate (Right_Effect, new Vector3 (BlockList [BlockList.Count - 2].transform.position.x,
							                                    BlockList [BlockList.Count - 2].transform.position.y,
							                                    BlockList [BlockList.Count - 2].transform.position.z - 2.0f),
							            						BlockList [BlockList.Count - 2].transform.rotation);


						Destroy (BlockList [BlockList.Count - 2]);
						BlockList.RemoveAt (BlockList.Count - 2);
	                    
						for (int i = 0; i < BlockList.Count; ++i) {
							BlockList [i].transform.localPosition = new Vector3 (0, -3.5f + 0.4f * i, 0);
						}
					} else {
						this.EsAudio2.Play ();
						
						GameObject NewBlock = Instantiate (BlockPrefab, new Vector3 (0, -1000, 0), BlockPrefab.transform.rotation) as GameObject;
						NewBlock.transform.parent = gameObject.transform;
						
						switch (Random.Range (0, 3)) {
						case 0:
							NewBlock.name = "Block_Blue";
							NewBlock.GetComponent<Renderer> ().material = Block_Blue;
							break;
						case 1:
							NewBlock.name = "Block_Red";
							NewBlock.GetComponent<Renderer> ().material = Block_Red;
							break;
						case 2:
							NewBlock.name = "Block_Green";
							NewBlock.GetComponent<Renderer> ().material = Block_Green;
							break;
						}
						
						BlockList.Insert (0, NewBlock);
						this.EsAudio3.Play ();
					}
	
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					if (BlockList [BlockList.Count - 2].name == "Block_Green") {
						this.EsAudio.Play ();
						Instantiate (Right_Effect, new Vector3 (BlockList [BlockList.Count - 2].transform.position.x,
						                                    BlockList [BlockList.Count - 2].transform.position.y,
						                                    BlockList [BlockList.Count - 2].transform.position.z - 2.0f),
						            						BlockList [BlockList.Count - 2].transform.rotation);
					


						Destroy (BlockList [BlockList.Count - 2]);
						BlockList.RemoveAt (BlockList.Count - 2);
						for (int i = 0; i < BlockList.Count; ++i) {
							BlockList [i].transform.localPosition = new Vector3 (0, -3.5f + 0.4f * i, 0);
						}

					} else {
						this.EsAudio2.Play ();
						GameObject NewBlock = Instantiate (BlockPrefab, new Vector3 (0, -1000, 0), BlockPrefab.transform.rotation) as GameObject;
						NewBlock.transform.parent = gameObject.transform;
						switch (Random.Range (0, 3)) {
						case 0:
							NewBlock.name = "Block_Blue";
							NewBlock.GetComponent<Renderer> ().material = Block_Blue;
							break;
						case 1:
							NewBlock.name = "Block_Red";
							NewBlock.GetComponent<Renderer> ().material = Block_Red;
							break;
						case 2:
							NewBlock.name = "Block_Green";
							NewBlock.GetComponent<Renderer> ().material = Block_Green;
							break;
						}
						
						BlockList.Insert (0, NewBlock);
						this.EsAudio3.Play ();

					}
				}

			}
		}
	}


    public override void Clear()
    {
        mTime = 0;
        foreach(GameObject block in BlockList)
        {
			//BlockList.Remove(block);
			Destroy(block);
        }
		BlockList.Clear ();
		        
        StopCoroutine("HamburgerGame");
    }

    public override void LeaveRoom()
    {
        //Explosion.SetActive(false);
    }

    IEnumerator HamburgerGame()
    {

        GameObject Bomb = Instantiate(BombPrefab, new Vector3(0, -1000, 0), BombPrefab.transform.rotation) as GameObject;
        Bomb.transform.parent = gameObject.transform;
        BlockList.Add(Bomb);
        while (BlockList.Count<20)
        {
            GameObject NewBlock = Instantiate(BlockPrefab, new Vector3(0, -1000, 0), BlockPrefab.transform.rotation) as GameObject;
            NewBlock.transform.parent = gameObject.transform;
            switch(Random.Range(0,3))
            {
                case 0:
                    NewBlock.name = "Block_Blue";
                    NewBlock.GetComponent<Renderer>().material = Block_Blue;
                    break;
                case 1:
                    NewBlock.name = "Block_Red";
                    NewBlock.GetComponent<Renderer>().material = Block_Red;
                    break;
                case 2:
                    NewBlock.name = "Block_Green";
                    NewBlock.GetComponent<Renderer>().material = Block_Green;
                    break;
            }
            
            BlockList.Insert(0, NewBlock);
			this.EsAudio3.Play ();

            for(int i=0;i <BlockList.Count; ++i)
            {
                BlockList[i].transform.localPosition = new Vector3(0, -3.5f + 0.4f * i, 0);
            }
			// 블렇쌓이는 속도조절
            yield return new WaitForSeconds(0.25f);
        }

        //.SetActive(true);
		Instantiate( Explosion, BombPrefab.transform.position, BombPrefab.transform.rotation);
			
        _isClearConditionCompleted = true;
        // 여기서 패배처리

		this.EsAudio4.Play ();





    }
}
