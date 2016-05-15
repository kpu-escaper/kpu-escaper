using UnityEngine;
using System.Collections;

public class FindMineManager : MainRoomPropertyController
{
    public GameObject BlockPrefab;

	public GameObject Mine_Explosion;
	public GameObject Right_Effect;

	public GameObject NumberBlockPrefab;

	int ClearCount = 0;


	// 메테리얼들 ( 클릭전, 클릭 후 0, 1, 2, 3, X, 지뢰 )

    public Material PlaneMaterial;	// 뒤집기 전 색깔
    public Material AfterClickPlaneMaterial;	// 뒤집고 난 후 색깔

	public Material ZeroMaterial;
    public Material OneMaterial;
    public Material TwoMaterial;
    public Material ThreeMaterial;
    public Material QuestionMaterial;

	// 사운드
	private AudioSource EsAudio;	// 마인 오디오 플레이어
	private AudioSource EsAudio2;	// 마인 오디오 플레이어


	public AudioClip Mine_Clear;			// 오디오 CD
	public AudioClip MineBlock_Open;		// 판 여는소리
	public AudioClip MineExplosion;


    int[,] MineArray = new int[5, 5];	

	GameObject[,] MineObject = new GameObject[5, 5];	// 발판
	GameObject[,] MineNumber = new GameObject[5, 5];	// 발판위에 생성될 숫자 오브젝트	

    void Awake()
    {
		this.EsAudio = this.gameObject.AddComponent<AudioSource> ();
		this.EsAudio2 = this.gameObject.AddComponent<AudioSource> ();

		// 지뢰가 아닌 숫자 클릭시 발판열림소리
		this.EsAudio.clip = this.MineBlock_Open;
		this.EsAudio.loop = false;

		// 지뢰폭발 사운드
		this.EsAudio2.clip = this.MineExplosion;
		this.EsAudio2.loop = false;



		// 초기화, 지뢰찾기 생성
        for (int i = 0; i < 5; ++i)
            for (int j = 0; j < 5; ++j)
            {
                MineArray[i, j] = 0;
                MineObject[i, j] = Instantiate(BlockPrefab) as GameObject;

                MineObject[i, j].transform.parent = gameObject.transform;
				MineObject[i, j].transform.rotation = gameObject.transform.rotation;

                MineObject[i, j].transform.name = i.ToString() + "," + j.ToString();

                //MineObject[i, j].transform.localPosition = new Vector3(-3 + i * 1.5f, -3.7f, -3 + j * 1.5f);
				MineObject[i, j].transform.localPosition = new Vector3(-2 + i * 1.0f, -3.8f, -2 + j * 1.0f);
				
			
				MineNumber[i, j] = Instantiate( NumberBlockPrefab ) as GameObject;

				MineNumber[i, j].transform.parent = gameObject.transform;
				MineNumber[i, j].transform.rotation = gameObject.transform.rotation;
			
				MineNumber[i, j].transform.localPosition = new Vector3( MineObject[i,j].transform.position.x,
			                                                       		MineObject[i,j].transform.position.y + 0.3f,
			                                                       		MineObject[i,j].transform.position.z);
			
				//MineNumber[i,j].transform.localRotation =  Quaternion.Euler( new Vector3 (-90, 0, 0) );
						
				MineNumber[i,j].SetActive(false);
	        }
    }
    // Use this for initialization

    public override void EnterRoom()
    {
		// 마우스 클릭 이벤트 등록
        StartCoroutine("FindMineGame");
        RayEvent.OnLeftClick += OnLeftClick;
        RayEvent.OnRightClick += OnRightClick;
        RayEvent.OnHover += OnHover;
        RayEvent.OnHoverEnd += OnHoverEnd;
    }

    public override void Update()
    {
        base.Update();
    }

	// 클리어 했을 때 오브젝트 모두 사라짐 + 문이 열려야함
	// 서서히 사라지거나 클리어 메시지 뜨도록 수정
    public override void Clear()
    {
		this.EsAudio.clip = this.Mine_Clear;
		this.EsAudio.loop = false;

		this.EsAudio.Play ();

        StopCoroutine("FindMineGame");
		StartCoroutine ("SlowClear");

		RoomController.instance.UnBlockTheDoor();



		Debug.Log("메인게임 지뢰찾기 클리어 했습니다.");

		
    }

