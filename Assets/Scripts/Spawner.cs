using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public bool devMode;

	public Wave[] waves;
	public Enemy enemy;

	LivingEntity playerEntity;
	Transform playerT;

	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	bool isDisabled;

	void Start(){
		playerEntity = FindObjectOfType<Player> ();
		playerT = playerEntity.transform;
		playerEntity.OnDeath += OnPlayerDeath;

		NextWave ();
	}

	void Update(){
		if (!isDisabled) {
			if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
			
				Enemy spawnedEnemy = Instantiate (enemy, Vector3.zero, Quaternion.identity) as Enemy;
				spawnedEnemy.OnDeath += OnEnemyDeath;
				spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColor);
			}
		}
		if(devMode)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				StopCoroutine("SpawnEnemy");
				foreach (Enemy Enemy in FindObjectsOfType<Enemy>())
				{
					GameObject.Destroy(Enemy.gameObject);
				}
				NextWave();
			}
		}
	}
		
	void OnPlayerDeath(){
		isDisabled = true;
	}

	void OnEnemyDeath(){
		enemiesRemainingAlive --;

		if (enemiesRemainingAlive == 0) {
			NextWave ();
		}
	}

	void NextWave() {
		currentWaveNumber++;

		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves [currentWaveNumber - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;

			}
		}

	[System.Serializable]
	public class Wave{
		public bool infinite;
		public int enemyCount;
		public float timeBetweenSpawns;

		public float moveSpeed;
		public int hitsToKillPlayer;
		public float enemyHealth;
		public Color skinColor;

	}
}