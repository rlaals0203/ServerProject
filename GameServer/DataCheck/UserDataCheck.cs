public class UserDataCheck
{
    public UserDataCheck(UserBaseData userData, UnitDataDefine unitData)
    {
        new UserUnitDataCheck(userData, unitData);
        new UserMonetaDataCheck(userData);
    }
}

public class UserMonetaDataCheck
{
    public UserMonetaDataCheck(UserBaseData userData)
    {
        userData.UserMonetaData ??= new UserMonetaData()
        {
            CatFood = 0,
            Exp = 0,
        };
    }
}

public class UserUnitDataCheck
{
    public UserUnitDataCheck(UserBaseData userData, UnitDataDefine unitData)
    {
        userData.UserUnitData ??= new UserUnitData();
        userData.UserUnitData.OwnUnitList ??= new Dictionary<string, UnitData>();
        userData.UserUnitData.DeployUnitList ??= new Dictionary<int, UnitData>();

        if(userData.UserUnitData.OwnUnitList.TryGetValue(unitData.StarterUnitId, out var _) == false)
        {
            UnitData starterUnit = new UpdateUnitData(unitData.GetUnitById(unitData.StarterUnitId), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add(unitData.StarterUnitId, starterUnit);
        }
    }
}