	// 클릭 이벤트 등록해제
    public override void LeaveRoom()
    {
        RayEvent.OnLeftClick -= OnLeftClick;
        RayEvent.OnRightClick -= OnRightClick;
        RayEvent.OnHover -= OnHover;
        RayEvent.OnHoverEnd -= OnHoverEnd;
    }

	// 왼쪽 마우스 버튼을 클릭 했을 때
    void OnLeftClick(GameObject obj)
    {
		if (obj.CompareTag("FindMineBlock"))	// 클릭한 곳이 발판이라면 아래 로직 실행
        {
            for(int i=0;i<5;++i)
			{
				for(int j=0;j<5;++j)
            	{
                	if(MineObject[i,j].name == obj.name)
                	{
                    	CalculateMine(i, j);
                	}
            	}
			}
        }
    }

	// 우클릭 누를 시에 X생성(깃발)
    void OnRightClick(GameObject obj)
    {
        if (obj.CompareTag("FindMineBlock"))
        {
            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                {
                    if (MineObject[i, j].name == obj.name)
                    {
					if(MineArray[i,j] != 3){
						MineArray[i,j] = 3;
                        //MineObject[i, j].GetComponent<Renderer>().material = QuestionMaterial;
						MineNumber[i,j].GetComponent<Renderer>().material = QuestionMaterial;
						MineNumber[i,j].SetActive(true);
                    }
					else{
						MineNumber[i,j].SetActive(false);
						MineArray[i,j] = 0;
					}
					}
					
                }
        }
    }

	// 하이라이트
    void OnHover(GameObject obj)
    {
        if (obj.CompareTag("FindMineBlock"))
        {
			obj.GetComponent<Renderer>().material = AfterClickPlaneMaterial;
        }
    }

	// 하이라이트 끄기
    void OnHoverEnd(GameObject obj)
    {
        if (obj.CompareTag("FindMineBlock"))
        {
            if (obj.GetComponent<Renderer>().material.mainTexture == QuestionMaterial.mainTexture)
            {
                obj.GetComponent<Renderer>().material.color = Color.white;
                return;
            }
            else
            {
                obj.GetComponent<Renderer>().material.color = PlaneMaterial.color;
                return;
            }
        }
    }

	// 메인 로직
    IEnumerator FindMineGame()
    {

        Initialize();	// 시작할 때 초기화해줌

        for (int i = 0; i < 3; ++i)
        {
            NotReduplicationRandom(true);   // 지뢰 생성
        }

        for (int i = 0; i < 5; ++i)
        {
            NotReduplicationRandom(false);  // 처음에 보여줄곳 생성
        }

		// 처음에 보여줄 지뢰가 아닌 3개의 숫자 계산
        for (int i = 0; i < 5; ++i)
            for (int j = 0; j < 5; ++j)
            {
                if (MineArray[i, j] == 2)
                {
                    CalculateMine(i, j);
                }
            }
        return null;
    }

    void Initialize()
    {
        for (int i = 0; i < 5; ++i)
            for (int j = 0; j < 5; ++j)
            {
                MineArray[i, j] = 0;
                MineObject[i, j].GetComponent<Renderer>().material = PlaneMaterial;
                MineObject[i, j].tag = "FindMineBlock";
                MineObject[i, j].SetActive(true);
            }
    }

	// 마인의 위치가 중복되지 않도록
    void NotReduplicationRandom(bool isMine)
    {
        int randX = Random.Range(0, 5);
        int randY = Random.Range(0, 5);
        if (MineArray[randX, randY] != 0)
        {
            NotReduplicationRandom(isMine);
            return;
        }
        if (isMine)	// 지뢰이면 int배열에 1을 넣어둠(체크하기위해)
            MineArray[randX, randY] = 1;
        else       // 숫자이면 int배열에 2를 넣어둠 
            MineArray[randX, randY] = 2;
    }

