using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PassiveSO", menuName = "SO/Passive")]
public class PassiveSO : ScriptableObject
{
    public EPassiveType PassiveType;
    public Image PassiveIcon;
    public string PassiveName;
    public string PassiveDesc;
}