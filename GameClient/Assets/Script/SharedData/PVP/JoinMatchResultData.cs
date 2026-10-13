public class JoinMatchResultData
{
    public const string CONNECTION = "JoinMatchResult";
    public EMatchResult MatchResult { get; set; }
    public string RoomId { get; set; }
    public WaitingUserData OpponentUser { get; set; }
}