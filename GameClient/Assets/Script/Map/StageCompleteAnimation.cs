using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageCompleteAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text expDropText;
    [SerializeField] private RectTransform reward;
    [SerializeField] private RectTransform winTitle;
    [SerializeField] private RectTransform loseTitle;
    [SerializeField] private RectTransform returnButton;
    [SerializeField] private RectTransform bottomUI;
    [SerializeField] private RectTransform topUI;
    [SerializeField] private Image fadeImage;

    private Action _onTouchReturn;

    public void PlayWinAnimation(int exp, Action onTouchReturn = null)
    {
        _onTouchReturn = onTouchReturn;
        expDropText.text = exp.ToString();
        StartCoroutine(ExcuteWinAnimation());
    }

    public void PlayLoseAnimation(Action onTouchReturn = null)
    {
        _onTouchReturn = onTouchReturn;
        StartCoroutine(ExcuteLoseAnimation());
    }

    private IEnumerator ExcuteWinAnimation()
    {
        bottomUI.DOAnchorPosY(-200, 1f);
        topUI.DOAnchorPosY(200, 1f);
        yield return new WaitForSeconds(0.8f);
        winTitle.DOAnchorPosX(0, 0.25f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.8f);
        reward.DOAnchorPosX(0, 0.25f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(1.6f);
        returnButton.gameObject.SetActive(true);
    }

    private IEnumerator ExcuteLoseAnimation()
    {
        bottomUI.DOAnchorPosY(-200, 1f);
        topUI.DOAnchorPosY(200, 1f);
        loseTitle.DOAnchorPosY(0f, 2.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.8f);
        fadeImage.DOFade(1f, 3f);
        yield return new WaitForSeconds(4.5f);
        returnButton.gameObject.SetActive(true);
    }

    public void OnTouchReturn()
    {
        _onTouchReturn?.Invoke();
        _onTouchReturn = null;
        SceneManager.LoadSceneAsync(SceneDefine.MAIN_MENU);
    }
}