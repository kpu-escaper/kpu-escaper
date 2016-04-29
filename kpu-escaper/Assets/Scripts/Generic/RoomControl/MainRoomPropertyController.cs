using UnityEngine;
using System.Collections;

public abstract class MainRoomPropertyController : RoomPropertyController { // 룸 프로퍼티 컨트롤러 클래스에서 파생된 메인룸 컨트롤러 클래스

    private bool isClearConditionCompleted = false; // 클리어 조건이 해결 되었는가

    public override void Update()   // 해결 되었다면 Clear함수 실행
    {
        if(isClearConditionCompleted)
        {
            Clear();
            isClearConditionCompleted = false;
        }
    }

    public bool _isClearConditionCompleted
    {
        get { return isClearConditionCompleted; }
        set { isClearConditionCompleted = value; }
    }
    public abstract override void EnterRoom();
    public abstract override void LeaveRoom();
    public abstract void Clear();
}
