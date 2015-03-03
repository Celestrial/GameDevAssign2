using UnityEngine;
using System.Collections;
using System;

public class NodeInfo {
	GameObject node;
	GameObject connection;
	float costSoFar;
	float estimatedTotalCost;
	static float distanceWeight = 1;

	public NodeInfo(GameObject currentNode, GameObject endNode)
	{
		node = currentNode;
		connection = null;
		costSoFar = 0;
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
