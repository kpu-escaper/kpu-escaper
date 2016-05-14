using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum RoomType // 큐브의 속성
{
    Safe,
    Trap,
    Main,
    Clue,
    Error
}

public enum RoomColor   // 큐브의 색상
{
    Red,
    Green,
    Blue,
    White,
    Error
}

public class Room
{
    RoomColor color;    // 현재 큐브 색상
    RoomType type;      // 현재 큐브 속성
    int number;         // 방 번호
    string name;        // 방 이름

    public Room(RoomColor _color, RoomType _type, int _number, string _name) { color = _color; type = _type; number = _number; name = _name; }

    public RoomColor _color { get { return color; } set { color = value; } }
    public RoomType _type { get { return type; } set { type = value; } }
    public int _number { get { return number; } set { number = value; } }
    public string _name { get { return name; } set { name = value; } }
}

public class RoomController : MonoBehaviour
{
	public GameObject tempMain;

    public static RoomController instance;              // 싱글톤

    Room[] SurroundRoomsInfo = new Room[6];             // 주변 방 정보
    Room CurrentRoom;                                   // 현재 방 정보

    GameObject[] RoomsObject;                           // 실제 방 게임오브젝트

    RoomColor[] Expression = new RoomColor[4];          // Expression 0 = 합, 1 = 곱, 2 = Abs, 3 = 걍넘버 // 컬러랑 계산식이랑 대응
    List<int> RoomNumberController = new List<int>();   // 현재 방 번호 관리

    GameObject[] RoomModule;                                    // 방 게임, 트랩등 모듈
    List<GameObject> TrapModule = new List<GameObject>();       //트랩 모듈
    List<GameObject> MainGameModule = new List<GameObject>();   //메인 게임 모듈
    List<GameObject> ClueModule = new List<GameObject>();       //단서 모듈

    GameObject CurrentModule;                           // 현재 방 모듈
    RoomPropertyController RoomPropertyRoutine;

