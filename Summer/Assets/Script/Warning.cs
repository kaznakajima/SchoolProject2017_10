using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        transform.parent = Camera.main.transform;
        Destroy(gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
