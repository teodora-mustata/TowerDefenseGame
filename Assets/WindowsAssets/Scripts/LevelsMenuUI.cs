using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsMenuUI : MonoBehaviour
{
    public Button[] levelButtons;

    public Sprite unlockedNormal;
    public Sprite unlockedHover;
    public Sprite unlockedPressed;

    public Sprite lockedNormal;
    public Sprite lockedHover;
    public Sprite lockedPressed;

    private void Start()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = i < unlocked;
            levelButtons[i].interactable = isUnlocked;

            var spriteState = new SpriteState();

            if (isUnlocked)
            {
                levelButtons[i].image.sprite = unlockedNormal;
                spriteState.highlightedSprite = unlockedHover;
                spriteState.pressedSprite = unlockedPressed;
            }
            else
            {
                levelButtons[i].image.sprite = lockedNormal;
                spriteState.highlightedSprite = lockedHover;
                spriteState.pressedSprite = lockedPressed;
            }

            levelButtons[i].spriteState = spriteState;
        }
    }

    public void OpenLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
