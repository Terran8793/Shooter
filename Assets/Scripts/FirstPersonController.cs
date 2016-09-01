using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GunController))]
public class FirstPersonController : LivingEntity {

	public float mouseSensitivityX = 250f;
	public float mouseSensitivityY = 250f;
	public float walkSpeed = 8f;
	public float runSpeed = 16f;
	public float jumpForce = 220;
	public LayerMask groundedMask;

	Transform cameraT;
	float verticalLookRotation;

	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;

	PlayerController controller;
	GunController gunController;

	bool grounded;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController> ();
		cameraT = Camera.main.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * Time.deltaTime * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 90);
		cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

		if (Input.GetButtonDown("Jump")) 
		{
			if (grounded) 
			{
		GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
		}
	}

	grounded = false;
	Ray ray = new Ray (transform.position, -transform.up);
	RaycastHit hit;

	if (Physics.Raycast (ray, out hit, 1 + .1f, groundedMask))
	{
		grounded = true;
	}
	
		// Weapon Input
	if (Input.GetMouseButton(0))
		{
			gunController.OnTriggerHold();
		}

		if (Input.GetMouseButtonUp(0))
		{
			gunController.OnTriggerRelease();
		}

}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}
}
