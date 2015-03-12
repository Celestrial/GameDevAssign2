using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace comp476a2
{
	public class mapScript : MonoBehaviour {
	    public GameObject node;
	    public GameObject POVNodesOriginal;
		GameObject POVNodesClone;
	    public int dimention = 100;
	    public Vector3 POVMapStartPos = new Vector3(46.604f, 38.703f, 33.637f);
		public List<GameObject> masterNodeList;
		public bool nodeMap = true;
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
				{
					Color temp = new Color(0,0,0,.25f);
				    node.renderer.material.color = temp;
				}
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
			masterNodeList = new List<GameObject>();
	        POVNodesClone = (GameObject)Instantiate(POVNodesOriginal, POVMapStartPos, Quaternion.identity);
			for(int i = 0; i < POVNodesClone.transform.childCount; ++i)
			{
				masterNodeList.Add(POVNodesClone.transform.GetChild(i).gameObject);
			}
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
	        Destroy(POVNodesClone);
		}
		public void destroyNodeMap()
		{
			foreach(GameObject node in masterNodeList)
			{
				Destroy (node);
			}
			masterNodeList = null;
		}
	    // Update is called once per frame
	    void Update()
	    {
			if(Input.GetKey(KeyCode.Space))
			{
				generateClusterTable();
			}
		}
		void generateClusterTable()
		{
			//StreamWriter sw = new StreamWriter("clusterTable.txt");
	//		{
	//			sw.WriteLine("blah");
	//			sw.Close();
	//		}
			float distance;
			float[,] table = new float[9,9];
			NodeRecord.heuristicWeight = 0;
			foreach(GameObject node1 in masterNodeList)
			{
				foreach(GameObject node2 in masterNodeList)
				{
					if(node1.GetComponent<NodeScript>().cluster != node2.GetComponent<NodeScript>().cluster
					   && isNeighbour(node1, node2))
					{
						distance = (node1.transform.position - node2.transform.position).magnitude;
						if(table[node1.GetComponent<NodeScript>().cluster, node2.GetComponent<NodeScript>().cluster] == 0 
						   || table[node1.GetComponent<NodeScript>().cluster, node2.GetComponent<NodeScript>().cluster] > distance)
						{
							table[node1.GetComponent<NodeScript>().cluster, node2.GetComponent<NodeScript>().cluster] = distance;
						}
					}
				}
			}
		}

		bool isNeighbour(GameObject node1, GameObject node2)
		{
			NodeScript script = node1.GetComponent<NodeScript>();
			foreach(GameObject neighbour in script.neighbours)
			{
				if(neighbour != null && neighbour == node2)
					return true;
			}
			return false;
		}
	}
}
