using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] textPlayerScores;
    public TextMeshProUGUI textPlayerLives;
    public TextMeshProUGUI textPlayerHealth;
    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    public static UIManager Instance { get; private set; }

    public void SetTextPlayerHealth(int health)
    {
        this.textPlayerHealth.text = $"Health: {health.ToString()}";
    }

    public void SetTextPlayerLives(int lives)
    {
        this.textPlayerLives.text = $"Lives: {lives.ToString()}";
    }

    public void SetTextPlayerScore(int score)
    {
        foreach (TextMeshProUGUI textPlayerScore in this.textPlayerScores)
        {
            textPlayerScore.text = $"Score: {score.ToString()}";
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }

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
}
