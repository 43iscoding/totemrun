using UnityEngine;
using System.Collections;

public class SimpleSpawner : MonoBehaviour
{
	public Transform container;
	public GameObject prefab;
	public string type;

	public int minHeight = 0;
	public int maxHeight = 6;

	public int interval = 20;

	public int limit = 50;

	public int start = 20;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < limit; i++)
		{
			GameObject obj = Instantiate(prefab, new Vector3(start + i*interval, Random.Range(minHeight, maxHeight)), Quaternion.identity) as GameObject;
			obj.name = type + " " + (i + 1);
			obj.transform.SetParent(container);
		}
	}
}
