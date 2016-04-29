using UnityEngine;
using System.Collections;

public class ClueManager : RoomPropertyController
{
    public override void EnterRoom()
    {
        Debug.Log("단서 방에 들어왔습니다.");
    }

    public override void Update()
    {

    }

    public override void LeaveRoom()
    {
        Debug.Log("단서 방에서 나갔습니다.");
    }
}
