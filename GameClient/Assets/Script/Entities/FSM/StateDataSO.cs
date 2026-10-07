using UnityEngine;
public enum EStateType
{
    Idle,
    Move,
    Attack,
    Hitback,
    Knockback
}

[CreateAssetMenu(fileName = "StateData", menuName = "SO/StateData", order = 0)]
public class StateDataSO : ScriptableObject
{
    public EStateType stateType;
    public string className;
    public string animParamName;
    public int animationHash;

    private void OnValidate()
    {
        animationHash = Animator.StringToHash(animParamName);
    }
}