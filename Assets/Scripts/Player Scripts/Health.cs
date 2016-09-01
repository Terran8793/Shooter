using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health;
	public static bool isPlayerAlive = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (health < 0f) 
		{
			EnemyAI.isPlayerAlive = false;
			Destroy (gameObject);
		}

	
	}
}
