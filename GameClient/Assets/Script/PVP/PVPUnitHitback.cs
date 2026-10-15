using System;
using UnityEngine;

public class PVPUnitHitback
{
    private PVPUnit _owner;
    private Transform _unit;
    private Transform _visual;
    private float _currentTime;
    private int _hitbackCount;
    private Vector3 _deathStartPosition;
    private Action<PVPUnit> _onDeathComplete;
    private const float HitbackDuration = 20f / 30f;
    private const float HitbackDistance = 1.1f;
    private const float FirstBounceTime = 0.32f;
    private const float SecondBounceTime = 0.21f;
    private const float ThirdBounceTime = HitbackDuration - FirstBounceTime - SecondBounceTime;
    public bool IsDead { get; private set; }

    public PVPUnitHitback(PVPUnit owner, Transform visual)
    {
        _owner = owner;
        _unit = owner.transform;
        _visual = visual;
    }

    public void Initialize(int hitbackCount)
    {
        _hitbackCount = 0;
        _currentTime = HitbackDuration;
        IsDead = false;
        _onDeathComplete = null;
        _visual.localPosition = Vector3.zero;
        UpdateHitback(hitbackCount);
    }

    public void UpdateHitback(int hitbackCount)
    {
        if (_hitbackCount == hitbackCount)
            return;

        _hitbackCount = hitbackCount;
        PlayHitback();
    }

    private void PlayHitback()
    {
        _currentTime = 0f;
        _visual.localPosition = Vector3.zero;
    }

    public void PlayDeath(Action<PVPUnit> onComplete)
    {
        IsDead = true;
        PlayHitback();
        _deathStartPosition = _unit.position;
        _onDeathComplete = onComplete;
    }

    public void Update(float dt, bool isFreeze)
    {
        if (IsDead)
        {
            _currentTime = Mathf.Min(_currentTime + dt, HitbackDuration);
            float progress = _currentTime / HitbackDuration;
            float remaining = 1f - progress;
            float distance = HitbackDistance * (1f - remaining * remaining * remaining);
            _unit.position = _deathStartPosition + Vector3.right * (_unit.localScale.x * distance);
            _visual.localPosition = Vector3.up * GetBounceHeight(_currentTime);

            if (_currentTime >= HitbackDuration && _onDeathComplete != null)
            {
                Action<PVPUnit> onComplete = _onDeathComplete;
                _onDeathComplete = null;
                onComplete(_owner);
            }
            return;
        }

        if (_currentTime >= HitbackDuration || isFreeze)
            return;

        _currentTime = Mathf.Min(_currentTime + dt, HitbackDuration);
        _visual.localPosition = Vector3.up * GetBounceHeight(_currentTime);
    }

    private float GetBounceHeight(float time)
    {
        if (time >= HitbackDuration)
            return 0f;
        if (time < FirstBounceTime)
            return BounceUtility.GetHeight(time / FirstBounceTime, 0.4f);
        if (time < FirstBounceTime + SecondBounceTime)
            return BounceUtility.GetHeight((time - FirstBounceTime) / SecondBounceTime, 0.25f);

        return BounceUtility.GetHeight((time - FirstBounceTime - SecondBounceTime) / ThirdBounceTime, 0.1f);
    }

    public void Release()
    {
        _onDeathComplete = null;
        IsDead = false;
        _visual.localPosition = Vector3.zero;
    }
}
