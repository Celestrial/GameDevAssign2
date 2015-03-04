using UnityEngine;
using System.Collections;
using System;

namespace comp476a2
{
    public class AStarAlgorithm
    {
        #region Constants
        const float heuristicWeight = 1;

        #endregion

        #region Member Variables
        GameObject startNode;
        GameObject endNode;
        ArrayList solutionPath;
        ArrayList closedList;
        FlexHeap openList; 
        #endregion

        public AStarAlgorithm(GameObject start, GameObject end)
        {
            startNode = start;
            endNode = end;
            solutionPath = new ArrayList();
            closedList = new ArrayList();
            openList = new FlexHeap();
        }

        public ArrayList findPath()
        {
            NodeRecord start = new NodeRecord(startNode, null, -1, endNode);
            openList.insert(start.getEstimatedTotalCost(), start);

            while(openList.count() > 0)
            {
                NodeRecord currentNode = openList.remove().value;
                closedList.Add(currentNode);
                ArrayList neighbours = getNeighbours(currentNode);

                //DEBUG CASE
                if (openList.count() > 1000000)
                    break;
            }

            return null;
        }

        ArrayList getNeighbours(NodeRecord currentNode)
        {
            GameObject[] neighbours = currentNode.getGameObject().GetComponent<NodeScript>().getNeighbours();
            foreach(GameObject neighbour in neighbours)
            {
                if (neighbour != null)
                {
                    NodeRecord currentNeighbour = new NodeRecord(neighbour, currentNode, currentNode.getCostSoFar(), endNode);
					neighbour.renderer.material.color = Color.grey;
                    openList.insert(currentNeighbour.getEstimatedTotalCost(), currentNeighbour);
                }
            }


            return null;
        }
    }
}
