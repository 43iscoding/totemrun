using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Totem : MonoBehaviour
{
	public TotemPart[] parts;

	public GameObject destroyEffect;

	public float jumpSpeed = 20f;
	public float fallSpeed = 0.5f;

	private bool jump = false;

	public bool trail = false;

	public void Awake()
	{
		The.totem = this;
		UpdateTrail();
	}

	public void UpdateTrail()
	{
		foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
		{
			tr.enabled = trail;
		}
	}

	public void Jump()
	{
		jump = true;
	}

	public void FixedUpdate()
	{
		if (!The.level.running)
		{
			jump = false;
			return;
		}

		for (int i = 0; i < parts.Length; i++)
		{
			TotemPart part = parts[i];
					
			if (CanJump(i) && jump)
			{
				JumpFrom(i);
			}

			part.ApplyGravity();
		}

		foreach (TotemPart part in parts)
		{
			float newY = part.transform.position.y + part.verticalSpeed * Time.fixedDeltaTime;
			part.transform.position = new Vector3(part.transform.position.x, Mathf.Max(newY, part.FloorLevel()));			
		}

		jump = false;
	}

	private void JumpFrom(int index)
	{
		for (int i = index; i < parts.Length; i++)
		{
			parts[i].Jump();
		}
	}

	private bool CanJump(int index)
	{
		if (!parts[index].IsPowered()) return false;

		for (int i = 0; i < parts.Length; i++)
		{
			if (!parts[i].IsPowered()) continue;

			if (i >= index)
			{
				if (!parts[i].Grounded()) return false;
			}
			else if (i == index - 1)
			{
				if (parts[i].Grounded()) return false;
			}
		}
		return true;
	}

	public void Destroy()
	{
		StartCoroutine(DestroyCoroutine());
	}

	IEnumerator DestroyCoroutine()
	{
		List<Coroutine> destroyParts = new List<Coroutine>();
		foreach (TotemPart part in parts)
		{
			destroyParts.Add(StartCoroutine(part.DestroyCoroutine()));
		}

		foreach (Coroutine c in destroyParts)
		{
			yield return c;
		}

		foreach (TotemPart part in parts)
		{
			part.GetComponent<Renderer>().enabled = false;
		}

		destroyEffect.SetActive(true);
	}
}
