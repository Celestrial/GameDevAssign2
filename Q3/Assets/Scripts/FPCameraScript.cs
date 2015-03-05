using UnityEngine;
using System.Collections;

public class FPCameraScript : MonoBehaviour {
    public GameObject player;
    public Vector3 offset = new Vector3(0f,1f,1.5f);
    public Vector3 lookOffset = new Vector3(0f, 3f, 0f);
    
	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.LookRotation((player.transform.position + lookOffset) - transform.position);
	}
}
