using UnityEngine;
using System.Collections;

public class RemoveEffect : MonoBehaviour {
    
    // 타이머 실수 변수 선언
    public float timerForDel;
    // 타이머 조건 실수 변수 선언
    public float timerForDelLim;

	// Update is called once per frame
	void Update () {
        // 타이머 값 계산
        timerForDel += Time.deltaTime;
        // 타이머 발동
        if (timerForDel > timerForDelLim)
        {
            Destroy(gameObject); // 오브젝트 삭제
        }	
	}
}
