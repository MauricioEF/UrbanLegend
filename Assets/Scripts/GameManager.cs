using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private int FinalScore;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void TimeLimit(int Score) 
    {
        FinalScore = Score;
        SceneManager.LoadScene(2);
       
    }

    public int GetFinalScore() 
    {
        return FinalScore;
    }
}
