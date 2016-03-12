using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ClickTest : MonoBehaviour {

	public UnityEvent action;

	void OnMouseDown()
	{
		if (action != null)
		{
			action.Invoke();
		}
	}
}
