using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


	public float verticalSpeed;
	public float horizontalSpeed;
	public float jumpPower;
	public Vector3 jumpDir;
	public Rigidbody rbLeft;
	public Rigidbody rbRight;
	public ForceMode fm;


	private Rigidbody rb;
	private Vector3 rotation;
	public int state = 0;


	void Start () {


		rb = GetComponent<Rigidbody> ();


	}


	void FixedUpdate () {


		state = CheckState ();


		switch (state) {
			case 0:
				LandMode ();
				break;
			case 1:
				WaterMode ();
				break;
			default:
				break;
		}


		Debug.DrawLine (transform.position, transform.position - Vector3.up, Color.red);


	}


	private int CheckState () {


		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.position - Vector3.up, out hit, 10f, 8)) {


			print (hit.collider.name);


			if (hit.collider.name == "floor") {
				return 0;
			} else {
				return 1;
			}
		}


		return 0;
	}


	private void LandMode () {
		float rotateBodyVertical = Input.GetAxis ("Vertical") * verticalSpeed;
		float rotateBodyHorizontal = Input.GetAxis ("Horizontal") * horizontalSpeed;


		if (Input.GetAxis ("Horizontal") >= 0.1f ||
			Input.GetAxis ("Horizontal") <= -0.1f ||
			Input.GetAxis ("Vertical") >= 0.1f ||
			Input.GetAxis ("Vertical") <= -0.1f) {


			Quaternion r = transform.localRotation;
			rb.AddRelativeTorque (new Vector3 (rotateBodyVertical, rotateBodyHorizontal, 0f), fm);
			if (rotateBodyHorizontal < 0) {
				rbLeft.AddRelativeTorque (new Vector3 (0f, rotateBodyHorizontal, 0f), fm);
			} else {
				rbRight.AddRelativeTorque (new Vector3 (0f, rotateBodyHorizontal, 0f), fm);
			}


		}


		if (Input.GetButtonDown ("Jump")) {


			rb.AddRelativeForce (jumpDir * jumpPower);


		}
	}


	private void WaterMode () {
		float moveBodyVertical = Input.GetAxis ("Vertical") * verticalSpeed;
		float rotateBodyHorizontal = Input.GetAxis ("Horizontal") * horizontalSpeed;


		if (Input.GetAxis ("Horizontal") >= 0.1f ||
			Input.GetAxis ("Horizontal") <= -0.1f ||
			Input.GetAxis ("Vertical") >= 0.1f ||
			Input.GetAxis ("Vertical") <= -0.1f) {


			Quaternion r = transform.localRotation;
			rb.AddRelativeForce (new Vector3 (0f, 0f, moveBodyVertical), fm);
			rb.AddRelativeTorque (new Vector3 (0f, rotateBodyHorizontal, 0f), fm);


		}


		if (Input.GetButtonDown ("Jump")) {


			rb.AddRelativeForce ((Vector3.forward + Vector3.up) * jumpPower);


		}
	}
}
