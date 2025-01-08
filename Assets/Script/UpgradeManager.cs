using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UpgradeManager : MonoBehaviour
{
    #region Scripts
    [HideInInspector] private GameManager gameManager;
    [HideInInspector] private CoastManager coastManager;
    #endregion
    
    #region GameObjects
    [Header("Buttons :")]
    [SerializeField] private GameObject levelUpGameObject;
    [SerializeField] private GameObject titleUpGameObject;
    [SerializeField] private GameObject damageUpGameObject;
    [SerializeField] private GameObject sellUpGameObject;
    [SerializeField] private GameObject healthUpGameObject;
    #endregion

    #region Buttons
    [HideInInspector] public Button levelUpButton;
    [HideInInspector] public Button titleUpButton;
    [HideInInspector] public Button damageUpButton;
    [HideInInspector] public Button sellUpButton;
    [HideInInspector] public Button healthUpButton;
    #endregion

    public void StartUpgradeManager()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        coastManager = gameObject.GetComponent<CoastManager>();

        levelUpButton = levelUpGameObject.GetComponent<Button>();
        titleUpButton = titleUpGameObject.GetComponent<Button>();
        damageUpButton = damageUpGameObject.GetComponent<Button>();
        sellUpButton = sellUpGameObject.GetComponent<Button>();
        healthUpButton = healthUpGameObject.GetComponent<Button>();
    }

    public void ButonLevelUp()
    {
        if (gameManager.playerLevel == 9 && gameManager.playerTitleIndex < 12)
        {
            titleUpGameObject.SetActive(true);
            levelUpGameObject.SetActive(false);
        }
        gameManager.playerLevel += 1;
        gameManager.playerGoldScore -= coastManager.coastButtonLevelUp;
        coastManager.coastButtonLevelUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }

    public void ButtonTitleUp()
    {
        if (gameManager.playerTitleIndex < 12)
        {
            gameManager.playerGoldScore -= coastManager.coastButtonTitleUp;
            coastManager.coastButtonTitleUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex) * 10;
            gameManager.playerTitleIndex += 1;
            gameManager.playerLevel = 0;
            gameManager.levelLimitUpgrade += 10;
            gameManager.levelShop++;
            titleUpGameObject.SetActive(false);
            levelUpGameObject.SetActive(true);
            damageUpGameObject.SetActive(true);
            sellUpGameObject.SetActive(true);
            healthUpGameObject.SetActive(true);
        }
    }

    public void ButonDamageUp()
    {
        if (gameManager.levelPlayerDamage == gameManager.levelLimitUpgrade)
        {
            damageUpGameObject.SetActive(false);
        }
        gameManager.levelPlayerDamage++;
        gameManager.playerGoldScore -= coastManager.coastButtonDamageUp;
        gameManager.playerClicDamage += 10 * (gameManager.playerTitleIndex + 1);
        coastManager.coastButtonDamageUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }

    public void ButonSellUp()
    {
        if (gameManager.levelPlayerSell == gameManager.levelLimitUpgrade)
        {
            sellUpGameObject.SetActive(false);
        }
        gameManager.levelPlayerSell++;
        gameManager.playerClicSell += ((gameManager.playerClicSell * 50) / 100);
        gameManager.playerGoldScore -= coastManager.coastButtonSellUp;
        coastManager.coastButtonSellUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }

    public void ButonHealthUp()
    {
        if (gameManager.levelPlayerHealth == gameManager.levelLimitUpgrade)
        {
            healthUpGameObject.SetActive(false);
        }
        gameManager.levelPlayerHealth++;
        gameManager.playerHealth += ((gameManager.playerHealth * 50) / 100);
        gameManager.playerGoldScore -= coastManager.coastButtonHealthUp;
        coastManager.coastButtonHealthUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }
}
