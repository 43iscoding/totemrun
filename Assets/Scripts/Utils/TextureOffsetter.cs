using UnityEngine;
using System.Collections;

public class TextureOffsetter : MonoBehaviour
{
	public Renderer texture;

	public Vector2 scrollSpeed;

	// Use this for initialization
	void Start () {
		texture.material.mainTextureOffset = Vector2.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		texture.material.mainTextureOffset = scrollSpeed*Time.realtimeSinceStartup;
	}
}
