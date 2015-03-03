using UnityEngine;
using System.Collections;

public class mapScript : MonoBehaviour {
    public GameObject node;
    public int dimention = 100;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < dimention; ++i)
        {
            for (int j = 0; j < dimention; ++j)
            {
                Instantiate(node, new Vector3((float)i*100/dimention, 5f, (float)j*100/dimention), Quaternion.identity);
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
