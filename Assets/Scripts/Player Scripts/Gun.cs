using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public enum FireMode{Auto, Single};
	public FireMode fireMode;

	public Transform[] projectileSpawn;
	public Projectile projectile;
	public float msBetweenShots = 100;
	public float muzzleVelocity = 35;

	float nextShotTime;

	bool triggerReleasedSinceLastShot;


	void Update(){
		}

	void Shoot(){

		if (Time.time > nextShotTime) {
			if (fireMode == FireMode.Single){
				if (!triggerReleasedSinceLastShot) {
					return;
				}
		}

			for (int i = 0; 1 < projectileSpawn.Length; i++) {
				nextShotTime = Time.time + msBetweenShots / 1000;
				Projectile newProjectile = Instantiate (projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
				newProjectile.SetSpeed (muzzleVelocity); 
			}
		}
	}

	public void OnTriggerHold()
	{
		Shoot ();
		triggerReleasedSinceLastShot = false;
	}

	public void OnTriggerRelease()
	{
		triggerReleasedSinceLastShot = true;

	}
}