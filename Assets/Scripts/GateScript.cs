using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public float upPosHeight;
    public float downPosHeight;
    public GameObject gateObject;
    private Vector3 gatePos;
    private float timer = 0.0f;

    public bool isUp = false;
	// Use this for initialization
	void Start () {
        Vector3 pos = gateObject.transform.localPosition;
        gatePos = pos;
        pos.y = downPosHeight;
        gateObject.transform.localPosition = pos;

        OpenGate();
	}

    public void OpenGate()
    {
        isUp = true;
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            timer += Time.fixedDeltaTime;
            Vector3 up = gatePos;
            Vector3 down = gatePos;
            up.y = upPosHeight;
            down.y = downPosHeight;
            gateObject.transform.localPosition = Vector3.Lerp(down, up, timer);
        }
    }
}
