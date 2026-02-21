using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TimeManager : MonoBehaviour
{
    
    public static TimeManager Instance;
    private int Timer = 60;
    private bool Stopped = false;
    public TextMeshProUGUI canvas;
    public float counter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

            Instance = this;
    }

    public void SetTime(int time) 
    {
     Timer = time;
     SetUI();
    }

    public void StopTime() 
    {
        Stopped = true;
    }

    public void ContinueTime() 
    {
        Stopped = false;
    }

    public void StartTime() 
    {
        Timer -= 1;
        SetUI();
    }

    private void Update()
    {
        if (Timer == 0)
        {
            StopTime();

        }

        counter += Time.deltaTime;

        if (counter >= 1f)
        {
            counter = 0f;
            DecreaseTimer();
        }
        
    }

    public void DecreaseTimer() 
    {
        if (!Stopped)
        {
            StartTime();
        }
    }

    public void SetUI() 
    {
        canvas.text = $"Timer : {Timer}";
    }
}
