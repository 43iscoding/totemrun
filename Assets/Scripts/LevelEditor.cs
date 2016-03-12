using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour
{
	[Range(1, 20)]
	public int snapX = 1;
	private int snapY = 1;
		
	public void CreateLevelConfig()
	{
		GameObject go = new GameObject();
		go.name = "Level Config";
		LevelConfig cfg = go.AddComponent<LevelConfig>();
		//Add Coins
		Coin[] coins = MonoBehaviour.FindObjectsOfType<Coin>();
		cfg.coins = new Vector2[coins.Length];
		for (int i = 0; i < coins.Length; i++)
		{
			Snap(coins[i].transform);
			cfg.coins[i] = new Vector2(coins[i].transform.position.x, coins[i].transform.position.y);
		}
		//Add Enemies
		Enemy[] enemies = MonoBehaviour.FindObjectsOfType<Enemy>();
		cfg.enemies = new Vector2[enemies.Length];
		for (int i = 0; i < enemies.Length; i++)
		{
			Snap(enemies[i].transform.parent);
			cfg.enemies[i] = new Vector2(enemies[i].transform.parent.position.x, enemies[i].transform.parent.position.y);
		}		
	}

	private void Snap(Transform t)
	{		
		t.position = new Vector3(Snap(t.position.x, snapX), Snap(t.position.y, snapY));
	}

	private int Snap(float value, int snap)
	{
		float mult = (value / snap);
		return snap * Mathf.RoundToInt(mult);
	}
}
