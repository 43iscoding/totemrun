using UnityEngine;

public class Rotating : MonoBehaviour
{
	public Vector3 rotation;
	public Level level;
	
	void FixedUpdate()
	{
		if (level != null && !level.running) return;

		transform.Rotate(rotation * Time.fixedDeltaTime);
	}
}