    void CalculateMine(int x, int y)
    {
        switch (MineArray[x, y])	// 현재 클릭한 위치가 지뢰인지 아닌지를 int배열에 넣어 둔 값으로 검사
        {
            case 1:	// 지뢰라면
				MineObject[x, y].GetComponent<Renderer>().material.color = new Color(0.9f, 0.15f, 0.15f);
                transform.GetChild(0).localPosition = MineObject[x, y].transform.localPosition;
                transform.GetChild(0).gameObject.SetActive(true);
			// transform.getchild(순서)  오브젝트 하위에 자식 오브젝트를 순서대로 접근할때 사용

				//Invoke("DisableExplosion", 2.0f);
                // 게임오버 처리
				
				Instantiate( Mine_Explosion, MineObject[x,y].transform.position, MineObject[x,y].transform.rotation );
				
				this.EsAudio2.Play ();

                break;
            
			default:	// 지뢰가 아니라면
                int mine = 0;

				this.EsAudio.Play ();

				ClearCount++;


				// 현재 위치를 중심으로 8방향 검사 후 지뢰가 몇개 있는지 계산  
                for (int i = -1; i <= 1; ++i)	
                {
                    for (int j = -1; j <= 1; ++j)
                    {
                        int beforeClampIteratorX = x + i;
                        int beforeClampIteratorY = y + j;

                        int afterClampIteratorX = Mathf.Clamp(beforeClampIteratorX, 0, 4);
                        int afterClampIteratorY = Mathf.Clamp(beforeClampIteratorY, 0, 4);

                        if ((beforeClampIteratorX != afterClampIteratorX) || (beforeClampIteratorY != afterClampIteratorY))
                        {
                            continue;
                        }
						
						// 클릭한 발판 주위로 총 9개의 발판에 총 지뢰의 갯수를 알아냄
                        if (MineArray[beforeClampIteratorX, beforeClampIteratorY] == 1)
                        {
                            mine++;
                        }

						Instantiate( Right_Effect, MineNumber[x,y].transform.position, MineNumber[x,y].transform.rotation );
                    }
                }

                switch (mine)	// 숫자에 따라 메테리얼을 바꿔줌
                {
                    case 0:
						MineNumber[x,y].GetComponent<Renderer>().material = ZeroMaterial;
						break;

					case 1:
						MineNumber[x,y].GetComponent<Renderer>().material = OneMaterial;
                        break;
                    
					case 2:
						MineNumber[x,y].GetComponent<Renderer>().material = TwoMaterial;					
						break;

                    case 3:
						MineNumber[x,y].GetComponent<Renderer>().material = ThreeMaterial;
                        break;
                }
                break;
        }
		MineNumber[x, y].SetActive (true);
        MineObject[x, y].tag = "Untagged";	// 이미 클릭한 발판은 클릭되지 않게 태그를 바꿔줌
        
		// 3개인 지뢰 이외의 발판을 모두 열면 클리어하게 되는 로직
		/*for(int i=0; i<transform.childCount; ++i)
        {
            if(transform.GetChild(i).CompareTag("FindMineBlock"))
            {
                numofCurrentBeforeBlock++;
            }
        }
        if (numofCurrentBeforeBlock <= 3)
			_isClearConditionCompleted = true;*/

		if (ClearCount >= 22)
			_isClearConditionCompleted = true;
        
    }

	// 서서히 없어지게 보이도록
	IEnumerator SlowClear()
	{

		yield return new WaitForSeconds(3.0f);

		float fAlpha = 0.51f;

		// 1. 회전하면서 야바위가 하얗게 없어지는 것처럼 보이기 위해 알파값을 점점 줄여준다. 
		while (fAlpha >= 0) 
		{
			fAlpha -= 0.01f;

			for (int i = 0; i < 5; ++i) 
			{
				for (int j = 0; j < 5; ++j) 
				{
					MineObject [i, j].GetComponent<Renderer> ().material.SetColor ("_Color", new Color (1, 1, 1, fAlpha));
					MineNumber [i, j].GetComponent<Renderer> ().material.SetColor ("_Color", new Color (1, 1, 1, fAlpha));
				}
			}
			yield return null;
		}

		for (int i = 0; i < 5; ++i) 
		{
			for (int j = 0; j < 5; ++j) 
			{
				MineObject [i, j].SetActive (false);
				MineNumber [i, j].SetActive (false);
				
			}
		}



	}

    //void DisableExplosion()
    //{
    //    transform.GetChild(0).gameObject.SetActive(false);
    //}
}