using UnityEngine;
using System.Collections;

public class LaserWallCollision : MonoBehaviour
{
    LaserTrapManager Manager;
    public void ArcReactorReflection(ArcReactorHitInfo hit)
    {
        switch (this.name)
        {
            case "Cube":
                Manager.Hit[0] = true;
                break;
            case "Cube (1)":
                Manager.Hit[1] = true;
                break;
            case "Cube (2)":
                Manager.Hit[2] = true;
                break;
            case "Cube (3)":
                Manager.Hit[3] = true;
                break;
            case "Cube (4)":
                Manager.Hit[4] = true;
                break;
            case "Cube (5)":
                Manager.Hit[5] = true;
                break;

        }
        bool AllHit = true;
        foreach(bool isHit in Manager.Hit)
        {
            if (!isHit)
                AllHit = false;
        }
        if (AllHit)
        {
            hit.rayInfo.arc.playbackType = ArcReactor_Arc.ArcsPlaybackType.once;
            hit.rayInfo.arc.elapsedTime = 4.9f;
            
            for(int i=0; i<6; ++i)
                Manager.Hit[i] = false;
            Invoke("LaunchRayAfterSeconds", 1.0f);
        }
    }

    void LaunchRayAfterSeconds()
    {
        GameObject.Find("LaserCube").GetComponent<ArcReactor_Launcher>().LaunchRay();
    }

    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("LaserTrap").GetComponent<LaserTrapManager>();
    }
}
