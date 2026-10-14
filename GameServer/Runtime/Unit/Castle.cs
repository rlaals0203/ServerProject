using System.Numerics;

public class Castle : RuntimeUnit
{
    public Castle(string runtimeId, bool isOpponent, RuntimeBattleField battleField, Vector3 position, ELayer layer)
        : base(runtimeId, null, isOpponent, battleField, position, layer)
    {
        
    }

    public void InitCastle(float health)
    {
        Get<StatComponent>().InitializeHealthStat(health);
        Get<HealthComponent>().InitializeComponent();
    }

    public override void Update(float dt) { }
}