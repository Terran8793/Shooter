using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity {

	public float moveSpeed = 5;
	public float runSpeed = 8;
	public float jumpForce = 220;

	public LayerMask groundedMask;

	public bool canMultipleJump;

	private int jumpCounter = 0;
	public int maxJumps = 0;

	public Crosshairs crosshairs;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;

	bool grounded;

	protected override void Start () {
		base.Start ();
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController> ();
		viewCamera = Camera.main;
		jumpCounter = 1;
	}

	void Update ()
	{
		// Movement Input
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 MoveVelocity = moveInput.normalized * moveSpeed;
		controller.Move (MoveVelocity);

		// Run Input
		if (Input.GetKey (KeyCode.LeftShift)){ 
			if (grounded) {
				moveSpeed = runSpeed;
		}
	}
		// Jump Input
		if (Input.GetButtonDown ("Jump")) {
			if (grounded) {
				GetComponent<Rigidbody> ().AddForce (transform.up * jumpForce);
			}
		}
		grounded = false;
		Ray _ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast (_ray, out hit, 1 + .1f, groundedMask)) {
			grounded = true;
		}

		if (Input.GetButtonDown ("Jump")) {
			if (jumpCounter < maxJumps) {
				GetComponent<Rigidbody>().AddForce (transform.up * jumpForce);
				jumpCounter++;
			}
		}

		if (grounded) {
			jumpCounter = 1;
		}

		// Look Input
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPLane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPLane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			//Debug.DrawLine(ray.origin,point,Color.black);
			controller.LookAt (point);
			crosshairs.transform.position = point;
			crosshairs.DetectTargets (ray);
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

		if (transform.position.y < -10) 
		{
			TakeDamage (health);
		}
	}
}
