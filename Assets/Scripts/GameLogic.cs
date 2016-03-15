using UnityEngine;
using System.Collections;
using System.Timers;

public class GameLogic : MonoBehaviour
{
	private int score;
	private Timer timer;

	public void Awake()
	{
		The.gameLogic = this;
	}

	public void Start()
	{
		Messenger.Broadcast(Events.ScoreChanged, score, Progress.Highscore, false);
		//ScoreTimer();
		//StartCoroutine(TimeScoreCoroutine());
	}

	void ScoreTimer()
	{
		timer = new Timer(1);
		timer.Elapsed += ((sender, e) =>
		{
			AddScore(1, false);
		});
		timer.Enabled = true;
	}

	IEnumerator TimeScoreCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			while (!The.level.running)
			{
				yield return null;
			}
			AddScore(1, false);
		}
	}

	public void AddScore(int value, bool pulse)
	{
		score += value;
		if (score > Progress.Highscore)
		{
			Progress.Highscore = score;
		}
		Messenger.Broadcast(Events.ScoreChanged, score, Progress.Highscore, pulse);
	}

	public bool NewBest()
	{
		return score == Progress.Highscore;
	}
}
