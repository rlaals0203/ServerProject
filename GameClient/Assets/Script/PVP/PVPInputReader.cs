using UnityEngine;

public class PVPInputReader : MonoBehaviour
{
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

    private void Start()
    {
        unitBattleDeckUI.InitializePvpDeck(HandleSpawnUnit);
    }

    private async void HandleSpawnUnit(UnitData unitData)
    {
        await SignalRClient.I.SendMessageToServer("RequestSpawnUnit", unitData.UnitStatData.UnitId);
    }
}