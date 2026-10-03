using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rightDoor;
    [SerializeField] private RectTransform leftDoor;

    [SerializeField] private Button[] options;

    [SerializeField] private GameObject pveMap; 
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject deployUI;

    private Sequence _doorSequence;
    private bool _isDoorOpen = false;

    private void Awake()
    {
        _doorSequence = DOTween.Sequence();
    }

    public void OnTouchPVE()
    {
        ControlDoor(!_isDoorOpen);
    }

    public void OnTouchUpgrade()
    {
        ControlDoor(!_isDoorOpen);
    }

    public void OnTouchDeploy()
    {
        ControlDoor(!_isDoorOpen);
    }

    public void OnTouchPVP()
    {
        ControlDoor(!_isDoorOpen);
    }

    private void ControlDoor(bool isOpen)
    {
        _isDoorOpen = isOpen;

        _doorSequence?.Kill();
        _doorSequence = DOTween.Sequence();

        float startXPos = isOpen ? 960 : 1920;
        float endXPos = isOpen ? 1920 : 960;
        float interval = isOpen ? 0.5f : 0f;
        Ease ease = isOpen ? Ease.InExpo : Ease.OutExpo;

        rightDoor.anchoredPosition = new Vector2(startXPos, rightDoor.anchoredPosition.y);
        leftDoor.anchoredPosition = new Vector2(-startXPos, leftDoor.anchoredPosition.y);
        _doorSequence
            .AppendInterval(interval)
            .Append(rightDoor.DOAnchorPosX(endXPos, 0.6f).SetEase(ease))
            .Join(leftDoor.DOAnchorPosX(-endXPos, 0.6f).SetEase(ease));
    }
}