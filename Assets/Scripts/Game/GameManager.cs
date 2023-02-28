using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button gameOverMenuSelected;
    public Menu gameOverMenu;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLives;
    private int score = 0;
    public int lives = 3;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        this.textLives.text = this.lives.ToString();
    }

    public void AddScore(int score)
    {
        this.score += score;
        this.textScore.text = score.ToString();
    }

    public void DecrementLife()
    {
        this.lives -= 1;
        this.textLives.text = this.lives.ToString();
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }
}
