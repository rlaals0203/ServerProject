using DG.Tweening;
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

    public void PlayWinAnimation(int exp)
    {
        expDropText.text = exp.ToString();
        StartCoroutine(ExcuteWinAnimation());
    }

    public void PlayLoseAnimation()
    {
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
        yield return new WaitForSeconds(1.5f);
        returnButton.gameObject.SetActive(true);
    }

    private IEnumerator ExcuteLoseAnimation()
    {
        bottomUI.DOAnchorPosY(-200, 1f);
        topUI.DOAnchorPosY(200, 1f);
        loseTitle.DOAnchorPosY(0f, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.8f);
        fadeImage.DOFade(1f, 3f);
        yield return new WaitForSeconds(3f);
        returnButton.gameObject.SetActive(true);
    }

    public void OnTouchReturn()
    {
        SceneManager.LoadSceneAsync(SceneDefine.MAIN_MENU);
    }
}