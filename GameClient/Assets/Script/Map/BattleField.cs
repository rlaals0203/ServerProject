using System;
using TMPro;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList enemyList;
    [SerializeField] private UnitSpawner unitSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private MapCamera mapCamera;
    [SerializeField] private UnitSO friendlyCastleData;
    [SerializeField] private TMP_Text moneyText;

    private MapDataSO _mapData;

    private Castle _unitCastle;
    private Castle _enemyCastle;
    private Vector3 _enemyPosition;
    private Vector3 _unitPosition;

    private float _money;
    private int _maxMoney = 6000;
    private float _moneyMultiplier = 20f;


    private void Start()
    {
        InitializeBattleField(GameManager.I.CurrentMap);
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

    private void HandleCastleDead(bool isEnemyCastle)
    {
        CompleteBattle();
        mapCamera.FocusOn(isEnemyCastle ? _enemyCastle.GetCameraTrm() : _unitCastle.GetCameraTrm());
    }

    public int GetMoney() => (int)_money;

    public void CompleteBattle()
    {
        enemySpawner.ReleaseSpawner();
    }

    private void OnDestroy()
    {
        CompleteBattle();
    }
}
