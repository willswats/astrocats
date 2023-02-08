using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int scoreAdd)
    {
        Debug.Log(score);
        score += scoreAdd;
        scoreText.text = score.ToString();
    }
}
