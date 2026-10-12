using TMPro;
using UnityEngine;

public class MonetaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text monetaText;

    private int _prevValue;

    private void Awake()
    {
        monetaText.text = GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()].ToString();
    }

    private void Update()
    {
        if(_prevValue != GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()])
        {
            _prevValue = GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()];
            monetaText.text = _prevValue.ToString();
        }
    }
}
