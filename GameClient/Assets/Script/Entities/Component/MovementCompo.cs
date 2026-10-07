using UnityEngine;

public class MovementCompo : MonoBehaviour, IComponentCache
{
    private StatCompo _ownerStat;
    private Unit _owner;
    private Vector3 _direction;
    private bool _canMove = true;

    public void InitializeComponent(Unit owner)
    {
        _owner = owner;
        _ownerStat = owner.Get<StatCompo>();
        _direction = _owner.TeamType == ETeamType.Friendly ? Vector3.left : Vector3.right;
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
}
