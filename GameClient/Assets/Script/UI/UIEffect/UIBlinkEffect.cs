using System.Collections;
using UnityEngine;

public class UIBlinkEffect : MonoBehaviour
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

    public void PlayBlinkEffect()
    {
        if (_hoverCoroutine != null)
        {
            return;
        }

        _hoverCoroutine = StartCoroutine(HoverAnimation());
    }

    public void StopBlinkEffect()
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
            yellowOutline.SetActive(true);
            pinkOutline.SetActive(false);
            yield return _waitForSeconds;

            yellowOutline.SetActive(false);
            pinkOutline.SetActive(true);
            yield return _waitForSeconds;
        }
    }
}