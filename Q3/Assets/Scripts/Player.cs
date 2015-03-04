using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace comp476a2
{
    public class Player : MonoBehaviour
    {
        bool firstClick = true;
        bool onTheMove = false;
        GameObject startPos;
        GameObject endPos;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!onTheMove)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (firstClick && hit.collider.tag == "node")
                        {
                            transform.position = hit.transform.position;
                            transform.renderer.enabled = true;
                            hit.transform.renderer.material.color = Color.red;
                            startPos = hit.collider.gameObject;
                            firstClick = false;
                        }
                        else
                        {
                            hit.transform.renderer.material.color = Color.green;
                            endPos = hit.collider.gameObject;
                            NodeScript temp = startPos.gameObject.GetComponent<NodeScript>();
                            //temp.setInfo(null,0, endPos.gameObject);
                            onTheMove = true;
                            //pathfindAStar();
                            AStarAlgorithm pathFinder = new AStarAlgorithm(startPos, endPos);
                            pathFinder.findPath();
                        }
                    }
                }
            }
        }

        //List<Transform> pathfindAStar()
        //{


        //    //FlexHeap openList = new FlexHeap();
        //    //List<GameObject> closedList = new List<GameObject>();
        //    //openList.insert(startPos.GetComponent<nodeScript>().info.getTotalCost(), startPos.gameObject);

        //    //while (openList.count() > 0)
        //    //{
        //    //    Node temp = openList.remove();
        //    //    GameObject tempGO = temp.value;
        //    //    if(tempGO.transform == endPos)
        //    //        break;

        //    //    getConnections(ref tempGO, ref openList, ref closedList);
        //    //}
        //    return null;
        //}

        //void getConnections(ref GameObject curNode, ref FlexHeap openList, ref List<GameObject> closedList)
        //{
        //    List<GameObject> connectedNodes = new List<GameObject>();
        //    nodeScript curScript = curNode.GetComponent<nodeScript>();

        //    foreach(GameObject neighbour in curScript.getNeighbours())
        //    {
        //        if(closedList.Contains(neighbour))
        //        {

        //        }
        //        nodeScript temp = neighbour.GetComponent<nodeScript>();
        //        temp.setInfo(curNode, curScript.info.costSoFar, endPos.gameObject);
        //        openList.insert(temp.info.getTotalCost(), neighbour);
        //    }
        //}

        //void processNode(GameObject currentNode)
        //{

        //}


    }
}