public class JoinMatchResultData
{
    public const string CONNECTION = "JoinMatchResult";
    public EMatchResult MatchResult { get; set; }
    public string RoomId { get; set; }
    public MatchingUserData OpponentUser { get; set; }
    public bool IsOpponent { get; set; }
}