using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform rightDoor;
    [SerializeField] private RectTransform leftDoor;
    [SerializeField] private ContentOpenButton[] contentButtons;

    private Stack<ContentUI> _openedContents = new();
    private Sequence _doorSequence;
    private Sequence _contentButtonSequence;
    private bool _isDuringAnim;

    private bool IsContentOpen => _openedContents.Count > 0;

    private void Awake()
    {
        foreach(var content in contentButtons)
        {
            content.OnContentOpened += HandleContentOpen;
        }
    }

    private void HandleContentOpen(ContentUI ui)
    {
        if (ui == null || _isDuringAnim)
        {
            return;
        }

        if(IsContentOpen)
        {
            ToggleDoor(false, () => OpenContent(ui));
            return;
        }

        OpenContent(ui);
    }

    private void OpenContent(ContentUI ui)
    {
        ToggleDoor(true);
        _openedContents.Push(ui);
        ui.Initialize();
        ui.gameObject.SetActive(true);

        RefreshContentSiblingIndex();
    }

    private void CloseContent()
    {
        if(_isDuringAnim)
        {
            return;
        }

        ContentUI ui = _openedContents.Pop();
        ui.gameObject?.SetActive(false);
        RefreshContentSiblingIndex();
    }

    public void OnTouchCloseContent()
    {
        if (!IsContentOpen)
        {
            SceneManager.LoadSceneAsync(SceneDefine.TITLE);
        }

        ToggleDoor(false, CloseContent);
    }

    private void RefreshContentSiblingIndex()
    {
        int order = 0;
        foreach(var content in _openedContents)
        {
            content.transform.SetSiblingIndex(order++);
        }
    }
   
    private void ToggleDoor(bool isOpen, Action onComplete = null)
    {
        _isDuringAnim = true;

        if (isOpen && IsContentOpen == false)
        {
            ToggleButtons(true);
        }

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
            .Join(leftDoor.DOAnchorPosX(-endXPos, 0.6f).SetEase(ease))
            .OnComplete(() =>
            {
                if (isOpen == false && _openedContents.Count == 1)
                {
                    ToggleButtons(false);
                }

                _isDuringAnim = false;
                onComplete?.Invoke();
            });
    }

    private void ToggleButtons(bool isOpen)
    {
        _contentButtonSequence?.Kill();
        _contentButtonSequence = DOTween.Sequence();
        float endX = isOpen ? -500f : 300f;

        for (int i = 0; i < contentButtons.Length; i++)
        {
            int index = isOpen ? contentButtons.Length - 1 - i : i;
            RectTransform rect = contentButtons[index].Rect;
            _contentButtonSequence.Insert(i * 0.04f, rect.DOAnchorPosX(endX, 0.5f)).SetEase(Ease.InOutQuad);
        }
    }
}