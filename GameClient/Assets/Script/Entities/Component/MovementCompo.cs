using DG.Tweening;
using MINLib.Script.Core.Resource;
using System;
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
        if (_healthCompo != null)
        {
            _healthCompo.OnHitback -= PlayKnockback;
        }

        _owner = owner;
        _ownerStat = owner.Get<StatCompo>();
        _healthCompo = owner.Get<HealthCompo>();

        _direction = _owner.TeamType == ETeamType.Friendly ? Vector3.left : Vector3.right;
        _canMove = true;
        _healthCompo.OnHitback += PlayKnockback;
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

    public void PlayKnockback()
    {
        _owner.ChangeState(EStateType.Knockback);

        var unit = _owner.transform;
        var pos = unit.position;

        DOTween.Sequence()
            .Append(unit.DOJump(pos - _direction * 0.6f, 0.5f, 1, 0.4f))
            .Append(unit.DOJump(pos - _direction * 0.9f, 0.25f, 1, 0.25f))
            .Append(unit.DOJump(pos - _direction * 1.05f, 0.1f, 1, 0.2f))

            .OnComplete(() =>
            {
                if (_healthCompo.IsDead == false)
                    return;

                ResourceManager.I.DespawnPool(_owner.gameObject);
            });
    }

    private void OnDestroy()
    {
        if (_healthCompo != null)
            _healthCompo.OnHitback -= PlayKnockback;
    }
}
