using DG.Tweening;
using UnityEngine;

public class FreezeUI: MonoBehaviour
{
    [SerializeField] private GameObject hour;
    [SerializeField] private GameObject minute;

    private Sequence _freezeSeq;

    public void PlayAnim()
    {
        _freezeSeq?.Kill();
        hour.transform.localRotation = Quaternion.identity;
        minute.transform.localRotation = Quaternion.identity;
        _freezeSeq = DOTween.Sequence();
        _freezeSeq
            .Append(hour.transform.DORotate(new Vector3(0, 0, 360), 1.5f, RotateMode.FastBeyond360))
            .SetEase(Ease.OutExpo)
            .Join(minute.transform.DORotate(new Vector3(0, 0, 1080), 1.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutExpo));

        _freezeSeq.Play();
    }
}