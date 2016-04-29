using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
    RoomPropertyController m_Controller;
	// Use this for initialization
	void Start () {
        m_Controller = FindObjectOfType<RoomPropertyController>().GetComponent<RoomPropertyController>();
        m_Controller.EnterRoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
