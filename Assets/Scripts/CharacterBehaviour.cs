using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


	public float verticalSpeedLand;
	public float horizontalSpeedLand;
	public float turnForceLand;
	public float maxVelocityLand;
	public float verticalSpeedWater;
	public float horizontalSpeedWater;
	public float maxVelocityWater;
	public float jumpPower;
	public Vector3 jumpDir;
	public Rigidbody rbLeft;
	public Rigidbody rbRight;
	public ForceMode fm;
	public int state = 0;


	private Rigidbody rb;
	private Vector3 rotation;
	private int chargeBodySlam = 0;
	private Vector3 boneZone;


	void Start () {


		rb = GetComponent<Rigidbody> ();
		boneZone = GameObject.Find ("BoneZone").transform.position;


	}


	void FixedUpdate () {


		state = CheckState ();


		switch (state) {
			case 0:
				LandMode ();
				rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocityLand);
				rbLeft.velocity = Vector3.ClampMagnitude (rbLeft.velocity, turnForceLand);
				rbRight.velocity = Vector3.ClampMagnitude (rbRight.velocity, turnForceLand);
				break;
			case 1:
				WaterMode ();
				rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocityWater);
				break;
			case 2:
				LandMode ();
				rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocityLand);
				rbLeft.velocity = Vector3.ClampMagnitude (rbLeft.velocity, turnForceLand);
				rbRight.velocity = Vector3.ClampMagnitude (rbRight.velocity, turnForceLand);
				break;
			default:
				break;
		}


		BodySlam ();


		rb.maxAngularVelocity = turnForceLand;
		rbLeft.maxAngularVelocity = turnForceLand;
		rbRight.maxAngularVelocity = turnForceLand;


		// print (rb.velocity.magnitude);


	}


	private int CheckState () {

		if (Vector3.Distance (transform.position, boneZone) <= 10f) {
			return 2;
		} else {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, -Vector3.up, out hit, Mathf.Infinity, 1 << 8)) {


				print (hit.collider.name);


				if (hit.collider.gameObject.name == "floor") {
					return 0;
				} else {
					return 1;
				}
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
			rb.AddRelativeForce ((transform.up + new Vector3 (rotateBodyHorizontal, 0f, 0f)) * turnForceLand);

		}


		if (Input.GetButtonDown ("Jump")) {


			rb.AddRelativeForce (jumpDir * jumpPower, ForceMode.Acceleration);


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
		if (Input.GetButton ("Fire1") && chargeBodySlam <= 30) {
			chargeBodySlam++;
		} else if (Input.GetButtonUp ("Fire1")) {
			rb.AddRelativeForce (((transform.forward + transform.up) * jumpPower) * chargeBodySlam);
			chargeBodySlam = 0;
		}
	}
}
