using Cinemachine;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;

    public void FocusOn(Transform target)
    {
        cinemachineCam.Follow = target;
        cinemachineCam.m_Lens.OrthographicSize = 3f;
    }
}