using MINLib.Script.Core.Resource;
using System;
using TMPro;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList enemyList;
    [SerializeField] private UnitSpawner unitSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private MapCamera mapCamera;
    [SerializeField] private StageCompleteAnimation stageCompAnim;
    [SerializeField] private UnitSO friendlyCastleData;
    [SerializeField] private TMP_Text moneyText;

    private MapDataSO _mapData;

    private Castle _unitCastle;
    private Castle _enemyCastle;
    private Vector3 _enemyPosition;
    private Vector3 _unitPosition;

    private float _money;
    private int _maxMoney = 6000;
    private float _moneyMultiplier = 85f;

    private bool _battleFinished;


    private void Start()
    {
        InitializeBattleField(GlobalInstance.I.CurrentMap);
    }

    public void InitializeBattleField(MapDataSO mapData)
    {
        _mapData = mapData;
        _enemyPosition = new Vector3(-_mapData.CastleDistance, -1.8f, 0);
        _unitPosition = new Vector3(_mapData.CastleDistance, -1.8f, 0);

        enemySpawner.Initialize(mapData, enemyList, _enemyPosition, HandleEnemyDead);
        unitSpawner.Initialize(_unitPosition, HandleUnitSpawn);
        SpawnCastle();
    }

    private void Update()
    {
        enemySpawner.UpdateSpawner();

        _money += Time.deltaTime * _moneyMultiplier;
        _money = Mathf.Clamp(_money, 0, _maxMoney);
        moneyText.text = $"{(int)_money}/{_maxMoney}";
    }

    private void HandleUnitSpawn(int cost)
    {
        _money -= cost;
    }

    private void HandleEnemyDead(int money)
    {
        _money += money;
    }

    private void SpawnCastle()
    {
        _unitCastle = new SpawnCacheObject<Castle>("Castle", _unitPosition).Spawned;
        _unitCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(friendlyCastleData).UnitStatData, 1).UnitData, ETeamType.Friendly);
        _unitCastle.OnDead += HandleCastleDead;

        _enemyCastle = new SpawnCacheObject<Castle>("Castle", _enemyPosition).Spawned;
        _enemyCastle.Initialize(new UpdateUnitData(new MakeUnitStatData(_mapData.CastleData).UnitStatData, 1).UnitData, ETeamType.Enemy);
        _enemyCastle.OnDead += HandleCastleDead;
    }

    private async void HandleCastleDead(bool isEnemyCastle)
    {
        if (_battleFinished)
            return;

        mapCamera.FocusOn(isEnemyCastle ? _enemyCastle.GetCameraTrm() : _unitCastle.GetCameraTrm());
        stageCompAnim.PlayWinAnimation(_mapData.expDrop, CompleteBattle);
        _battleFinished = true;

        await NetworkManager.I.SendUserMonetaChangeRequest(EMonetaType.Exp, _mapData.expDrop, HandleReceiveExp);
    }

    private void HandleReceiveExp(UserMonetaChangePacketResultData data)
    {
        GlobalInstance.I.MainUser.UserMonetaData[EMonetaType.Exp.ToString()] = data.Amount;
    }

    public int GetMoney() => (int)_money;

    public void CompleteBattle()
    {
        enemySpawner.ReleaseSpawner();
        unitSpawner.ReleaseSpawner();

        ResourceManager.I.DespawnPool(_unitCastle.gameObject);
        ResourceManager.I.DespawnPool(_enemyCastle.gameObject);
    }

    private void OnDestroy()
    {
        CompleteBattle();
    }
}
