using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetUnit;
    [SerializeField] private GameObject[] redDots;

    [SerializeField] private float moveSpeed = 50f;

    private Queue<Vector3> _destinationQueue = new();
    private Coroutine _moveCoroutine;

    public void ChangeStage(int stageId)
    {
        if (stageId < 0 || stageId >= redDots.Length)
            return;

        Vector3 destination = redDots[stageId].transform.position;
        target.transform.position = destination;
        _destinationQueue.Enqueue(destination);
    }

    private void Update()
    {
        float remainingDistance = moveSpeed * Time.deltaTime;

        while (_destinationQueue.Count > 0)
        {
            Vector3 destination = _destinationQueue.Peek();
            Vector3 current = targetUnit.transform.position;
            float distance = Vector3.Distance(current, destination);

            if (distance > remainingDistance)
            {
                targetUnit.transform.position = Vector3.MoveTowards(current, destination, remainingDistance);
                break;
            }

            targetUnit.transform.position = destination;
            _destinationQueue.Dequeue();
            remainingDistance -= distance;
        }
    }
}