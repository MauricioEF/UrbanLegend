using TMPro;
using UnityEngine;

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

    public void AddPoints(NPCActionKind kind)
    {
        int points = kind switch
        {
            NPCActionKind.Smoking => 5,
            NPCActionKind.Drinking => 4,
            NPCActionKind.Fighting => 8,
            NPCActionKind.Walking => -3,
            NPCActionKind.Idle => -5,
            _ => 1
        };
        Score += points;
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
