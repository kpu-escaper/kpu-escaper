using UnityEngine;
using System.Collections;

public class MedievalTrapCollision : MonoBehaviour {
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            HPManager.instance.GetDamage(5);
        }
    }
}
