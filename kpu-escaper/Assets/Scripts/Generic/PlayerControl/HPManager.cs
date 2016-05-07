using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// Hp 관리 스크립트, UI와 연동할 때 여기에서
public class HPManager : MonoBehaviour {
    public static HPManager instance;

    int HP = 100;					// 실제 Hp수치
    bool isCoolDown = false;		// 쿨타임

    public int _HP { get { return HP; } set { HP = value; } }
    public bool _isCoolDown { get { return isCoolDown; } set { isCoolDown = value; } }

    public Image[] Blood;			// 피격시에 표시되는 핏자국 스프라이트

    void Awake()
    {
		Debug.Log ("슙슙");
        if (instance == null)
            instance = this;
        foreach (Image blood in Blood)
        {
            blood.canvasRenderer.SetAlpha(0.0f);
			blood.GetComponent<Image>().color = new Color(1, 0, 0);
        }
    }

    void Damage(int DamageAmount)	// 실제 Hp를 감소시키는 루틴
    {
        HP -= DamageAmount;			// Hp 감소
        foreach (Image blood in Blood)
        {
            blood.canvasRenderer.SetAlpha( 1.0f );
            blood.CrossFadeAlpha( 0.0f, 1.0f, false );
        }
    }

	public void SetBloodColor(string _Color){
		foreach (Image blood in Blood)
		{
			if(_Color == "Red"){
				blood.GetComponent<Image>().color = new Color(1, 0, 0, 1);			
			}
			else if(_Color == "Green"){
				blood.GetComponent<Image>().color = new Color(0.09f, 1, 0.51f, 1);
			}
		}
	}

    public void GetDamage(int DamageAmount)
    {
        if(!isCoolDown)
        {
            StartCoroutine("CoolDown",(object)DamageAmount);
        }
    }

    IEnumerator CoolDown(object DamageAmountObj)
    {
        int DamageAmount = (int)DamageAmountObj;
        Damage(DamageAmount);
        Debug.Log(HP.ToString());
        isCoolDown = true;
        yield return new WaitForSeconds(1);
        isCoolDown = false;
    }

    public void ArcReactorTouch(ArcReactorHitInfo hit)
    {
        GetDamage(30);
    }
}
