using UnityEngine;
using System.Collections;

public class EnemyAI1 : MonoBehaviour {

	public Transform player;
	public float playerDistance;
	public float rotationDamping;
	public float moveSpeed;
	public static bool isPlayerAlive = true;

	//Color originalColor;
	//Renderer rend;

	public float attackDistanceThreshold = 1.5f;
	public float timeBeteenAttacks = 1;
	public float damage = 1;

	//float nextAttackTime;
	//float myCollisionRadius;
	//float targetCollisionRadius;

	//bool hasTarget;


	// Use this for initialization
	void Start () {
	//rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

		if (isPlayerAlive) 
		{
			playerDistance = Vector3.Distance (player.position, transform.position);

			if (playerDistance < 15f) {
				lookAtPlayer ();
			}
			if (playerDistance < 12f) {
				if (playerDistance > 2f) {
					chase ();
				} else if (playerDistance < 2f) {
					//rend.material.shader = Shader.Find ("Specular");
					//rend.material.SetColor("_SpecColor", Color.blue);
					attack ();
				}
			}
	
		}
	}

	void lookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}

	void chase()
	{
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}

	void attack()
	{
		
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) 
		{
			if (hit.collider.gameObject.tag == "Player") 
			{
				hit.collider.gameObject.GetComponent<Health> ().health -= 5f;
			}
		}
	}
}
