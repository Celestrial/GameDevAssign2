using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	bool firstClick = true;
	bool onTheMove = false;
	Transform startPos;
	Transform endPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!onTheMove)
		{
			if (Input.GetButtonDown("Fire1")) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if(firstClick && hit.collider.tag == "node")
					{
						transform.position = hit.transform.position;
						transform.renderer.enabled = true;
						hit.transform.renderer.material.color = Color.red;
						startPos = hit.transform;
						firstClick = false;
					}else{
						hit.transform.renderer.material.color = Color.green;
						endPos = hit.transform;
						onTheMove = true;
					}
				}
			}
		}
	}
}
