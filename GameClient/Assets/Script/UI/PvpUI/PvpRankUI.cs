using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PvpRankUI : ContentUI
{
    [SerializeField] private Transform contentTrm;

    private Queue<PvpRankUIElement> _elementQueue = new();

    public override async void Initialize()
    {
        base.Initialize();
        await NetworkManager.I.SendGetRankDataRequest(HandleLoadRanking);
    }

    private void HandleLoadRanking(GetPvpRankPacketResultData data)
    {
        var rankList = data.RankingData.Ranks.Values.OrderBy(rank => rank.Point);
        Queue<PvpRankUIElement> elementQueue = new();
        int count = 1;

        foreach (var rankData in rankList)
        {
            if(_elementQueue.TryDequeue(out var element))
            {
                element.Initialize(count, rankData.UserId, rankData.Point);
                elementQueue.Enqueue(element);
                continue;
            }

            new SpawnCacheObject<PvpRankUIElement>("PvpRankUIElement", Vector3.zero, contentTrm, spawned =>
            {
                spawned.Initialize(count, rankData.UserId, rankData.Point);
                elementQueue.Enqueue(element);
            });

            count++;
        }

        _elementQueue = elementQueue;
    }
}