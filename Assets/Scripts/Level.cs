using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public float floorLevel;

	public Color winColor;
	public Color loseColor;

	public GameObject loseScreen;
	public GameObject winScreen;
	public GameObject pauseScreen;
	public GameObject perfect;
	public GameObject newBest;
	public GameObject UI;

	public Collider pauseButton;

	public bool endless;

	public Transform coinContainer;
	public Transform enemyContainer;

	public GameObject coinPrefab;
	public GameObject enemyPrefab;
	public GameObject exitPrefab;

	public bool running;

	public float levelSpeed = 5;

	void Awake()
	{
		The.level = this;
	}

	void Start()
	{
		if (endless) return;

		LevelConfig lvl = The.currentLevelConfig;

		if (lvl == null)
		{
			Debug.LogWarning("Not loading level");
			return;
		}
		else
		{
			Debug.Log("Loading level " + lvl.index);
		}
		
		//Cleanup
		for (int i = 0; i < coinContainer.childCount; i++)
		{
			Destroy(coinContainer.GetChild(i).gameObject);
		}

		for (int i = 0; i < enemyContainer.childCount; i++)
		{
			Destroy(enemyContainer.GetChild(i).gameObject);
		}

		foreach (Vector2 pos in lvl.coins)
		{
			SpawnCoin(pos);
		}

		foreach (Vector2 pos in lvl.enemies)
		{
			SpawnEnemy(pos);
		}

		levelSpeed = lvl.speed;

		SpawnExit(lvl.exit);
	}	

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Menu();
		}
	}

	void FixedUpdate()
	{
		if (!running) return;
		
		Move(The.totem.transform);
		Move(Camera.main.transform);
		Move(UI.transform);
	}

	void Move(Transform t)
	{
		t.position = t.position + Vector3.right * levelSpeed * Time.fixedDeltaTime;
	}

	public void Lose()
	{
		StartCoroutine(LoseCoroutine());
	}

	public void Win()
	{
		StartCoroutine(WinCoroutine(coinContainer.childCount == 0));
	}

	IEnumerator WinCoroutine(bool perfect)
	{
		Progress.LevelComplete(The.currentLevelConfig.index, perfect);
		SoundManager.Play(SoundType.Win);
		running = false;
		//yield return new WaitForSeconds(1f);
		yield return SceneTransition.FadeOut(winColor, 1f);
		winScreen.SetActive(true);
		this.perfect.SetActive(perfect);
		yield return SceneTransition.FadeIn(winColor);
	}

	IEnumerator LoseCoroutine()
	{
		running = false;
		SoundManager.Play(SoundType.Lose);
		The.totem.Destroy();
		yield return new WaitForSeconds(1f);
		yield return SceneTransition.FadeOut(loseColor, 1f);
		loseScreen.SetActive(true);
		if (newBest != null)
		{
			newBest.SetActive(The.gameLogic.NewBest());
		}
		yield return SceneTransition.FadeIn(loseColor);
	}

	public void Restart()
	{
		SceneTransition.ReloadScene();
	}

	public void Menu()
	{
		if (endless)
		{
			SceneTransition.LoadScene("Menu");
		}
		else
		{
			SceneTransition.LoadScene("CampaignSelection");
		}
	}

	public void NextLevel()
	{
		CampaignSelectionLogic.LoadLevel(The.currentLevelConfig.index + 1);
	}

	public void SpawnCoin(Vector2 pos)
	{
		GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity) as GameObject;
		coin.transform.SetParent(coinContainer.transform);
	}

	public void SpawnEnemy(Vector2 pos)
	{
		GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
		enemy.transform.SetParent(enemyContainer.transform);
	}

	private void SpawnExit(float x)
	{
		GameObject exit = Instantiate(exitPrefab, Vector3.right * x, Quaternion.identity) as GameObject;
		exit.transform.SetParent(transform);
	}

	public void Pause()
	{
		running = !running;
		pauseScreen.SetActive(!running);
		pauseButton.enabled = running;
	}
}
