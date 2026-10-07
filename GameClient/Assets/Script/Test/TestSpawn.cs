using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private UnitSO spawnUnit;
    [SerializeField] private UnitSO testEnemy;

    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            var spawned = new SpawnCacheObject<Unit>("Unit", spawnPosition.position).Spawned;
            spawned.Initialize(new MakeUnitData(spawnUnit).UnitData, ETeamType.Friendly);
        }

        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            var enemy = new SpawnCacheObject<Unit>("Unit", enemyPosition.position).Spawned;
            enemy.Initialize(new MakeUnitData(testEnemy).UnitData, ETeamType.Enemy);
        }
    }
}
