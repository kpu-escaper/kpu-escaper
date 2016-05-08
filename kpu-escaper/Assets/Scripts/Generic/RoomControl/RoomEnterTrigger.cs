using UnityEngine;
using System.Collections;

public class RoomEnterTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            RoomController.instance.SetCurrentRoom(this.name);
			LiftManager.instance.SetCurrentPosition(RoomController.instance.GetCurrentRoomTransform());
			LiftController.instance.SetInitPos();
			if(RoomController.instance.isMainGame()){
				LiftManager.instance.TurnOffManager();
			}
        }
    }
    /*
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            RoomController.instance.LeaveCurrentRoom(this.name);
        }
    }
    */
}
