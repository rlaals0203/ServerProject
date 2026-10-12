public class UserDataCheck
{
    public UserDataCheck(UserBaseData userData, UnitDataDefine unitData)
    {
        new UserUnitDataCheck(userData, unitData);
    }
}

public class UserUnitDataCheck
{
    public UserUnitDataCheck(UserBaseData userData, UnitDataDefine unitData)
    {
        userData.UserMonetaData ??= new Dictionary<string, int>();
        userData.UserPassiveData ??= new Dictionary<string, int>();
        userData.UserUnitData ??= new UserUnitData();
        userData.UserUnitData.OwnUnitList ??= new Dictionary<string, UnitData>();
        userData.UserUnitData.DeployUnitList ??= new Dictionary<string, UnitData>();

        if(userData.UserMonetaData.Count == 0)
        {
            foreach(EMonetaType monetaType in Enum.GetValues(typeof(EMonetaType)))
            {
                userData.UserMonetaData.Add(monetaType.ToString(), 0);
            }
        }

        if(userData.UserPassiveData.Count == 0)
        {
            foreach(EPassiveType passiveType in Enum.GetValues(typeof(EPassiveType)))
            {
                userData.UserPassiveData.Add(passiveType.ToString(), 1);
            }
        }

        if(userData.UserUnitData.OwnUnitList.TryGetValue(unitData.StarterUnitId, out var _) == false)
        {
            // UnitData starterUnit = new UpdateUnitData(unitData.GetUnitById(unitData.StarterUnitId), 1).UnitData;
            // userData.UserUnitData.OwnUnitList.Add(unitData.StarterUnitId, starterUnit);

            UnitData starterUnit1 = new UpdateUnitData(unitData.GetUnitById("1"), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add("1", starterUnit1);

            UnitData starterUnit2 = new UpdateUnitData(unitData.GetUnitById("2"), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add("2", starterUnit2);

            UnitData starterUnit3 = new UpdateUnitData(unitData.GetUnitById("3"), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add("3", starterUnit3);

            UnitData starterUnit4 = new UpdateUnitData(unitData.GetUnitById("4"), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add("4", starterUnit4);

            UnitData starterUnit5 = new UpdateUnitData(unitData.GetUnitById("5"), 1).UnitData;
            userData.UserUnitData.OwnUnitList.Add("5", starterUnit5);
        }
    }
}