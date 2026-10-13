using UnityEngine;

public class PVPUnitRenderer
{
    public int LastSnapshotId { get; set; }
    private GameObject _visual;
    private Animator _animator;
    private Vector3 _targetPosition;
    private EStateType _currentState;

    public PVPUnitRenderer(RuntimeUnitData unitData, UnitSO unitSO, Transform parent, bool isOpponent)
    {
        _visual = new GameObject(unitSO.UnitName);
        _visual.transform.SetParent(parent, false);
        _visual.transform.localScale = new Vector3(unitData.IsOpponent != isOpponent ? -1f : 1f, 1f, 1f);

        var spriteRenderer = _visual.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Unit";
        spriteRenderer.sprite = unitSO.UnitIcon;

        _animator = _visual.AddComponent<Animator>();
        _animator.runtimeAnimatorController = unitSO.animator;
        _animator.fireEvents = false;
        _currentState = (EStateType)(-1);
        UpdateUnit(unitData, isOpponent);
        _visual.transform.position = _targetPosition;
    }

    public void UpdateUnit(RuntimeUnitData unitData, bool isOpponent)
    {
        _targetPosition = new Vector3(isOpponent ? -unitData.PositionX : unitData.PositionX, unitData.PositionY, 0f);

        if(_currentState == unitData.State)
            return;

        _currentState = unitData.State;
        _animator.SetBool("IDLE", _currentState == EStateType.Idle);
        _animator.SetBool("MOVE", _currentState == EStateType.Move);
        _animator.SetBool("ATTACK", _currentState == EStateType.Attack);
        _animator.SetBool("KNOCKBACK", _currentState == EStateType.Knockback || _currentState == EStateType.Hitback);
    }

    public void Update(float dt)
    {
        _visual.transform.position = Vector3.Lerp(_visual.transform.position, _targetPosition, Mathf.Min(1f, dt * 12f));
    }

    public void Release()
    {
        Object.Destroy(_visual);
    }
}