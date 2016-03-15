using UnityEngine;
using System.Collections.Generic;

public enum SoundType
{
	None,
	Jump1,
	Jump2,
	Jump3,
	Coin,
	Lose,
	Win,
	Click
}

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance;
	private Dictionary<SoundType, AudioClip> clips;

	public AudioSource source;

	public AudioClip jump1;
	public AudioClip jump2;
	public AudioClip jump3;
	public AudioClip coin;
	public AudioClip lose;
	public AudioClip win;
	public AudioClip click;	

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		clips = new Dictionary<SoundType, AudioClip>
		{
			{SoundType.Jump1, jump1},
			{SoundType.Jump2, jump2},
			{SoundType.Jump3, jump3},
			{SoundType.Coin, coin },
			{SoundType.Lose, lose },
			{SoundType.Win, win },
			{SoundType.Click, click }
		};
	}

	public static void Play(SoundType sound)
	{
		if (instance == null || instance.source == null)
		{
			Debug.LogWarning("SoundManager not present");
			return;
		}

		if (!instance.clips.ContainsKey(sound) || instance.clips[sound] == null)
		{
			Debug.LogWarning("No sound for " + sound);
		}

		instance.source.PlayOneShot(instance.clips[sound]);		
	}
}
