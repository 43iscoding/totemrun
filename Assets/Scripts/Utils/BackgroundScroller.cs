using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
	public SpriteRenderer sprite;
	public float scrollSpeed;

	private Transform first;
	private Transform second;

	private float size;

	void Start()
	{
		size = sprite.bounds.extents.x * 2;
		second = new GameObject(name + " 2").transform;
		second.SetParent(transform.parent, true);
		Vector3 pos = transform.localPosition;
		second.localPosition = new Vector3(pos.x + size, pos.y, pos.z);
		second.localScale = transform.localScale;
		SpriteRenderer cloneRenderer = second.gameObject.AddComponent<SpriteRenderer>();
		cloneRenderer.sortingOrder = sprite.sortingOrder;
		cloneRenderer.sprite = sprite.sprite;
	}

	void FixedUpdate()
	{
		if (!The.level.running) return;

		Vector3 pos = transform.localPosition;
		float offset = scrollSpeed*Time.realtimeSinceStartup % size;
		transform.localPosition = new Vector3(-offset, pos.y, pos.z);
		second.localPosition = transform.localPosition + Vector3.right * this.size;
	}
}
