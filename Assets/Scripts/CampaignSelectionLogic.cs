using UnityEngine;

public class CampaignSelectionLogic : MonoBehaviour {

	public LevelConfig[] levels;

	private static CampaignSelectionLogic instance;

	void Awake()
	{
		instance = this;
	}

	public static void LoadLevel(int index)
	{
		index--;
		if (index < 0 || index >= instance.levels.Length)
		{
			Debug.LogError("No level " + index.ToString("00"));
			The.currentLevelConfig = null;
		}

		The.currentLevelConfig = instance.levels[index];
		SceneTransition.LoadScene("Campaign");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Back();
		}
	}

	public void Back()
	{
		SceneTransition.LoadScene("Menu");
	}
}
