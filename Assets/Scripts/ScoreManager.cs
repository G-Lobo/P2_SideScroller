using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Referências UI")]
    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    

    public void AddPoint()
    {
        score += 10;
        
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }
}