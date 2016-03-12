using UnityEngine;

public class PerformanceControl : MonoBehaviour
{
	public TextMesh text;

	private long frames;
	private float deltaTime;

	void Update()
	{
		frames++;
		deltaTime += Time.deltaTime;
		UpdateText();
	}

	void ResetFPS()
	{
		frames = 0;
		deltaTime = 0;
	}

	int FPS()
	{
		return (int) (frames / deltaTime);
	}

	void UpdateText()
	{
		text.text = FPS().ToString();
	}

	public void ToggleFPS()
	{
		Renderer r = GetComponent<Renderer>();
		r.enabled = !r.enabled;
	}
}
