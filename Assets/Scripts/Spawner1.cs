using UnityEngine;
using System.Collections;

public class Spawner1 : MonoBehaviour {

	public Wave[] waves;
	public Enemy enemy;

	LivingEntity playerEntity;
	Transform playerT;

	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	MapGenerator map;

	bool isDisabled;

	void Start(){
		playerEntity = FindObjectOfType<Player> ();
		playerT = playerEntity.transform;
		playerEntity.OnDeath += OnPlayerDeath;

		map = FindObjectOfType<MapGenerator> ();
		NextWave ();
	}

	void Update(){
		if (!isDisabled) {
			if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
			
				StartCoroutine (SpawnEnemy ());
			}
		}
	}
	IEnumerator SpawnEnemy() {
		float spawnDelay = 1;
		float tileFlashSpeed = 4;
		
		Transform randomTile = map.GetRandomOpenTile ();			
		Material tileMat = randomTile.GetComponent<Renderer> ().material;
		Color initalColor = tileMat.color;
		Color flashColor = Color.red;
		float spawnTimer = 0;

		while (spawnTimer < spawnDelay) {

			tileMat.color = Color.Lerp (initalColor, flashColor, Mathf.PingPong (spawnTimer * tileFlashSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
		}

		Enemy spawnedEnemy = Instantiate(enemy, randomTile.position + Vector3.up, Quaternion.identity) as Enemy;
		spawnedEnemy.OnDeath += OnEnemyDeath;
		spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColor);
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