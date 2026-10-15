using TMPro;
using UnityEngine;

public class PvpRankUIElement : MonoBehaviour
{
    [SerializeField] private TMP_Text rankNumberText;
    [SerializeField] private TMP_Text userIdText;
    [SerializeField] private TMP_Text pointText;

    public void Initialize(int rankNumber, string userId, int point)
    {
        rankNumberText.text = $"{rankNumber}À§";
        userIdText.text = userId.ToString();
        pointText.text = $"{point.ToString()}Á¡";
    }
}