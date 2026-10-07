using DG.Tweening;
using UnityEngine;

public class MovementCompo : MonoBehaviour, IComponentCache
{
    private StatCompo _ownerStat;
    private HealthCompo _healthCompo;
    private Unit _owner;

    private Vector3 _direction;
    private bool _canMove = true;
    private const float KnockbackPower = -0.8f;

    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _ownerStat = owner.Get<StatCompo>();
        _healthCompo = owner.Get<HealthCompo>();

        _direction = _owner.TeamType == ETeamType.Friendly ? Vector3.left : Vector3.right;
        _healthCompo.OnHitback += PlayHitback;
    }

    public void MovingUnit()
    {
        if (_canMove == false)
            return;

       _owner.transform.position += _ownerStat.GetStat(EStatType.MoveSpeed).CurrentStat * _direction * Time.deltaTime;
    }

    public void SetMove(bool canMove)
    {
        _canMove = canMove;
    }

    public void PlayHitback()
    {
        Vector3 destination = transform.position + _direction * KnockbackPower;
        _owner.transform.DOMove(destination, 0.8f).SetEase(Ease.OutQuart);
    }

    private void OnDestroy()
    {
        if (_healthCompo != null)
            _healthCompo.OnHitback -= PlayHitback;
    }
}
