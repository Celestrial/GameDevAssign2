using UnityEngine;
using System.Collections;

public class nodeScript : MonoBehaviour {
    public const int LINE_LENGTH = 2;
    Vector3 temp;
	GameObject[] neighbours = new GameObject[8];
	bool haveNeighbours = false;
	float timer = 0;
	public NodeInfo info;

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
			regNeighbours ();
			haveNeighbours = true;
		}
		else 
			timer += Time.deltaTime;

        

	}

	//GET ALL NEIGHBOURING NODES TO CURRENT NODE
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
            Destroy(gameObject);
    }

//	public void setInfo(GameObject endNode)
//	{
//		info = new NodeInfo(0, this.gameObject, endNode );
//	}

	public void setInfo(GameObject connection, int costSoFar, GameObject endNode)
	{
		info = new NodeInfo(connection, costSoFar, this.gameObject, endNode );
	}

	public GameObject[] getNeighbours()
	{
		return neighbours;
	}
}
