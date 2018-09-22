using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


	public float verticalSpeed;
	public float horizontalSpeed;
	public float jumpPower;
	public Rigidbody rbPivot;


	private Rigidbody rb;
	private Vector3 rotation;


	void Start () {


		rb = GetComponent<Rigidbody> ();


	}


	void FixedUpdate () {


		float rotateBodyVertical = -1 * Input.GetAxis ("Vertical") * verticalSpeed;
		float rotateBodyHorizontal = Input.GetAxis ("Horizontal") * horizontalSpeed;


		if (Input.GetAxis ("Horizontal") >= 0.1f ||
			Input.GetAxis ("Horizontal") <= -0.1f ||
			Input.GetAxis ("Vertical") >= 0.1f ||
			Input.GetAxis ("Vertical") <= -0.1f) {


			Quaternion r = transform.localRotation;
			rb.AddRelativeTorque (new Vector3 (0f, 0f, rotateBodyVertical));
			rbPivot.AddRelativeTorque (new Vector3 (0f, rotateBodyHorizontal, 0f));


		}


		if (Input.GetButtonDown ("Jump")) {


			rb.AddRelativeForce (new Vector3 (jumpPower, jumpPower, 0f));


		}


	}


}
