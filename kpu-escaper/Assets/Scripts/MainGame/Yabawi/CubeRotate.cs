using UnityEngine;
using System.Collections;

public class CubeRotate : MonoBehaviour
{
    public int CurrentRotateIndex = 0;

    Vector3 Axis;
    Vector3 Point;
    public void Rotate(Vector3 _Point, Vector3 _Axis)
    {
        Point = _Point;
        Axis = _Axis;

        StartCoroutine("RotateCoroutine");
    }

    IEnumerator RotateCoroutine(){
		yield return new WaitForSeconds(0.3f);
		float fAngle = 0;
        while(fAngle<360){
				float frameAngle = Time.deltaTime * 150;
				fAngle += frameAngle;
				transform.RotateAround(Point, Axis, frameAngle);
				yield return null;
		}

		GameObject.Find ("YabaPrefab").GetComponent<YabawiManager> ().SetBool ();
	}
}
