using UnityEngine;
using System.Collections;

public class FollowPathPlayerCollision : MonoBehaviour {
    MainGameFollowPathManager GameManager;
    void Awake()
    {
        GameManager = GameObject.Find("FollowPathPrefab").GetComponent<MainGameFollowPathManager>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Judge(gameObject);
        }
    }
}
