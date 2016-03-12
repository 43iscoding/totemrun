using System.Collections;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	public TextMesh scoreText;
	public TextMesh loseScoreText;
	public TextMesh highScoreText;
	
	void Start () {
		Messenger.AddListener<int, int, bool>(Events.ScoreChanged, UpdateScore);
	}

	void UpdateScore(int score, int highscore, bool pulse)
	{
		if (scoreText != null)
		{
			scoreText.text = score.ToString();
			if (pulse)
			{
				Pulsing pulsing = scoreText.GetComponent<Pulsing>();
				if (pulsing != null)
				{
					pulsing.Pulse();
				}
			}			
		}

		if (loseScoreText != null)
		{
			loseScoreText.text = "Score: " + score;
		}

		if (highScoreText != null)
		{
			highScoreText.text = "Best: " + highscore;
		}
	}	
}
