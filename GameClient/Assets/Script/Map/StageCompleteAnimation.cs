using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCompleteAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text expDropText;
    [SerializeField] private RectTransform reward;
    [SerializeField] private RectTransform winTitle;
    [SerializeField] private RectTransform returnButton;
    [SerializeField] private RectTransform bottomUI;
    [SerializeField] private RectTransform topUI;

    public void PlayAnimation(int exp)
    {
        expDropText.text = exp.ToString();
        StartCoroutine(ExcuteAnimation());
    }

    private IEnumerator ExcuteAnimation()
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

    public void OnTouchReturn()
    {
        SceneManager.LoadSceneAsync(SceneDefine.MAIN_MENU);
    }
}