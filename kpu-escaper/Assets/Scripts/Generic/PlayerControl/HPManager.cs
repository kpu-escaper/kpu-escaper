using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//HP 관리 스크립트. UI와 연동할때 여기서
public class HPManager : MonoBehaviour {
    public static HPManager instance;

    int HP = 100;               //실제 HP수치
    bool isCoolDown = false;    //쿨타임

    public int _HP { get { return HP; } set { HP = value; } }
    public bool _isCoolDown { get { return isCoolDown; } set { isCoolDown = value; } }
    public Image[] Blood;       //피격시에 표시되는 핏자국 스프라이트

    void Awake()
    {
        if (instance == null)
            instance = this;
        foreach (Image blood in Blood)
        {
            blood.canvasRenderer.SetAlpha(0.0f);
        }
    }

    void Damage(int DamageAmount)   // 실제 HP를 감소시키는 루틴
    {
        HP -= DamageAmount;         // HP감소
        foreach (Image blood in Blood)
        {
            blood.canvasRenderer.SetAlpha( 1.0f );      // 핏자국 스프라이트 알파값
            blood.CrossFadeAlpha( 0.0f, 1.0f, false );  // 0~1로 조정
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
