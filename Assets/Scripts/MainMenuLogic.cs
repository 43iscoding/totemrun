using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MainMenuLogic : MonoBehaviour
{

	public GameObject campaign;
	public GameObject endless;

	public bool campaignUnlocked;
	public bool endlessUnlocked;

	void Start()
	{
		UpdateButtons();
	}

	void UpdateButtons()
	{
		campaign.SetActive(campaignUnlocked);
		endless.SetActive(endlessUnlocked);
	}

	void Update()
	{
		if (Application.isEditor)
		{
			UpdateButtons();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneTransition.Quit();
		}
	}

	public void OnCampaignClick()
	{
		SceneTransition.LoadScene("CampaignSelection");
	}

	public void OnEndlessClick()
	{
		SceneTransition.LoadScene("Endless");
		SoundManager.TransitionToGame();
	}

	public void OnExitClick()
	{
		SceneTransition.Quit();
	}

	public void OnResetClick()
	{
		Progress.Reset();
	}
}
