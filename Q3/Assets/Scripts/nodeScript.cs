using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nodeScript : MonoBehaviour {
    public const int LINE_LENGTH = 2;
	public string cluster;
    Vector3 temp;
	public GameObject[] neighbours = new GameObject[12];
    List<GameObject> nodesMasterList;
	bool haveNeighbours = false;
	float timer = 0;
	//public AStarAlgorithm info;

	// Use this for initialization
	void Start () {
        temp = transform.position + new Vector3(0f, -3.8f, 0f);
		//info = new NodeInfo(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//POSITION NODES, DESTROY NODES THAT INTERSECT WITH WALL, ONCE COMPLETE DESTROYS THE RIGIDBODY ON NODES
		//AS PER ASSIGNMENT REQUIREMENTS
        if (transform.position.y - 1.2f > 0.01f)
        {
            temp = transform.position;
            temp.y = 1.2f;
            transform.position = Vector3.Lerp(transform.position, temp, 0.1f);
        }
        else
        {
            Destroy(rigidbody);
        }

		//GET NODE NEIGHBOURS, COLOR BLACK AFTER 3 SEC
		if(timer > 1 && !haveNeighbours)
		{
            if (transform.parent != null && transform.parent.tag == "POVMap")
            {
                regPOVNeighbours();
				getCluster();
            }
            else
			{
                regNeighbours();
				getCluster();
			}
			haveNeighbours = true;
		}
		else 
			timer += Time.deltaTime;
	}
	void getCluster()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, -transform.up * 20, out hit);
		cluster = hit.transform.name;
	}
    void regPOVNeighbours()
    {
        nodesMasterList = new List<GameObject>();
        int count = transform.parent.childCount;
        for(int i = 0; i < count; ++i)
        {
            nodesMasterList.Add(transform.parent.GetChild(i).gameObject);
        }
        makePOVLinks();
    }

    void makePOVLinks()
    {
        int count = 0;
        foreach(GameObject node in nodesMasterList)
        {
            if(node.transform != transform)
            {
                Debug.Log(count);


                RaycastHit hit;
                Physics.Linecast(transform.position, node.transform.position, out hit);
                if(hit.transform == node.transform && count < neighbours.Length)
                {
                    
                    neighbours[count++] = node;
                }
            }
        }
    }

	//GET ALL NEIGHBOURING NODES TO CURRENT NODE IN NODE MAP
	void regNeighbours ()
	{
		RaycastHit hit;

		if(Physics.Linecast(transform.position, transform.position + transform.forward * LINE_LENGTH, out hit) && hit.collider.tag == "node")
		{
			neighbours[0] = hit.collider.gameObject;
			neighbours[0].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, transform.position + transform.forward * LINE_LENGTH);

		if(Physics.Linecast(transform.position, transform.position + transform.right * LINE_LENGTH, out hit) && hit.collider.tag == "node")
		{
			neighbours[1] = hit.collider.gameObject;
			neighbours[1].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, transform.position + transform.right * LINE_LENGTH);

		if(Physics.Linecast(transform.position, transform.position - transform.forward * LINE_LENGTH, out hit) && hit.collider.tag == "node")
		{
			neighbours[2] = hit.collider.gameObject;
			neighbours[2].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, transform.position - transform.forward * LINE_LENGTH);

		if(Physics.Linecast(transform.position, transform.position - transform.right * LINE_LENGTH, out hit) && hit.collider.tag == "node")
		{
			neighbours[3] = hit.collider.gameObject;
			neighbours[3].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, transform.position - transform.right * LINE_LENGTH);


		temp = transform.position + new Vector3 (1f, 0f, 1f) * LINE_LENGTH;
		if(Physics.Linecast(transform.position, temp, out hit) && hit.collider.tag == "node")
		{
			neighbours[4] = hit.collider.gameObject;
			neighbours[4].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, temp);

		temp = transform.position - new Vector3 (1f, 0f, 1f) * LINE_LENGTH;
		if(Physics.Linecast(transform.position, temp, out hit) && hit.collider.tag == "node")
		{
			neighbours[5] = hit.collider.gameObject;
			neighbours[5].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, temp);

		temp = transform.position + new Vector3 (1f, 0f, -1f) * LINE_LENGTH;
		if(Physics.Linecast(transform.position, temp, out hit) && hit.collider.tag == "node")
		{
			neighbours[6] = hit.collider.gameObject;
			neighbours[6].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, temp);

		temp = transform.position - new Vector3 (1f, 0f, -1f) * LINE_LENGTH;
		if(Physics.Linecast(transform.position, temp, out hit) && hit.collider.tag == "node")
		{
			neighbours[7] = hit.collider.gameObject;
			neighbours[7].renderer.material.color = Color.black;
		}
		Debug.DrawLine (transform.position, temp);
	}

    public GameObject[] getNeighbours()
    {
        return neighbours;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
            Destroy(gameObject);
    }

}
