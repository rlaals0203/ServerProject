using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoveringAnimationComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject yellowOutline;
    [SerializeField] private GameObject pinkOutline;
    [SerializeField] private float blinkDuration = 0.05f;

    private Coroutine _hoverCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(blinkDuration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverCoroutine = StartCoroutine(HoverAnimation());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_hoverCoroutine != null)
        {
            StopCoroutine(_hoverCoroutine);
            _hoverCoroutine = null;
        }

        yellowOutline.gameObject.SetActive(false);
        pinkOutline.gameObject.SetActive(false);
    }

    private IEnumerator HoverAnimation()
    {
        while (true)
        {
            yellowOutline.gameObject.SetActive(true);
            pinkOutline.gameObject.SetActive(false);
            yield return _waitForSeconds;

            yellowOutline.gameObject.SetActive(false);
            pinkOutline.gameObject.SetActive(true);
            yield return _waitForSeconds;
        }
    }
}