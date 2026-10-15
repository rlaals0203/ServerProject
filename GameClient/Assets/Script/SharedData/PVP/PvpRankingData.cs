using System.Collections.Generic;
using System;

[Serializable]
public class PvpRankingData
{
    public Dictionary<string, UserPvpRankData> Ranks { get; set; }
    public DateTime UpdatedTime { get; set; }
}

[Serializable]
public class UserPvpRankData
{
    public string UserId { get; set; }
    public int Point { get; set; }
}