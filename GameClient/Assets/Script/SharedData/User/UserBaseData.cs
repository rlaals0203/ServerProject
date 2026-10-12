using System;
using System.Collections.Generic;

[Serializable]
public class UserBaseData
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public Dictionary<EMonetaType, int> UserMonetaData { get; set; }
    public Dictionary<EPassiveType, int> UserPassiveData { get; set; }
    public UserUnitData UserUnitData { get; set; }
}