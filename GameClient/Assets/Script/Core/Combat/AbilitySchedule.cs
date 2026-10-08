public delegate void OnAbilityStatusChange(ESpecialAbilityType abilityType, bool status);

public class AbilitySchedule
{
    private bool _status = false;
    private float _remainTime;
    private ESpecialAbilityType _abilityType;

    public event OnAbilityStatusChange OnStatusChange;

    public AbilitySchedule(ESpecialAbilityType abilityType)
    {
        _abilityType = abilityType;
    }

    public void UpdateSchedule(float dt)
    {
        if (_status == false)
            return;

        _remainTime -= dt;

        if (_remainTime <= 0f)
        {
            _remainTime = 0f;
            ChangeStatus(false);
        }
    }

    public void ModifyTime(float duration)
    {
        if (_status == false)
        {
            ChangeStatus(true);
        }

        _remainTime += duration;
    }

    private void ChangeStatus(bool status)
    {
        _status = status;
        OnStatusChange?.Invoke(_abilityType, status);
    }
}