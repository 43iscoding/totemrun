using UnityEngine;

public class EmitOnce : MonoBehaviour
{

	public ParticleSystem[] particles;

	public int emitRate;

	void OnEnable()
	{		
		foreach (ParticleSystem system in particles)
		{
			system.Emit(emitRate);			
		}
	}
}
