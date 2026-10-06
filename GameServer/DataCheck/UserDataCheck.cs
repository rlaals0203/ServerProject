public class UserDataCheck
{
    public UserDataCheck(UserBaseData userData)
    {
        new UserUnitDataCheck(userData);
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
    public UserUnitDataCheck(UserBaseData userData)
    {
        userData.UserUnitData ??= new UserUnitData();
        userData.UserUnitData.OwnUnitList ??= new Dictionary<string, UnitData>();
        userData.UserUnitData.DeployUnitList ??= new Dictionary<int, UnitData>();
        //userData.UserUnitData.OwnUnitList.Add("1", )
    }
}