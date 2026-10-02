using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : UIBase
{
	[SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;

	private List<RectTransform> _unitUpgradeSlots;

    protected override void Awake()
    {
        var upgradeSlots = GetComponentsInChildren<UnitSlotUI>();
        foreach(var slot in upgradeSlots)
        {
            _unitUpgradeSlots.Add(slot.Rect);
        }
    }

    private void InitializeUI()
    {

    }

    private void SetScaleNearElement()
    {

    }

    private void OnCurrentSlotChanged()
    {

    }

    public RectTransform GetCenterElement()
    {
        RectTransform content = scrollRect.content;
        RectTransform viewport = scrollRect.viewport ?? scrollRect.GetComponent<RectTransform>();

        Vector3 viewportCenter = viewport.TransformPoint(viewport.rect.center);

        RectTransform closest = null;
        float minDistance = float.MaxValue;

        foreach (RectTransform child in content)
        {
            Vector3 childCenter = child.TransformPoint(child.rect.center);
            float distance = Mathf.Abs(childCenter.x - viewportCenter.x);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = child;
            }
        }

        return closest;
    }
}