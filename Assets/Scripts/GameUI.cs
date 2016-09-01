using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;

	public Text scoreUI;
	public Text gameOverScoreUI;
	public RectTransform healthBar;

	Player player;

	void Start () 
	{
		player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;

	}

	void Update(){
		scoreUI.text = ScoreKeeper.score.ToString();
		float healthPercent = 0;
		if (player != null) {
			healthPercent = player.health / player.startingHealth;

		}
		healthBar.localScale = new Vector3 (healthPercent, 1, 1);
	}

	void OnGameOver(){
		Cursor.visible = true;
		StartCoroutine (Fade (Color.clear, Color.black,1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive (false);
		healthBar.transform.parent.gameObject.SetActive (false);
		gameOverUI.SetActive (true);
	}

	IEnumerator Fade(Color from, Color to, float time)
	{
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from, to, percent);
			yield return null;
		}
	}

	//UI Input
	public void StartNewGame()
	{
		SceneManager.LoadScene ("Enemy Ai");
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene ("Main Menu");
	}

}
