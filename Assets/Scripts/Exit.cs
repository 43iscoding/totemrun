using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{
	private bool collected;

	public void OnTriggerEnter(Collider collider)
	{
		if (collected) return;

		collected = true;
		The.level.Win();
	}
}
