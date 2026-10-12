using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCompleteAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform reward;
    [SerializeField] private RectTransform winTitle;
    [SerializeField] private RectTransform returnButton;

    public void PlayAnimation()
    {
        StartCoroutine(ExcuteAnimation());
    }

    private IEnumerator ExcuteAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        winTitle.DOAnchorPosX(0, 0.25f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.8f);
        reward.DOAnchorPosX(0, 0.25f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(1.5f);
        returnButton.gameObject.SetActive(true);
    }

    public void OnTouchReturn()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}