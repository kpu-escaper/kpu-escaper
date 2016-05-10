using UnityEngine;
using System.Collections;

public class LaserTrapManager : RoomPropertyController
{
    ArcReactor_Launcher Laser;

    public bool[] Hit;

    public override void EnterRoom()
    {

        Laser = transform.FindChild("LaserCube").GetComponent<ArcReactor_Launcher>();
        Laser.LaunchRay();
        Debug.Log("레이저 트랩 방에 들어왔습니다.");
    }

    public override void Update()
    {
    }

    public override void LeaveRoom()
    {
        foreach(ArcReactor_Launcher.RayInfo ray in Laser.rays)
        {
            ray.arc.playbackType = ArcReactor_Arc.ArcsPlaybackType.once;
            ray.arc.elapsedTime = 4.9f;
            
            for(int i=0; i<6; ++i)
                Hit[i] = false;
        }
        Debug.Log("레이저 트랩 방에서 나갔습니다.");
    }
}
