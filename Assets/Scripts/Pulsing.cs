using UnityEngine;

public class Pulsing : MonoBehaviour
{

	public Vector3 defaultScale = Vector3.one;
	public float grow;
	public float shrink;

	// Use this for initialization
	void Start ()
	{
		transform.localScale = defaultScale;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, shrink);
	}

	public void Pulse()
	{
		transform.localScale = transform.localScale * (1 + grow);
	}
}
