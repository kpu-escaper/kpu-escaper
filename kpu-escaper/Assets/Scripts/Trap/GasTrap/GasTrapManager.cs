using UnityEngine;
using System.Collections;

public class GasTrapManager : RoomPropertyController
{
    public override void EnterRoom()
    {
		BlurController.instance.EnterGas ();
        StartCoroutine("GasTrapRoutine");
        Debug.Log("독가스 트랩 방에 들어왔습니다.");
    }

	public override void Update()
	{
    }

    public override void LeaveRoom()
    {
		BlurController.instance.ExitGas ();
        StopCoroutine("GasTrapRoutine");
		CameraShake.instance.shakePower = 0.25f;
		HPManager.instance.SetBloodColor("Red");
		Debug.Log("독가스 트랩 방에서 나갔습니다.");
    }

    IEnumerator GasTrapRoutine()
    {
		CameraShake.instance.shakePower = 0.05f;
        int Damage = 0;
        while (true)
        {
			HPManager.instance.GetDamage(Damage);
			HPManager.instance.SetBloodColor("Green");
           	Damage++;
            yield return new WaitForSeconds(1);
        }
    }
}
