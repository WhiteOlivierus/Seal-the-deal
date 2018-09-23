using UnityEngine;

public class NewCharacterBehaviour : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	public int state = 0;
	public float waveHeight, waveSpeed;
	public Rigidbody rb;


	private Vector3 boneZone;
	private float xPingPong = 0;


	void Start () {


		// rb = GetComponent<Rigidbody> ();
		boneZone = GameObject.Find ("BoneZone").transform.position;


	}


	void FixedUpdate () {
		xPingPong = Mathf.Sin (Time.time * waveSpeed) * waveHeight;


		state = CheckState ();
        GetInput();
        FollowRB();

        switch (state) {
			case 0:
				LandMode ();
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				break;
        }

    }

	private int CheckState () {


		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.position - Vector3.up, out hit, Mathf.Infinity, 1 << 8)) {
			if (hit.collider.gameObject.name == "floor") {
				if (Vector3.Distance (transform.position, boneZone) <= 10f) {
					return 2;
				} else {
					return 0;
				}
			} else {
				return 1;
			}
		}


		return 0;
	}


	private void LandMode () {


		//if (Input.GetAxis ("Horizontal") < 0) {
		//	transform.Rotate (-Vector3.up * rotateSpeed);
		//} else if (Input.GetAxis ("Horizontal") > 0) {
		//	transform.Rotate (Vector3.up * rotateSpeed);
		//} else if (Input.GetAxis ("Vertical") < 0) {
		//	transform.Translate (new Vector3 (0f, xPingPong, -1f) * moveSpeed);
		//	//print (new Vector3 (0f, xPingPong, -1f) * moveSpeed);
		//} else if (Input.GetAxis ("Vertical") > 0) {
		//	transform.Translate (new Vector3 (0f, xPingPong, 1f) * moveSpeed);
		//	//print (new Vector3 (0f, xPingPong, 1f) * moveSpeed);
		//}
  //      if (Input.GetMouseButtonDown(0))
  //      {
  //          transform.Translate(new Vector3(0, 0, 3f));
  //      }
        if (left)
        {
            transform.Rotate(-Vector3.up * rotateSpeed);
        }
        if (right)
        {
            transform.Rotate(Vector3.up * rotateSpeed);
        }
        if (forward)
        {
            transform.Translate(new Vector3(0f, xPingPong, -1f) * moveSpeed);
            //print (new Vector3 (0f, xPingPong, -1f) * moveSpeed);
        }
        else if (back)
        {
            transform.Translate(new Vector3(0f, xPingPong, 1f) * moveSpeed);
            //print (new Vector3 (0f, xPingPong, 1f) * moveSpeed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            transform.Translate(new Vector3(0, 0, 3f));
        }

        rb.position = transform.position;
        //rb.MovePosition (transform.position);
		rb.MoveRotation (new Quaternion (0f, transform.rotation.y, transform.rotation.z, transform.rotation.w));
	}

    private void FollowRB()
    {
        Vector3 pos = Vector3.Lerp(transform.position, rb.position+new Vector3(0,2.5f,0), 0.1f);
        transform.position = pos;
    }

    private bool forward = false;
    private bool back = false;
    private bool left = false;
    private bool right = false;
    private void GetInput()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            left = true;
            right = false;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            right = true;
            left = false;
        }
        else if(Input.GetAxis("Horizontal") == 0)
        {
            right = false;
            left = false;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            forward = true;
            back = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            back = true;
            forward = false;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            back = false;
            forward = false;
        }
    }
}
