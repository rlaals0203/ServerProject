using TMPro;
using UnityEngine;

public class PVPCastleHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text unitCastleHealthText;
    [SerializeField] private TMP_Text opponentCastleHealthText;

    [SerializeField] private Transform unitCastleCameraTrm;
    [SerializeField] private Transform opponentCastleCameraTrm;

    [SerializeField] private MapCamera mapCamera;
    [SerializeField] private StageCompleteAnimation stageCompAnim;

    public void UpdateCastleHealthText(UpdateCastleDataMessage message, bool isOpponent)
    {
        string unitCastleString = $"{Mathf.Clamp(message.UnitCastleData.CurrentHealth, 0, float.MaxValue)} / {message.UnitCastleData.MaxHealth}";
        string opponentCastleString = $"{Mathf.Clamp(message.OpponentCastleData.CurrentHealth, 0, float.MaxValue)} / {message.OpponentCastleData.MaxHealth}";

        if(isOpponent)
        {
            opponentCastleHealthText.text = unitCastleString;
            unitCastleHealthText.text = opponentCastleString;
        }
        else
        {
            unitCastleHealthText.text = unitCastleString;
            opponentCastleHealthText.text = opponentCastleString;
        }

        if (message.OpponentCastleData.CurrentHealth <= 0)
        {
            if (isOpponent)
                LoseCastle();
            else
                WinCastle();
        }
        else if (message.UnitCastleData.CurrentHealth <= 0)
        {
            if (isOpponent)
                WinCastle();
            else
                LoseCastle();
        }
    }

    private void WinCastle()
    {
        mapCamera.FocusOn(opponentCastleCameraTrm);
        stageCompAnim.PlayWinAnimation(1000);
    }

    private void LoseCastle()
    {
        mapCamera.FocusOn(unitCastleCameraTrm);
        stageCompAnim.PlayLoseAnimation();
    }
}