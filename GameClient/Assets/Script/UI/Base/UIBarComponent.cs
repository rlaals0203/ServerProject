using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBarComponent : UIBase
{
    [SerializeField] protected Image fill;
    [SerializeField] protected Image trailFill;
    [SerializeField] protected TextMeshProUGUI amountText;

    private Tween _trailTween;


    public virtual void SetBar(float current, float max, Action<float> callback = null)
    {
        float target = current / max;

        if (amountText != null)
            amountText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";

        fill.transform.localScale = new Vector3(target, 1, 1);

        if(trailFill != null)
        {
            _trailTween?.Kill();
            _trailTween = DOVirtual.DelayedCall(0.2f, () =>
            {
                trailFill.transform.DOScaleX(target, 0.4f).SetUpdate(true);
            });
        }
    }
}