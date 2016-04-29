using UnityEngine;
using System.Collections;

//플레이어 레이이벤트
public class RayEvent : MonoBehaviour {
    
    public delegate void OnClickRayEvent(GameObject obj);

    public static event OnClickRayEvent OnLeftClick;
    public static event OnClickRayEvent OnRightClick;
    public static event OnClickRayEvent OnHover;
    public static event OnClickRayEvent OnHoverEnd;

    GameObject CurrentObject = null;
    // Handle our Ray and Hit
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(OnLeftClick!=null)
                    OnLeftClick(hit.transform.gameObject);  //좌클릭했을때
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (OnRightClick != null)
                    OnRightClick(hit.transform.gameObject); //우클릭했을때
            }

            if(hit.transform.gameObject!=null)
            {
                if (CurrentObject == null)
                {
                    CurrentObject = hit.transform.gameObject;
                    if (OnHover != null)
                        OnHover(hit.transform.gameObject);  // 하이라이트되고있을때
                }
                else if (hit.transform.gameObject != CurrentObject)
                {
                    if(OnHoverEnd!=null)
                        OnHoverEnd(CurrentObject);          // 하이라이트 꺼졌을때
                    CurrentObject = hit.transform.gameObject;
                    if (OnHover != null)
                        OnHover(hit.transform.gameObject);
                }
            }
            else
            {
                if (CurrentObject != null)
                {
                    if (OnHoverEnd != null)
                        OnHoverEnd(CurrentObject);
                    CurrentObject = null;
                }
            }
        }
    }
}
