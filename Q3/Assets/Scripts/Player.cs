using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
						nodeScript temp = startPos.gameObject.GetComponent<nodeScript>();
						temp.setInfo(endPos.gameObject);
						onTheMove = true;
					}
				}
			}
		}
	}

	List<Transform> pathfindAStar()
	{
		SortedList openList = new SortedList();
		List<GameObject> closedList = new List<GameObject>();
		openList.Add(startPos.GetComponent<nodeScript>().info.getTotalCost(), startPos.gameObject);

		while(openList.Count() > 0)
		{
			GameObject temp = openList.RemoveAt(0);
		}

		return null;

	}

	void processNode(GameObject currentNode)
	{

	}


}
