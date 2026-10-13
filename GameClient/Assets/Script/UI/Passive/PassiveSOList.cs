using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSOList", menuName = "SO/PassiveList")]
public class PassiveSOList : ScriptableObject
{
    public List<PassiveSO> PassiveList;

    public PassiveSO GetPassiveByType(EPassiveType passiveType)
    {
        foreach(var passiveSO in PassiveList)
        {
            if(passiveSO.PassiveType == passiveType)
            {
                return passiveSO;
            }
        }

        return null;
    }
}