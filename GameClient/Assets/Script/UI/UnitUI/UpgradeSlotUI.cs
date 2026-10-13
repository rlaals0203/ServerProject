using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlotUI : UIBase
{
    [SerializeField] private Image purchaseEffect;

    public void PlayPurchseEffect()
    {
        purchaseEffect.DOKill();
        purchaseEffect.DOFade(1f, 0f);
        DOVirtual.DelayedCall(0.1f, () =>
        {
            purchaseEffect.DOFade(0f, 0.8f);
        });
    }
}