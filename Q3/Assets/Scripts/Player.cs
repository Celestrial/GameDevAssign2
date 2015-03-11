using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace comp476a2
{
    public class Player : MonoBehaviour
    {
        public static float maximumSeekVelocity = 15f, maximumRotationVelocity = 2f,
        maximumFleeVelocity = 10f, maximumAcceleration = 0.05f, maxinumRotationAcceleration = 0.01f;
        float currentVelocity = 0, currentRotationVelocity = 0, currentAcceleration = 0.05f;

        Vector3 directionVector = Vector3.zero;
        public float desiredSlowVelocity = .1f;
        public int maxVelocity = 4;
        public int maxAcceleration = 30;
        bool firstClick = true;
        bool onTheMove = false;
		float sphereCastRadius = 5f;
        GameObject startPos;
        GameObject endPos;
        AStarAlgorithm pathFinder;
		Vector3[] solutionPath;
        int targetNode = 0;
		public float satisfactionRadius = 2f;
        public float slowDownRadius = 2f;
		public GameObject walls;
		mapScript wallScript;
		public Movement movementScript;
        // Use this for initialization
        void Start()
        {
			movementScript = GetComponent<Movement>();
			wallScript = walls.GetComponent<mapScript>();
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

                            wallScript.nodeColorReset();
                            transform.position = hit.transform.position;
                            hit.transform.renderer.material.color = Color.red;
                            startPos = hit.collider.gameObject;
                            firstClick = false;
                        }
						else if(firstClick && hit.collider.tag == "Cluster")
						{

                            wallScript.nodeColorReset();
							GameObject closestNode = null;
							float closest = 1000f;
							foreach(Collider node in Physics.OverlapSphere(hit.point, sphereCastRadius))
							{
								if((hit.point - node.transform.position).magnitude < closest)
								{
									closest = (hit.point - node.transform.position).magnitude;
									closestNode = node.gameObject;
								}
							}
							transform.position = closestNode.transform.position;
							closestNode.transform.renderer.material.color = Color.red;
							startPos = closestNode.collider.gameObject;
							firstClick = false;
						}
						else if(!firstClick && hit.collider.tag == "node")
                        {
                            hit.transform.renderer.material.color = Color.green;
                            endPos = hit.collider.gameObject;
                            NodeScript temp = startPos.gameObject.GetComponent<NodeScript>();
                            onTheMove = true;
                            pathFinder = new AStarAlgorithm(startPos, endPos);
                            pathFinder.findPath();
                            onTheMove = true;
							firstClick = true;
                        }
						else if(!firstClick && hit.collider.tag == "Cluster")
						{
							GameObject closestNode = null;
							float closest = 1000f;
							foreach(Collider node in Physics.OverlapSphere(hit.point, sphereCastRadius))
							{
								if(node.transform.tag == "node" && (hit.point - node.transform.position).magnitude < closest)
								{
									closest = (hit.point - node.transform.position).magnitude;
									closestNode = node.gameObject;
								}
							}
							closestNode.transform.renderer.material.color = Color.green;
							endPos = closestNode;
							NodeScript temp = startPos.gameObject.GetComponent<NodeScript>();
							onTheMove = true;
							pathFinder = new AStarAlgorithm(startPos, endPos);
							pathFinder.findPath();
							onTheMove = true;
							firstClick = true;
						}else{}
                    }
                }

                if(Input.GetKeyDown(KeyCode.D))
                {
                    if (NodeRecord.heuristicWeight == 1)
                        NodeRecord.heuristicWeight = 0;
                    else
                        NodeRecord.heuristicWeight = 1;
                }
                if(Input.GetKeyDown(KeyCode.P))
                {
                    wallScript.switchMapType();
                }
            }
        }

		void FixedUpdate()
		{
			if(onTheMove)
			{
				if(null == solutionPath)
					converSolutionPath();
				else
				{
                    if (targetNode != solutionPath.Length)
                    {
                        //Find the direction vector based on the target's position
                        directionVector = (solutionPath[targetNode] - transform.position);
                        directionVector.Normalize();
                        //Find the current rotation velocity
                        currentRotationVelocity = Mathf.Min(currentRotationVelocity + maxinumRotationAcceleration, maximumRotationVelocity);
                        //Create a goal velocity that is proportional to the distance to the target (interpolated from 0 to max)
                        float goalVelocity = maximumSeekVelocity * ((solutionPath[targetNode] - transform.position).magnitude / 15f);
                        currentVelocity = Mathf.Min(currentVelocity + currentAcceleration, maximumFleeVelocity);
                        //Calculate the current acceleration based on the goal velocity and the current velocity
                        currentAcceleration = Mathf.Min((goalVelocity - currentVelocity) / 2, maximumAcceleration);
                        //Interpolate the orientation of the NPC object
                        Quaternion targetRotation = Quaternion.LookRotation(directionVector);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, currentRotationVelocity * Time.deltaTime);
                        //Update the position
                        Vector3 newPosition = transform.position + (currentVelocity * Time.deltaTime) * transform.forward.normalized;
                        transform.position = newPosition;


                        if (targetNode != solutionPath.Length-1 && (transform.position - solutionPath[targetNode]).magnitude <= satisfactionRadius)
                        {

                            ++targetNode;
                        }
                        else if ((transform.position - solutionPath[targetNode]).magnitude <= satisfactionRadius * .125f)
                        {
                            Debug.Log("In last node!!!");
                            ++targetNode;
                        }
                        Debug.Log(solutionPath.Length - targetNode + " nodes from the end");
                    }
                    else
                    {
                        targetNode = 0;
                        onTheMove = false;
                        //firstClick = true;
                        pathFinder = null;
                        solutionPath = null;
                        startPos = endPos;
                        startPos.renderer.material.color = Color.red;
                        endPos = null;
                    }
				}
			}
		}

         private void converSolutionPath()
        {
            solutionPath = new Vector3[pathFinder.getSolutionPath().Count];
            for (int i = 0; i < solutionPath.Length; ++i)
            {
                GameObject temp = (GameObject)pathFinder.getSolutionPath()[i];
                solutionPath[i] = temp.transform.position;
            }
        }
    }
}