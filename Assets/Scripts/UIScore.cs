using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    private uint score;
    private string scoreText = "Score: ";
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(uint currentScore)
    {
        this.score = currentScore;

        if (textMesh != null)
            textMesh.text = scoreText + score.ToString("0");
    }

    public uint GetScore() => score;
}