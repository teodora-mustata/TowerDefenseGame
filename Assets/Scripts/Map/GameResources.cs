using UnityEngine;
using TMPro;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;
    public int coins = 200;
    public int CurrentCoins => coins;

    public TMP_Text coinsText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount)
            return false;

        coins -= amount;
        UpdateUI();
        return true;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinsText.text = coins.ToString();
    }
}
