using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textPlayerScore;
    public TextMeshProUGUI textPlayerLives;
    public TextMeshProUGUI textPlayerHealth;
    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    public static UIManager Instance { get; private set; }

    public void SetTextPlayerHealth(int health)
    {
        this.textPlayerHealth.text = health.ToString();
    }

    public void SetTextPlayerLives(int lives)
    {
        this.textPlayerLives.text = lives.ToString();
    }

    public void SetTextPlayerScore(int score)
    {
        this.textPlayerScore.text = score.ToString();
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
