using TMPro;
using UnityEngine;

public class CastleHealthComponent : HealthCompo
{
    [SerializeField] private TMP_Text unitHealthText;
    [SerializeField] private TMP_Text enemyHealthText;
    private Castle _castle;
    private TMP_Text _healthText;

    public override void AfterInitialize()
    {
        base.AfterInitialize();

        unitHealthText.gameObject.SetActive(false);
        enemyHealthText.gameObject.SetActive(false);

        _castle = _owner as Castle;
        if (_owner.IsEnemyUnit)
        {
            enemyHealthText.gameObject.SetActive(true);
            _healthText = enemyHealthText;
        }
        else
        {
            unitHealthText.gameObject.SetActive(true);
            _healthText = unitHealthText;
        }

        RefreshHealthText();
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        if(_healthStat.CurrentStat <= 0)
        {
            _castle.OnDead?.Invoke(_owner.IsEnemyUnit);
        }

        RefreshHealthText();
    }

    private void RefreshHealthText()
    {
        if (_healthText == null)
            return;

        _healthText.text = $"{Mathf.Clamp(_healthStat.CurrentStat, 0, float.MaxValue)}/{_healthStat.MaxStat}";
    }
}