using System.Numerics;

public class MovementComponent : ComponentBase
{
    private RuntimeUnit _owner;
    private StatComponent _statCompo;
    private HealthComponent _healthCompo;
    private StatBase _movementStat;

    private Vector3 _direction;
    private bool _canMove = true;
    private float _knockbackTime;
    private float _knockbackMoved;
    private float _knockbackDuration;
    private float _knockbackDistance;
    private const float HitbackTime = 20f / 30f;
    private const float KnockbackTime = 12f / 30f;
    private const float HitbackDistance = 1.1f;
    private const float KnockbackDistance = 0.55f;
    public int HitbackCount { get; private set; }

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
        _healthCompo.OnHitback += PlayHitback;
    }

    public void MoveUnit(float dt)
    {
        if (_canMove == false)
            return;

        _owner.Position += _movementStat.CurrentStat * _direction * dt;
    }

    public bool UpdateKnockback(float dt)
    {
        _knockbackTime = MathF.Min(_knockbackTime + dt, _knockbackDuration);
        float progress = _knockbackTime / _knockbackDuration;
        float distance = _knockbackDistance * progress;

        if (_owner.CurrentState == EStateType.Hitback)
        {
            float remaining = 1f - progress;
            distance = _knockbackDistance * (1f - remaining * remaining * remaining);
        }

        _owner.Position -= _direction * (distance - _knockbackMoved);
        _knockbackMoved = distance;
        return _knockbackTime >= _knockbackDuration;
    }

    private void PlayHitback()
    {
        HitbackCount++;
        StartKnockback(EStateType.Hitback, HitbackTime, HitbackDistance);
    }

    public void PlayKnockback()
    {
        StartKnockback(EStateType.Knockback, KnockbackTime, KnockbackDistance);
    }

    private void StartKnockback(EStateType stateType, float duration, float distance)
    {
        if (_owner.IsDead)
            return;

        _knockbackTime = 0f;
        _knockbackMoved = 0f;
        _knockbackDuration = duration;
        _knockbackDistance = distance;
        _owner.ChangeState(stateType);
    }

    public void SetMove(bool canMove)
    {
        _canMove = canMove;
    }

    public override void ReleaseComponent()
    {
        base.ReleaseComponent();
        _healthCompo.OnHitback -= PlayHitback;
    }
}