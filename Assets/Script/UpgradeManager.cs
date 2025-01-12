using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    #region Scripts
    private GameManager gameManager;
    private CoastManager coastManager;
    private BoatManager boatManager;
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

    public void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        coastManager = gameObject.GetComponent<CoastManager>();
        boatManager = gameObject.GetComponent<BoatManager>();

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
        boatManager.refBoatMaxHealth += (gameManager.playerLevel + 1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
        boatManager.refBoatDamage += (gameManager.playerLevel + 1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
        boatManager.refBoatValue += (gameManager.playerLevel + 1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
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
        gameManager.playerClicDamage += (gameManager.playerLevel+1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
        coastManager.coastButtonDamageUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }

    public void ButonSellUp()
    {
        if (gameManager.levelPlayerSell == gameManager.levelLimitUpgrade)
        {
            sellUpGameObject.SetActive(false);
        }
        gameManager.levelPlayerSell++;
        gameManager.playerClicSell += (gameManager.playerLevel+1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
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
        gameManager.playerHealth += (gameManager.playerLevel +1) * coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
        gameManager.playerGoldScore -= coastManager.coastButtonHealthUp;
        coastManager.coastButtonHealthUp += coastManager.coastDictionaire.GetValueOrDefault(gameManager.playerTitleIndex);
    }
}
