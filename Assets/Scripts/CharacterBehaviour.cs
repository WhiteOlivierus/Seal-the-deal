using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


	public float verticalSpeedLand;
	public float horizontalSpeedLand;
	public float verticalSpeedWater;
	public float horizontalSpeedWater;
	public float jumpPower;
	public float landSideForce;
	public Vector3 jumpDir;
	public Rigidbody rbLeft;
	public Rigidbody rbRight;
	public ForceMode fm;


	private Rigidbody rb;
	private Vector3 rotation;
	private int state = 0;


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


		BodySlam ();


		Debug.DrawLine (transform.position, transform.position - Vector3.up, Color.red);


	}


	private int CheckState () {


		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, Mathf.Infinity, 1 << 8)) {


			print (hit.collider.name);


			if (hit.collider.gameObject.name == "floor") {
				return 0;
			} else {
				return 1;
			}
		}


		return 0;
	}


	private void LandMode () {
		float rotateBodyVertical = Input.GetAxis ("Vertical") * verticalSpeedLand;
		float rotateBodyHorizontal = Input.GetAxis ("Horizontal") * horizontalSpeedLand;


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
			rb.AddRelativeForce ((transform.up + new Vector3 (rotateBodyHorizontal, 0f, 0f)) * landSideForce);
		}


		if (Input.GetButtonDown ("Jump")) {


			rb.AddRelativeForce (jumpDir * jumpPower);


		}
	}


	private void WaterMode () {
		float moveBodyVertical = Input.GetAxis ("Vertical") * verticalSpeedWater;
		float rotateBodyHorizontal = Input.GetAxis ("Horizontal") * horizontalSpeedWater;


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


	private void BodySlam () {

	}
}
