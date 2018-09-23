using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDamp : MonoBehaviour {

    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.05f);
        Vector3 angles = Vector3.Slerp(transform.eulerAngles, target.transform.eulerAngles, 0.05f);
        angles.z = 0;
        transform.eulerAngles = angles;

    }
}
