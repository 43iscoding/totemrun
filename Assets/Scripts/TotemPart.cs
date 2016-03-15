using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TotemPart : MonoBehaviour
{
	private const float EPSILON = 0.005f;

	private const float height = 1f;

	public TotemPart parent;

	internal float verticalSpeed;

	public float power;

	public bool IsPowered()
	{
		return power > 0;
	}

	public bool Grounded()
	{
		if (parent == null)
		{
			return transform.position.y - The.level.floorLevel < EPSILON;
		}
		else
		{
			return transform.position.y - (parent.transform.position.y + height) < EPSILON;
		}
	}

	public float FloorLevel()
	{
		return parent == null ? The.level.floorLevel : (height + parent.FloorLevel());
	}

	public void Jump()
	{
		verticalSpeed = The.totem.jumpSpeed;
	}

	public void ApplyGravity()
	{
		verticalSpeed -= The.totem.fallSpeed;
	}

	public IEnumerator DestroyCoroutine()
	{
		Vector3 initPos = transform.localPosition;
		Collider c = GetComponent<Collider>();
		c.enabled = false;
		Renderer r = GetComponent<Renderer>();
		float duration = 0.8f;
		float shake = 0.05f;
		for (float t = 0f; t <= 1.0f; t += Time.deltaTime / duration)
		{
			transform.localPosition = initPos + new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake));
			r.material.color = Color.Lerp(Color.white, Color.red, t);
			yield return null;
		}

		transform.localPosition = initPos;

	}
}
