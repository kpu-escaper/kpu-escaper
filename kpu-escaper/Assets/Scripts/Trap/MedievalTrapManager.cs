using UnityEngine;
using System.Collections;

public class MedievalTrapManager : RoomPropertyController
{
    GameObject[] Trap;
	Vector3 thornPos;
    public override void EnterRoom()
    {
        Trap = GameObject.FindGameObjectsWithTag("MedievalTrap");
        StartCoroutine("MedievalTrapRoutine");
        Debug.Log("가시 트랩 방에 들어왔습니다.");
    }

    public override void Update()
    {

    }

    public override void LeaveRoom()
    {
        StopCoroutine("MedievalTrapRoutine");
        Debug.Log("가시 트랩 방에서 나갔습니다.");
    }

    IEnumerator MedievalTrapRoutine()
    {
        while(true)
        {
            for (int i = 0; i < 10; ++i)
            {
                int randomInt = Random.Range(0, Trap.Length);
				thornPos = new Vector3(Random.Range (-1106, 890),0,Random.Range (-980,1207));
				thornPos = thornPos/1000;
				Trap[randomInt].transform.localPosition = thornPos;
                Trap[randomInt].GetComponent<Animation>()["Up Down"].speed = 1;
                Trap[randomInt].GetComponent<Animation>().Play();
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}
