using UnityEngine;
using System.Collections;

public class RoomEnterTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            RoomController.instance.SetCurrentRoom(this.name);
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
