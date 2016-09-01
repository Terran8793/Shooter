using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score { get; private set;}
	public float lastEnemyKillTime;
	int streakCount;
	public float streakExpiryTime = 1;

	void Start(){
		Enemy.OnDeathStatic += OnEnemyKilled;
		FindObjectOfType<Player> ().OnDeath += OnPlayerDeath;
	}

	void OnEnemyKilled(){

		if (Time.time < lastEnemyKillTime + streakExpiryTime) 
		{
			streakCount++;
		} 
		else 
		{
			streakCount = 0;
		}

		lastEnemyKillTime = Time.time;

		score += 5 + (int)Mathf.Pow(2,streakCount);
	}

	void OnPlayerDeath(){
		Enemy.OnDeathStatic -= OnEnemyKilled;
	}
}
