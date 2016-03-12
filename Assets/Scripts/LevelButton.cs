using UnityEngine;

[ExecuteInEditMode]
public class LevelButton : MonoBehaviour
{
	public TextMesh text;
	public BoxCollider hitbox;

	public Color locked;
	public Color unlocked;
	public Color completed;
	public Color perfected;

	public int index;

	void Awake()
	{
		name = "Level" + index.ToString("00");
		hitbox.enabled = Progress.IsLevelUnlocked(index);
		if (Progress.IsLevelUnlocked(index))
		{
			if (Progress.IsLevelCompleted(index))
			{
				text.color = Progress.IsLevelPerfected(index) ? perfected : completed;
			}
			else
			{
				text.color = unlocked;
			}
		}
		else
		{
			text.color = locked;
		}

		text.text = index.ToString("00");
	}

	public void LoadLevel()
	{
		CampaignSelectionLogic.LoadLevel(index);
	}
}
