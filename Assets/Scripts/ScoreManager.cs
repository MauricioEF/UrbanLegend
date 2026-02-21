using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI canvas;

    private int Score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public int CurrentScore() 
    {
     return Score;
    }

    public void AddPoints() 
    {
        Score++;
        UpdateUI();
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void UpdateUI() 
    {
        canvas.text = $"Score: {Score}";
    }

}
