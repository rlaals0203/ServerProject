using UnityEngine;

public class MapMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetUnit;
    [SerializeField] private Transform redDotParent;

    private GameObject[] _redDots;

    private void Awake()
    {
        _redDots = GetComponentsInChildren<GameObject>();
    }

    public void ChangeStage(int stageId)
    {
        if(_redDots.Length >= stageId || _redDots.Length < 0)
        {
            return;
        }

        target.transform.position = _redDots[stageId].transform.position;
    }
}