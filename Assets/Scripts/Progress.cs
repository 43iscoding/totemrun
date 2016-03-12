using UnityEngine;

public class Progress {

	public static int LastLevel
	{
		get { return PlayerPrefs.GetInt(Prefs.LastLevel, 0); }
		set
		{
			PlayerPrefs.SetInt(Prefs.LastLevel, value);
		}
	}

	public static bool IsLevelUnlocked(int index)
	{
		return index <= LastLevel + 1;
	}

	public static bool IsLevelCompleted(int index)
	{
		int result = PlayerPrefs.GetInt(Prefs.Level + index, 0);
		return result == 1 || result == 2;
	}

	public static void LevelComplete(int index, bool perfect)
	{
		if (perfect)
		{
			PlayerPrefs.SetInt(Prefs.Level + index, 2);
		} else if (!IsLevelPerfected(index))
		{
			PlayerPrefs.SetInt(Prefs.Level + index, 1);
		}		
		
		if (index > LastLevel)
		{
			LastLevel = index;
		}
	}

	public static bool IsLevelPerfected(int index)
	{
		return PlayerPrefs.GetInt(Prefs.Level + index, 0) == 2;
	}

	public static int Highscore 
	{
		get { return PlayerPrefs.GetInt(Prefs.Highscore, 0); }
		set
		{
			PlayerPrefs.SetInt(Prefs.Highscore, value);
		}
	}

	public static void Reset()
	{
		PlayerPrefs.DeleteAll();
	}
}
