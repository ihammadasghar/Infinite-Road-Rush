using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public static double secondsPassed = 0;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;

	public enum gameStates {Playing, Death, GameOver, BeatLevel};
	public gameStates gameState = gameStates.Playing;

	public int score=0;
	public int health=100;
	public int bombs = 1;
	public bool canBeatLevel = false;
	public int beatLevelScore=0;
	public static double maxDifficultySeconds = 120;

	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public Text healthDisplay;
	public Text secondsDisplay;
	public Text difficultyDisplay;
	public Text nightProbabilityDisplay;
	public Text intervalOfObstaclesDisplay;
	public Text intervalOfEnemySpeedDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public GameObject beatLevelCanvas;

	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public AudioClip beatLevelSFX;
	private Health playerHealth;
	public static double obstaclePositionNormalBias = 3.0;
	public static int maxObstacles = 50;
	public static int obstaclesIntervalLen = 10;
	public static int obstaclesUpperLimit;
	public static double enemySpeedNormalBias = 3.0;
	public static int maxEnemySpeed = 50;
	public static int enemySpeedIntervalLen = 20;
	public static int enemySpeedUpperLimit;
	public static double nightProbability;

	void Start () {
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		playerHealth = player.GetComponent<Health>();

		// setup score display
		Collect (0);

		// make other UI inactive
		gameOverCanvas.SetActive (false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive (false);
	}

	void Update () {
   		secondsPassed += Time.deltaTime;
		secondsDisplay.text = ((int)secondsPassed).ToString();

		nightProbability = Math.Min(1.0, secondsPassed/maxDifficultySeconds);
		difficultyDisplay.text = Math.Round(nightProbability, 2).ToString();
		nightProbabilityDisplay.text = difficultyDisplay.text;

		obstaclesUpperLimit = (int)Math.Max(obstaclesIntervalLen, (maxObstacles+1) * Math.Min(1, secondsPassed/maxDifficultySeconds));
		intervalOfObstaclesDisplay.text = (obstaclesUpperLimit - obstaclesIntervalLen) + ", " + obstaclesUpperLimit;

		enemySpeedUpperLimit = (int)Math.Max(enemySpeedIntervalLen, (maxEnemySpeed+1) * Math.Min(1, secondsPassed/maxDifficultySeconds));
		intervalOfEnemySpeedDisplay.text = (enemySpeedUpperLimit - enemySpeedIntervalLen) + ", " + enemySpeedUpperLimit;

		switch (gameState)
		{
			case gameStates.Playing:
				if (playerHealth.isAlive == false)
				{
					// update gameState
					gameState = gameStates.Death;

					// set the end game score
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// switch which GUI is showing		
					mainCanvas.SetActive (false);
					gameOverCanvas.SetActive (true);
				} else if (canBeatLevel && score>=beatLevelScore) {
					// update gameState
					gameState = gameStates.BeatLevel;

					// hide the player so game doesn't continue playing
					player.SetActive(false);

					// switch which GUI is showing			
					mainCanvas.SetActive (false);
					beatLevelCanvas.SetActive (true);
				}
				break;
			case gameStates.Death:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (gameOverSFX,gameObject.transform.position);

					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.BeatLevel:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (beatLevelSFX,gameObject.transform.position);
					
					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.GameOver:
				// nothing
				break;
		}

	}


	public void Collect(int amount) {
		score += amount;
		if (canBeatLevel) {
			mainScoreDisplay.text = score.ToString () + " of "+beatLevelScore.ToString ();
		} else {
			mainScoreDisplay.text = score.ToString ();
		}

	}

	public void AddDamage(float amount) {
		health -= (int)amount;
		if (canBeatLevel) {
			healthDisplay.text = health.ToString ();
		} else {
			healthDisplay.text = health.ToString ();
		}

	}

	public void CollectBomb(int amount)
	{
		bombs += amount;
		mainScoreDisplay.text = bombs.ToString();
		

	}
}
