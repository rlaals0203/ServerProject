using TMPro;
using UnityEngine;

public class MonetaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text monetaText;

    private void Awake()
    {
        monetaText.text = GameManager.I.User.UserMonetaData.Exp.ToString();
    }
}
