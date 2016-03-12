using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
	public int value;	

	private bool collected;

	public void OnTriggerEnter(Collider collider)
	{
		if (collected) return;

		collected = true;
		The.gameLogic.AddScore(value, true);
		Destroy(gameObject);
	}	
}
