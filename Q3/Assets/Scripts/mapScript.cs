using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapScript : MonoBehaviour {
    public GameObject node;
    public int dimention = 100;
	public List<GameObject> masterNodeList;
	bool nodeMap = true;
	// Use this for initialization
	void Start () {
		masterNodeList = new List<GameObject>();
        for (int i = 0; i < dimention; ++i)
        {
            for (int j = 0; j < dimention; ++j)
            {
               masterNodeList.Add((GameObject)Instantiate(node, new Vector3((float)i*100/dimention, 5f, (float)j*100/dimention), Quaternion.identity));
            }
        }
	}

	public void nodeColorReset()
	{
		foreach(GameObject node in masterNodeList)
		{
            if(null != node)
			    node.renderer.material.color = Color.black;
		}
	}

	public void switchMapType()
	{
		if(nodeMap)
		{
			nodeMap = !nodeMap;
			destroyNodeMap();
			generatePOVMap();
		}else{
			nodeMap = !nodeMap;
			destroyPOVMap();
			generateNodeMap();
		}
	}
	public void generatePOVMap()
	{

	}
	public void generateNodeMap()
	{
		masterNodeList = new List<GameObject>();
		for (int i = 0; i < dimention; ++i)
		{
			for (int j = 0; j < dimention; ++j)
			{
				masterNodeList.Add((GameObject)Instantiate(node, new Vector3((float)i*100/dimention, 5f, (float)j*100/dimention), Quaternion.identity));
			}
		}
	}
	public void destroyPOVMap()
	{

	}
	public void destroyNodeMap()
	{
		foreach(GameObject node in masterNodeList)
		{
			Destroy (node);
		}
	}
    // Update is called once per frame
    void Update()
    {
	
	}
}
