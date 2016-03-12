using UnityEngine;

public class CampaignSelectionLogic : MonoBehaviour {

	public LevelConfig[] levels;

	private static CampaignSelectionLogic instance;

	void Awake()
	{
		instance = this;
	}

	public static bool LoadLevel(int index)
	{		
		if (index < 0 || index >= instance.levels.Length)
		{
			Debug.LogError("No level " + index.ToString("00"));
			The.currentLevelConfig = null;
			return false;
		}

		The.currentLevelConfig = instance.levels[index];
		return true;
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
