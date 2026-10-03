using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SnapScrollViewBase<T> : UIBase, IBeginDragHandler, IEndDragHandler 
    where T : UIBase
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;

    protected List<T> _elements = new();
    private T _currentElement;
    private bool _isDragging;
    private bool _isSnapping;
    private float _snapVelocity;

    private const float _snapDeceleration = 5000f;
    private const float _minSnapVelocity = 100f;

    public event Action<T> OnElementChanged;

    private void Update()
    {
        CheckElementChanged();

        if(_isDragging == false && _isSnapping)
        {
            _snapVelocity = scrollRect.velocity.x;
            _snapVelocity = Mathf.MoveTowards(_snapVelocity, 0f, _snapDeceleration * Time.deltaTime);
            content.anchoredPosition += Vector2.right * (_snapVelocity * Time.deltaTime);

            if (Mathf.Abs(_snapVelocity) <= _minSnapVelocity)
            {
                _snapVelocity = 0f;
                _isSnapping = false;
                SnapToElement(_currentElement);
            }
        }
    }

    private void CheckElementChanged()
    {
        T centerElement = GetCenterElement();

        if (centerElement == null || centerElement == _currentElement)
        {
            return;
        }

        _currentElement = centerElement;
        OnElementChanged?.Invoke(centerElement);
        ElementChanged();
    }

    private T GetCenterElement()
    {
        RectTransform viewport = scrollRect.viewport ?? scrollRect.GetComponent<RectTransform>();
        Vector3 viewportCenter = viewport.TransformPoint(viewport.rect.center);

        T closest = null;
        float minDistance = float.MaxValue;

        foreach (T element in _elements)
        {
            RectTransform rect = element.Rect;
            Vector3 elementCenter = rect.TransformPoint(rect.rect.center);
            float distance = Mathf.Abs(elementCenter.x - viewportCenter.x);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = element;
            }
        }

        return closest;
    }

    public void SnapToElement(T element)
    {
        scrollRect.StopMovement();
        Vector3 viewportCenter = scrollRect.viewport.TransformPoint(scrollRect.viewport.rect.center);
        Vector3 elementCenter = element.Rect.TransformPoint(element.Rect.rect.center);
        float targetX = content.anchoredPosition.x + viewportCenter.x - elementCenter.x;
        content.DOAnchorPosX(targetX, 0.4f).SetEase(Ease.OutCubic);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _isSnapping = true;
        content.DOKill();
    }

    protected abstract void ElementChanged();
}

