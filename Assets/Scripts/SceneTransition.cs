using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	private static SceneTransition instance;

	public SpriteRenderer screen;

	private Color from = new Color(0f, 0f, 0f, 1f);
	private Color to = new Color(0f, 0f, 0f, 0f);

	private Vector3 defaultPos;

	// Use this for initialization
	void Awake () {
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		instance = this;
		defaultPos = transform.position;
	}	

	public static void LoadScene(string scene)
	{
		instance.StartCoroutine(instance.LoadSceneCoroutine(scene, 0.5f, 0.5f));
	}

	public static void ReloadScene()
	{
		LoadScene(SceneManager.GetActiveScene().name);		
	}

	private IEnumerator LoadSceneCoroutine(string scene, float fadeOut, float fadeIn)
	{
		Color was = screen.color;
		for (float t = 0f; t <= 1.0f; t += Time.deltaTime / fadeOut)
		{
			screen.color = Color.Lerp(was, from, t);
			yield return null;
		}
		transform.position = defaultPos;
		SceneManager.LoadScene(scene);
		
		for (float t = 0f; t <= 1.0f; t += Time.deltaTime / fadeIn)
		{
			screen.color = Color.Lerp(from, to, t);
			yield return null;
		}		
	}

	public static IEnumerator FadeIn(Color color, float duration = 0)
	{
		Color to = new Color(color.r, color.g, color.b, 0);
		if (duration == 0)
		{
			instance.screen.color = to;
			yield break;
		}
		yield return instance.Fade(duration, color, to);
	}

	public static IEnumerator FadeOut(Color color, float duration = 0)
	{
		Color from = new Color(color.r, color.g, color.b, 0);
		if (duration == 0)
		{
			instance.screen.color = color;
			yield break;
		}
		yield return instance.Fade(duration, from, color);
	}

	private IEnumerator Fade(float duration, Color from, Color to)
	{		
		for (float t = 0f; t <= 1.0f; t += Time.deltaTime / duration)
		{
			screen.color = Color.Lerp(from, to, t);
			yield return null;
		}
	}

	public static void Quit()
	{
		instance.StartCoroutine(instance.QuitCoroutine(0.5f));
	}

	private IEnumerator QuitCoroutine(float fadeOut)
	{
		for (float t = 0f; t <= 1.0f; t += Time.deltaTime / fadeOut)
		{
			screen.color = Color.Lerp(to, from, t);
			yield return null;
		}

#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
