using UnityEngine;
using System.Collections;

public class LaserTrapManager : RoomPropertyController {

	Laser Laser;

	public override void EnterRoom()
	{
		Laser = transform.FindChild ("LaserCube").transform.FindChild ("Laser").GetComponent<Laser> ();
		Laser.instance.Launch ();
		Debug.Log("레이저 트랩 방에 들어왔습니다.");
	}
	
	public override void Update()
	{
	}
	
	public override void LeaveRoom()
	{
		Laser.instance.UnLaunch ();
		Debug.Log("레이저 트랩 방에서 나갔습니다.");
	}
}