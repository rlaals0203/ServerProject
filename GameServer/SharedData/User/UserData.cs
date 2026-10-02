[Serializable]
public class UserBaseData
{
    public string UserId { get; set;}
    public string Password { get; set; }
    public UserUnitData UserUnitData {get; set;}
    public UserMonetaData UserMonetaData { get; set; }
}