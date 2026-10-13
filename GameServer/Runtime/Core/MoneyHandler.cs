public class MoneyHandler
{
    private float _userMoney;
    private float _opponentMoney;

    private float _userMoneyMultiplier;
    private float _opponentMoneyMultiplier;

    public MoneyHandler(UserBaseData userData, UserBaseData opponentData)
    {
        string passiveKey = EPassiveType.WorkerCatRate.ToString();
        _userMoneyMultiplier = 1f * userData.UserPassiveData[passiveKey] * GameDefine.AddictionalMoneyPerSecond;
        _opponentMoneyMultiplier = 1f * opponentData.UserPassiveData[passiveKey] * GameDefine.AddictionalMoneyPerSecond;
    }

    public void Update(float dt)
    {
        _userMoney += dt * _userMoneyMultiplier * GameDefine.DefaultMoneyPerSecond;
        _opponentMoney += dt * _opponentMoneyMultiplier * GameDefine.DefaultMoneyPerSecond;
    }

    public void ChargeMoneyByDeadUnit(RuntimeUnit unit)
    {
        int dropMoney = unit.UnitData.UnitStatData.UnitDropMoneyAmount;
        
        if(unit.IsOpponent)
            _opponentMoney += dropMoney;
        else
            _userMoney += dropMoney;
    }

    public float GetMoney(bool isOpponent)
    {
        return isOpponent ? _opponentMoney : _userMoney;
    }
}