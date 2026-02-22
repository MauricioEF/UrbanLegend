using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI canvas;
    private int FinalScore;

    private void Start()
    {
      FinalScore =  GameManager.Instance.GetFinalScore();
      canvas.text = $"Score: {FinalScore}";
    }

}
