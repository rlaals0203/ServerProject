using TMPro;
using UnityEngine;

public class MonetaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text monetaText;

    private int _prevValue;

    private void Awake()
    {
        monetaText.text = GameManager.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()].ToString();
    }

    private void Update()
    {
        if(_prevValue != GameManager.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()])
        {
            _prevValue = GameManager.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()];
            monetaText.text = _prevValue.ToString();
        }
    }
}
