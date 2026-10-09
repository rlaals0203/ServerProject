using System.Numerics;

public class MovementComponent : ComponentBase
{
    private RuntimeUnit _owner;
    private StatComponent _statCompo;
    private HealthComponent _healthCompo;
    private StatBase _movementStat;

    private Vector3 _direction;
    private bool _canMove = true;
    private const float KnockbackPower = -0.8f;


    public MovementComponent(RuntimeUnit owner) : base(owner)
    {
        _owner = owner;
        _direction = _owner.IsOpponent ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
    }

    public override void InitializeComponent()
    {
        base.InitializeComponent();

        _statCompo = _owner.Get<StatComponent>();
        _healthCompo = _owner.Get<HealthComponent>();
        _movementStat = _statCompo.GetStat(EStatType.MoveSpeed);
    }

    public override void UpdateComponent(float dt)
    {
        base.UpdateComponent(dt);

        if (_canMove == false)
            return;

       _owner.Position += _movementStat.CurrentStat * _direction * dt;
    }

    public void PlayKnockback()
    {

    }

    public void SetMove(bool canMove)
    {
        _canMove = canMove;
    }
}