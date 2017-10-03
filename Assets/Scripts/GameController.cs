using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public GameObject hazard;
	public Vector3 spawnValues;

	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool gameover;
	private bool restart;

	void Start()
	{
		score = 0;
		gameover = false;
		restart = false;
		restartText.text = gameOverText.text = string.Empty;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (!restart) return;
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
		}
	}

	public void AddScore(int value)
	{
		score += value;
		UpdateScore ();
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameover = true;
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(!gameover)
		{
			for (int i = 0; i < hazardCount; ++i) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameover) 
			{
				restartText.text = "Press 'R' for restart";
				restart = true;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = string.Format ("Score: {0}", score);
	}

}