	GameObject TempModuleParent; ////////////////////////// for Demo
	GameObject TempCurrentModule; ////////////////////////// for Demo
	int TempIndex = 0; ////////////////////////// for Demo

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
        ConnectColorToProperty();
        SetRoomsProperty();
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomPropertyRoutine != null)
        {
            RoomPropertyRoutine.Update();
        }
    }

    //////////// 초기화 및 색깔과 연산 연결
    void Initialize()
    {
		TempModuleParent = GameObject.Find("GameModule"); ////////////////////////// for Demo

        RoomsObject = new GameObject[transform.childCount];
        for (int RoomObjectIndex = 0; RoomObjectIndex < transform.childCount; ++RoomObjectIndex)
        {
            RoomsObject[RoomObjectIndex] = transform.GetChild(RoomObjectIndex).gameObject;
        }
        for (int i = 2; i < 1000; ++i)
            RoomNumberController.Add(i);

        RoomsObject[0].name = "EscaperCube-1";                                                  //첫방은 무조건 1번, 안전
        CurrentRoom = new Room(RandomColorGenerate(), RoomType.Safe, 1, RoomsObject[0].name);

        GameObject ModuleParent = GameObject.Find("GameModule");                                //게임모듈에서 방의 모듈들을 모두 리스트에 저장
        for (int i = 0; i < ModuleParent.transform.childCount; ++i)
        {
            GameObject ChildObject = ModuleParent.transform.GetChild(i).gameObject;
            switch (ChildObject.tag)
            {
                case "Trap":
                    TrapModule.Add(ChildObject);
                    break;
                case "MainGame":
                    MainGameModule.Add(ChildObject);
                    break;
                case "Clue":
                    ClueModule.Add(ChildObject);
                    break;
            }
        }
    }

    void ConnectColorToProperty()
    {
        int[] Exp = new int[4];
        for (int i = 0; i < 4; ++i)
        {
            NotReduplicationRandom(Exp, i);                 //안겹치게 랜덤
            switch (Exp[i])                                 //합,곱,차,번호 공식에 랜덤 색깔 연결하기
            {
                case 0:
                    Expression[i] = RoomColor.Red;
                    break;
                case 1:
                    Expression[i] = RoomColor.Blue;
                    break;
                case 2:
                    Expression[i] = RoomColor.Green;
                    break;
                case 3:
                    Expression[i] = RoomColor.White;
                    break;
            }
        }
    }

    //////////// 색상 및 방 번호, 방타입 생성
    public void SetCurrentRoom(string _Name)                //현재방 지정
    {
        foreach (Room RoomsInfo in SurroundRoomsInfo)
        {
            if (RoomsInfo._name == _Name)
            {
                CurrentRoom = RoomsInfo;
                SetRoomsProperty();
            }
        }
	}

    public void LeaveCurrentRoom(string _Name)              //현재방에서 나감
    {
        foreach (Room RoomsInfo in SurroundRoomsInfo)
        {
            if (RoomsInfo._name == _Name)
            {
                if (CurrentModule != null)
                {
                    if (RoomPropertyRoutine != null)
                    {
                        RoomPropertyRoutine.LeaveRoom();
                    }
                    CurrentModule.SetActive(false);
                }
            }
        }

    }
    void SetRoomsProperty()                             //방속성 지정
    {
        int CurrentRoomIndex = 0;
        Vector3 CurrentRoomPosition = Vector3.zero;

        //////////// 현재 들어가있는 방 찾기
        for (int i = 0; i < RoomsObject.Length; ++i)
        {
            if (RoomsObject[i].name == CurrentRoom._name)
            {
                CurrentRoomIndex = i;
                CurrentRoomPosition = RoomsObject[i].transform.position;
                break;
            }
        }

        //////////////////////// 모듈 부분 ////////////////////////
        //////////// 이전방 정리
        if (CurrentModule != null)
        {
            if (RoomPropertyRoutine != null)
            {
                RoomPropertyRoutine.LeaveRoom();
            }
            CurrentModule.SetActive(false);
        }
        //////////// 방 루틴 작동
		/*switch(CurrentRoom._type)  ////////////////////////// for Demo
        {
            case RoomType.Trap:
                CurrentModule = TrapModule[Random.Range(0, TrapModule.Count)];
                CurrentModule.SetActive(true);
                break;
            case RoomType.Clue:
                CurrentModule = ClueModule[Random.Range(0, ClueModule.Count)];
                CurrentModule.SetActive(true);
                break;
            case RoomType.Main:
                CurrentModule = MainGameModule[Random.Range(0, MainGameModule.Count)];
				Debug.Log (CurrentModule.name);
                CurrentModule.SetActive(true);
                break;
            case RoomType.Safe:
                CurrentModule = null;
                break;
        }*/
		if (CurrentRoom._name != "EscaperCube-1") { ////////////////////////// for Demo
			if(TempIndex == TempModuleParent.transform.childCount)
				TempIndex = 0;
			CurrentModule = TempModuleParent.transform.GetChild (TempIndex).gameObject; ////////////////////////// for Demo


			CurrentModule.SetActive (true); ////////////////////////// for Demo
			++TempIndex; ////////////////////////// for Demo

			if(CurrentModule.CompareTag("MainGame")){
				LiftManager.instance.TurnOffManager();
				BlockTheDoor();
			}
			else{
				LiftManager.instance.TurnOnManager();				
				UnBlockTheDoor();
			}
		}

        if (CurrentModule != null)
        {
            CurrentModule.transform.position = CurrentRoomPosition;
            RoomPropertyRoutine = CurrentModule.GetComponent<RoomPropertyController>();
            if (RoomPropertyRoutine != null)
            {
                if (RoomPropertyRoutine._Player == null)
                    RoomPropertyRoutine._Player = GameObject.FindGameObjectWithTag("Player");
                RoomPropertyRoutine.EnterRoom();
            }
        }

        //////////////////////// 주변방 생성 부분 ////////////////////////

        for (int i = 0; i < SurroundRoomsInfo.Length; ++i)
        {
            int SurroundRoomNumber = NotReduplicationSurroundRoom(SurroundRoomsInfo, i);        //안겹치게 랜덤번호 생성
            RoomColor SurroundRoomColor = RandomColorGenerate();                                //랜덤색상 생성
            RoomType SurroundRoomType = CalculateRoomType(CurrentRoom._color, SurroundRoomNumber);  // 색상과 번호에 따른 방 타입 지정
            SurroundRoomsInfo[i] = new Room(SurroundRoomColor, SurroundRoomType, SurroundRoomNumber, "EscaperCube-" + SurroundRoomNumber.ToString());
            Debug.Log("주변방 " + (i + 1).ToString() + ": 방번호 " + SurroundRoomNumber.ToString() + "번, 색상 : " + SurroundRoomColor.ToString() + ", 방속성 : " + SurroundRoomType.ToString());
        }

        for (int i = 0, j = 0; i < RoomsObject.Length; ++i)     // 현재 주변 방 속성을 가지고, 주변방 실제 게임오브젝트를 맞춰줌
        {
            if (i == CurrentRoomIndex)
                continue;
            RoomsObject[i].name = SurroundRoomsInfo[j]._name;
            SetRoomColor(SurroundRoomsInfo[j]._color, RoomsObject[i].transform);
            ++j;
        }


        //////////// 방 배치 (방중에 현재방을 뺀 나머지 오브젝트를 맞춰줌
        bool[] ReduplicationCheck = { true, true, true, true, true, true };
        for (int i = 0; i < RoomsObject.Length; ++i)
        {
            if (RoomsObject[i].name != CurrentRoom._name)
            {
                for (int j = 0; j < 6; ++j)
                {
                    if (ReduplicationCheck[j])
                    {
                        SetRoomPosition(i, j, CurrentRoomPosition);
						ReduplicationCheck[j] = false;
                        break;
                    }
                }
            }
        }
        //RoomsObject[]
		DoorArrange ();
		BlockTheDoor();

    }

    void SetRoomPosition(int _RoomIndex, int _DuplicationCheckIndex, Vector3 _CurrentRoomPosition)  //주변방의 위치를 맞춰줌
    {
        switch (_DuplicationCheckIndex)
        {
            case 0:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x - 8.36f, _CurrentRoomPosition.y, _CurrentRoomPosition.z);
                break;
            case 1:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x + 8.36f, _CurrentRoomPosition.y, _CurrentRoomPosition.z);
                break;
            case 2:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x, _CurrentRoomPosition.y + 8.36f, _CurrentRoomPosition.z);
                break;
            case 3:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x, _CurrentRoomPosition.y - 8.36f, _CurrentRoomPosition.z);
                break;
            case 4:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x, _CurrentRoomPosition.y, _CurrentRoomPosition.z - 8.36f);
                break;
            case 5:
                RoomsObject[_RoomIndex].transform.position = new Vector3(_CurrentRoomPosition.x, _CurrentRoomPosition.y, _CurrentRoomPosition.z + 8.36f);
                break;

        }
    }

	//현재방의 문은 on 주변방의 문은 off
	void DoorArrange(){		
		for (int i=0; i<RoomsObject.Length; ++i) {
			if (RoomsObject [i].name == CurrentRoom._name) {
				FindChild (true, RoomsObject[i].transform);
			}
			else{
				FindChild(false, RoomsObject[i].transform);
			}
		}
	}

	//임시로 기능적인 부분만 실행하기 위해서 작성
	void BlockTheDoor(){
		for (int i = 0; i < RoomsObject.Length; ++i)
		{
			if (RoomsObject[i].name == CurrentRoom._name && CurrentRoom._type == RoomType.Main)
			{
				//Debug.Log (CurrentRoom._type);
				RoomsObject[i].transform.FindChild("wall_1").GetComponent<DoorController>().block = true;
				RoomsObject[i].transform.FindChild("wall_2").GetComponent<DoorController>().block = true;
				RoomsObject[i].transform.FindChild("wall_3").GetComponent<DoorController>().block = true;
				RoomsObject[i].transform.FindChild("wall_4").GetComponent<DoorController>().block = true;
				RoomsObject[i].transform.FindChild("wall_5").GetComponent<DoorController>().block = true;
				RoomsObject[i].transform.FindChild("wall_6").GetComponent<DoorController>().block = true;
			}
		}
	}
	//임시로 기능적인 부분만 실행하기 위해서 작성
	public void UnBlockTheDoor(){
		for (int i = 0; i < RoomsObject.Length; ++i) {
			if (RoomsObject [i].name == CurrentRoom._name) {
				RoomsObject [i].transform.FindChild ("wall_1").GetComponent<DoorController> ().block = false;
				RoomsObject [i].transform.FindChild ("wall_2").GetComponent<DoorController> ().block = false;
				RoomsObject [i].transform.FindChild ("wall_3").GetComponent<DoorController> ().block = false;
				RoomsObject [i].transform.FindChild ("wall_4").GetComponent<DoorController> ().block = false;
				RoomsObject [i].transform.FindChild ("wall_5").GetComponent<DoorController> ().block = false;
				RoomsObject [i].transform.FindChild ("wall_6").GetComponent<DoorController> ().block = false;
			}
		}			
	}
	
	void FindChild(bool _Current, Transform _RoomObject){
		for (int ChildIndex = 0; ChildIndex <_RoomObject.childCount; ++ChildIndex) {
			Transform childTransform = _RoomObject.GetChild(ChildIndex);
			if(childTransform.CompareTag("Door")){
				childTransform.gameObject.SetActive(_Current);
			}FindChild(_Current, childTransform);
		}
	}

    void SetRoomColor(RoomColor _Color, Transform _RoomObject) //실제 게임오브젝트의 방 색을 바꿔주는 루틴
    {
        for (int ChildIndex = 0; ChildIndex < _RoomObject.childCount; ++ChildIndex)
        {

            Transform childTransform = _RoomObject.GetChild(ChildIndex);
            Renderer ObjectRenderer = childTransform.GetComponent<Renderer>();

            if (ObjectRenderer)
            {
                Color materialColor = Color.white;
                switch (_Color)
                {
                    case RoomColor.Blue:
                        materialColor = Color.blue + Color.cyan;
                        break;
                    case RoomColor.Green:
						materialColor = Color.blue + Color.green + Color.yellow;
                        break;
                    case RoomColor.Red:
						materialColor = Color.red + Color.white;
                        break;
                    case RoomColor.White:
						materialColor = Color.white;
                        break;
                }
                ObjectRenderer.material.SetColor("_EmissionColor", materialColor);  // Emission Color로 조정해주는 중
            }
            SetRoomColor(_Color, childTransform);   // 하위 오브젝트 전부 적용
        }
    }

    //////////// 색상 및 방 번호, 방타입 생성
    RoomColor RandomColorGenerate()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return RoomColor.Blue;
            case 1:
                return RoomColor.Green;
            case 2:
                return RoomColor.Red;
            case 3:
                return RoomColor.White;
        }
        return RoomColor.Error;
    }
    int RandomRoomNumberGenerate()
    {
        int RoomIndex = Random.Range(0, RoomNumberController.Count);
        return RoomNumberController[RoomIndex];
    }

    // 방계산
    RoomType CalculateRoomType(RoomColor CurrentRoomColor, int DestinationRoomNumber)
    {
        for (int i = 0; i < Expression.Length; ++i)
        {
            if (CurrentRoomColor == Expression[i])
            {
                int One = DestinationRoomNumber % 10;           //1의자리
                int Ten = DestinationRoomNumber % 100 - One;    //10의자리
                int Hundred = DestinationRoomNumber / 100;      //100의자리

                switch (i)
                {
                    case 0: //합
                        if ((One + Ten + Hundred) % 2 == 0)
                            return RoomType.Safe;
                        else
                            return RoomType.Main;
                    case 1: //곱
                        if ((One * Ten * Hundred) % 2 == 0)
                            return RoomType.Main;
                        else
                            return RoomType.Clue;
                    case 2: //차
                        if (Mathf.Abs(Hundred - Ten - One) % 2 == 0)
                            return RoomType.Clue;
                        else
                            return RoomType.Trap;
                    case 3: //번호
                        if (DestinationRoomNumber % 2 == 0)
                            return RoomType.Trap;
                        else
                            return RoomType.Safe;
                }
            }
        }
        return RoomType.Error;
    }

    //겹치지 않는 랜덤들
    void NotReduplicationRandom(int[] _Exp, int _Index)
    {
        _Exp[_Index] = Random.Range(0, 4);
        for (int j = 0; j < _Index; ++j)
        {
            if (_Exp[_Index] == _Exp[j])
            {
                NotReduplicationRandom(_Exp, _Index);
                break;
            }
        }
    }
    int NotReduplicationSurroundRoom(Room[] _Exp, int _Index)
    {
        int temp = RandomRoomNumberGenerate();
        for (int j = 0; j < _Index; ++j)
        {
            if (temp == _Exp[j]._number)
            {
                temp = NotReduplicationSurroundRoom(_Exp, _Index);
                break;
            }
        }
        return temp;
    }

	public Transform GetCurrentRoomTransform(){
		int index = 0;
		for (int i = 0; i < RoomsObject.Length; ++i) {
			if (RoomsObject [i].name == CurrentRoom._name) {
				index = i;
			}
		}
		return RoomsObject [index].transform;
	}

	public bool isMainGame(){
		if (CurrentRoom._type == RoomType.Main) {
			return true;
		} else {
			return false;
		}
	}

	//만약 한번 클리어한 메인게임이면 메인게임 모듈에서 제거
	public void RemoveMainGame(string MainGameName){
		if (MainGameModule.Count == 1) {
			tempMain.transform.SetParent (GameObject.Find ("GameModule").transform);
			MainGameModule.Add(tempMain);
		}
		for (int i = 0; i < MainGameModule.Count; ++i) {
			if (MainGameModule [i].name == MainGameName && MainGameModule[i].name != "testMain") {
				MainGameModule.Remove (MainGameModule [i]);
			}
		}
	}

}