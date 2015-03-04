using UnityEngine;
using System.Collections;

namespace comp476a2
{
    public class NodeRecord
    {
        GameObject node;
        //GameObject connection;
		NodeRecord connection;
        int costSoFar;
        float estimatedTotalCost;
        const int heuristicWeight = 1;


        public NodeRecord(GameObject curNode, NodeRecord connectNode, int prevNodePathCost, GameObject destNode)
        {
            node = curNode;
            connection = connectNode;
            costSoFar = prevNodePathCost + 1;
            estimatedTotalCost = costSoFar + (destNode.transform.position - node.transform.position).magnitude * heuristicWeight;
        }

        public int getCostSoFar()
        {
            return costSoFar;
        }
        public float getEstimatedTotalCost()
        {
            return estimatedTotalCost;
        }
        public GameObject getGameObject()
        {
            return node;
        }
        public NodeRecord getConnection()
        {
            return connection;
        }

    }

}