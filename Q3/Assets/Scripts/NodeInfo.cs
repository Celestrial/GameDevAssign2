using UnityEngine;
using System.Collections;
using System;

public class NodeInfo {
	GameObject node;
	public GameObject connection;
	public float costSoFar;
	float estimatedTotalCost;
	static float distanceWeight = 1;

	public NodeInfo(GameObject connecting, int costSoFar, GameObject currentNode, GameObject endNode)
	{
		node = currentNode;
		connection = connecting;
		this.costSoFar = costSoFar + 1;
		setTotalCost(currentNode.transform, endNode.transform);
	}

	void setTotalCost(Transform currentNode , Transform endNode)
	{
		estimatedTotalCost = costSoFar + (currentNode.position - endNode.position).magnitude * distanceWeight;
	}

	public float getTotalCost()
	{
		return estimatedTotalCost;
	}
}
