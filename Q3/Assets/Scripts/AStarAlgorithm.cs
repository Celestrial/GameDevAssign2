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

				if(currentNode.getGameObject() == endNode)
				{
					solutionPath = getSolutionPath(currentNode);
					break;
				}

				getNeighbours(currentNode);
				closedList.Add(currentNode);


                //DEBUG CASE
                if (openList.count() > 1000000)
                    break;
            }

            return null;
        }

		ArrayList getSolutionPath(NodeRecord currentNode)
		{
			ArrayList temp = new ArrayList();
			GameObject currentObject = currentNode.getGameObject();
			temp.Add(currentObject);
			currentObject.renderer.material.color = Color.yellow;
			while (currentObject != startNode.gameObject)
			{
				currentNode = currentNode.getConnection();
				currentObject = currentNode.getGameObject();
				temp.Add(currentObject);
				currentObject.renderer.material.color = Color.yellow;
			}

			temp.Reverse();
			endNode.renderer.material.color = Color.green;
			return temp;
		}

        void getNeighbours(NodeRecord currentNode)
        {
            GameObject[] neighbours = currentNode.getGameObject().GetComponent<NodeScript>().getNeighbours();

			//GO THROUGH THE NEIGHBOURS AND EITHER ADD OPEN LIST, UPDATE OPEN LIST, REMOVE FROM CLOSE LIST OR SKIP
            foreach(GameObject neighbour in neighbours)
            {
                if (neighbour != null)
                {
					//GET NEIGHBOUR NODE, CHANGE TO GREY ONCE CONSIDERED
                    NodeRecord currentNeighbour = new NodeRecord(neighbour, currentNode, currentNode.getCostSoFar(), endNode);
					neighbour.renderer.material.color = Color.grey;

					//CHECK IF NODE IN CLOSED LIST, IF NEEDED REMOVE FROM CLOSED LIST
					NodeRecord nodeInClosed = closedListContains(currentNeighbour.getGameObject());
					NodeRecord nodeInOpen = openList.contains(currentNeighbour.getGameObject());
                    if(nodeInClosed != null)
                    {
                        if(currentNeighbour.getCostSoFar() < nodeInClosed.getCostSoFar())
						{
							closedList.Remove(nodeInClosed);
//************************************************NOT SURE IF WE SHOULD ADD BACK TO OPEN LIST*******************************************************
//							openList.insert(currentNeighbour.getEstimatedTotalCost(), currentNeighbour);
						}else{
							continue;//SKIP THIS NODE AND DO NOT ADD TO OPEN LIST SINCE PATH IS NOT BETTER
						}
                    }
					else if(nodeInOpen != null)
					{
						if(currentNeighbour.getCostSoFar() < nodeInOpen.getCostSoFar())
						{
							//PROBABLY WILL NOT WORK SINCE THIS IS NOT A REFERENCE, NEED TO CHECK HOW C# HANDLES OBJECTS
							nodeInOpen = currentNeighbour;
						}else{
							continue;
						}
					}
                    openList.insert(currentNeighbour.getEstimatedTotalCost(), currentNeighbour);
                }
            }
			
        }

        NodeRecord closedListContains(GameObject gameObject)
        {
            foreach(NodeRecord currentRecord  in closedList)
            {
                if (currentRecord.getGameObject() == gameObject)
                    return currentRecord;
            }
            return null;
        }
    }
}
