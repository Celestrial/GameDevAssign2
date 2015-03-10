using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace comp476a2
{
    public class Player : MonoBehaviour
    {
        Vector3 velocity = Vector3.zero;
        public float desiredSlowVelocity = .1f;
        public int maxVelocity = 4;
        public int maxAcceleration = 30;
        bool firstClick = true;
		bool playerSpawned = false;
        bool onTheMove = false;
		float sphereCastRadius = 5f;
        GameObject startPos;
        GameObject endPos;
        AStarAlgorithm pathFinder;
		Vector3[] solutionPath;
        int targetNode = 0;
        float movementSpeed = 20f;
		public float satisfactionRadius = 0.25f;
        public float slowDownRadius = 1f;
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
                            //transform.renderer.enabled = true;
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
							//hit.transform.renderer.material.color = Color.green;
							transform.position = closestNode.transform.position;
							//transform.renderer.enabled = true;
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

                        //transform.position += getMovement(solutionPath[targetNode]) * Time.deltaTime * 5;
                        velocity += getAcceleration(solutionPath[targetNode]) * Time.deltaTime;
                        velocity += getDecelleration(solutionPath[targetNode]) * Time.deltaTime;
                        transform.position += Vector3.ClampMagnitude(velocity, maxVelocity)*Time.deltaTime;
                        Debug.Log("Distance from target = " + (solutionPath[targetNode] - transform.position).magnitude );
                        if ((transform.position - solutionPath[targetNode]).magnitude <= satisfactionRadius)
                        {

                            ++targetNode;
                        }
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

        private Vector3 getVelocity(Vector3 target)
        {
            velocity += getAcceleration(target);
            return Vector3.ClampMagnitude(velocity, maxVelocity);
        }
        private Vector3 getDecelleration(Vector3 target)
        {
            Vector3 temp = -Vector3.ClampMagnitude((velocity + getAcceleration(target)), maxVelocity);
            Vector3 newVelocity = temp * (1 / Mathf.Pow((transform.position - target).magnitude, 2));
            if (newVelocity.magnitude > desiredSlowVelocity)
                return newVelocity;
            else
                return temp.normalized * desiredSlowVelocity;
        }
        private Vector3 getAcceleration(Vector3 target)
        {
            return Vector3.ClampMagnitude((target - transform.position), maxAcceleration);
        }
		private Vector3 getMovement(Vector3 target)
		{
			Vector3 temp = (target - transform.position);
			Vector3.ClampMagnitude(temp, movementSpeed);
			return temp;
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