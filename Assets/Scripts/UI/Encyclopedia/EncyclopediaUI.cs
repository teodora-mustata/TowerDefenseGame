using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EncyclopediaUI : MonoBehaviour
{
    [Header("Header")]
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    [Header("Panels")]
    public GameObject towerStatsPanel;
    public GameObject enemyStatsPanel;

    [Header("Tower Values")]
    public TMP_Text towerDamageValue;
    public TMP_Text towerFireRateValue;
    public TMP_Text towerRangeValue;
    public TMP_Text towerDamageTypeValue;
    public TMP_Text towerHealthValue;

    [Header("Enemy Values")]
    public TMP_Text enemyHealthValue;
    public TMP_Text enemySpeedValue;
    public TMP_Text enemyDamageValue;
    public TMP_Text enemyTypeValue;
    public TMP_Text enemyCoinRewardValue;

    public void ShowEntry(EncyclopediaEntry entry)
    {
        nameText.text = entry.entryName;
        iconImage.sprite = entry.icon;
        descriptionText.text = entry.description;

        towerStatsPanel.SetActive(false);
        enemyStatsPanel.SetActive(false);

        BaseTower tower = entry.prefab.GetComponent<BaseTower>();
        if (tower != null)
        {
            towerStatsPanel.SetActive(true);

            towerDamageValue.text = tower.damage.ToString();
            towerFireRateValue.text = tower.fireRate.ToString("0.00");
            towerRangeValue.text = tower.range.ToString();
            towerDamageTypeValue.text = tower.damageType;
            towerHealthValue.text = tower.maxHealth.ToString();

            return;
        }

        BaseEnemy enemy = entry.prefab.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemyStatsPanel.SetActive(true);

            enemyHealthValue.text = enemy.health.ToString();
            enemySpeedValue.text = enemy.speed.ToString("0.00");
            enemyDamageValue.text = enemy.damageToTower.ToString("0");
            enemyTypeValue.text = enemy.type;
            enemyCoinRewardValue.text = enemy.coinReward.ToString();
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
