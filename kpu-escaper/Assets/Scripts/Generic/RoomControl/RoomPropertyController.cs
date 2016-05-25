using UnityEngine;
using System.Collections;

//방에 들어가고 나올때 처리해주는 기본 베이스 클래스
public abstract class RoomPropertyController : MonoBehaviour{
    private GameObject Player;  // 플레이어

    public GameObject _Player { get { return Player; } set { Player = value; } }

	// Use this for initialization
	public abstract void EnterRoom ();  //들어갈때
	public abstract void Update ();     //안에서 업데이트 루틴
    public abstract void LeaveRoom ();  //나갈때
}