using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapScript : MonoBehaviour {
    public GameObject node;
    public int dimention = 100;
	public List<GameObject> masterNodeList;
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

	public void nodeReset()
	{
		foreach(GameObject node in masterNodeList)
		{
            if(null != node)
			    node.renderer.material.color = Color.black;
		}
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
