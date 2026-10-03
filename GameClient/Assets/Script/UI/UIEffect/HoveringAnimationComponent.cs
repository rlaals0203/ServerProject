using UnityEngine;
using UnityEngine.EventSystems;

public class HoveringAnimationComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIBlinkEffect uiBlinkEffect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiBlinkEffect.PlayBlinkEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiBlinkEffect.StopBlinkEffect();
    }
}