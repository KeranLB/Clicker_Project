using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Scripts
    private GameManager gameManager;
    private CoastManager coastManager;
    private BoatManager boatManager;
    private AutoClicManager autoClicManager;
    #endregion
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        coastManager = gameObject.GetComponent<CoastManager>();
        boatManager = gameObject.GetComponent<BoatManager>();
        autoClicManager = gameObject.GetComponent<AutoClicManager>();
    }

    public void Save()
    {
        // coastManager save
        PlayerPrefs.SetInt("coastButtonLevelUp", coastManager.coastButtonLevelUp);
        PlayerPrefs.SetInt("coastButtonTitleUp", coastManager.coastButtonTitleUp);
        PlayerPrefs.SetInt("coastButtonDamageUp", coastManager.coastButtonDamageUp);
        PlayerPrefs.SetInt("coastButtonSellUp", coastManager.coastButtonSellUp);
        PlayerPrefs.SetInt("coastButtonHealthUp", coastManager.coastButtonHealthUp);
        PlayerPrefs.SetInt("coastButtonAutoClicUp", coastManager.coastButtonAutoClicUp);

        // gameManager save
        PlayerPrefs.SetInt("playerTitleIndex", gameManager.playerTitleIndex);
        PlayerPrefs.SetInt("playerClicDamage", gameManager.playerClicDamage);
        PlayerPrefs.SetInt("playerClicSell", gameManager.playerClicSell);
        PlayerPrefs.SetInt("playerHealth", gameManager.playerHealth);
        PlayerPrefs.SetInt("playerLevel", gameManager.playerLevel);

        PlayerPrefs.SetInt("levelAutoClic", gameManager.levelAutoClic);
        PlayerPrefs.SetInt("levelLimitUpgrade", gameManager.levelLimitUpgrade);
        PlayerPrefs.SetInt("levelPlayerDamage", gameManager.levelPlayerDamage);
        PlayerPrefs.SetInt("levelPlayerSell", gameManager.levelPlayerSell);
        PlayerPrefs.SetInt("levelPlayerHealth", gameManager.levelPlayerHealth);
        PlayerPrefs.SetInt("levelShop", gameManager.levelShop);

        PlayerPrefs.SetInt("playerGoldScore", gameManager.playerGoldScore);
        PlayerPrefs.SetInt("playerSellScore", gameManager.playerSellScore);

        if (gameManager.godMode)
        {
            PlayerPrefs.SetInt("godMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("godMode", 0);
        }

        // boatManager save
        PlayerPrefs.SetInt("refBoatMaxHealth", boatManager.refBoatMaxHealth);
        PlayerPrefs.SetInt("refBoatDamage", boatManager.refBoatDamage);
        PlayerPrefs.SetInt("refBoatValue", boatManager.refBoatValue);

        // autoClicManager save
        if (autoClicManager.autoClic)
        {
            PlayerPrefs.SetInt("autoClic", 1);
        }
        else
        {
            PlayerPrefs.SetInt("autoClic", 0);
        }
        PlayerPrefs.SetFloat("timer", autoClicManager.timer);
    }

    public void Load()
    {
        // coastManager load
        coastManager.coastButtonLevelUp = PlayerPrefs.GetInt("coastButtonLevelUp", 50);
        coastManager.coastButtonTitleUp = PlayerPrefs.GetInt("coastButtonTitleUp", 100);
        coastManager.coastButtonDamageUp = PlayerPrefs.GetInt("coastButtonDamageUp", 15);
        coastManager.coastButtonSellUp = PlayerPrefs.GetInt("coastButtonSellUp", 10);
        coastManager.coastButtonHealthUp = PlayerPrefs.GetInt("coastButtonHealthUp", 20);
        coastManager.coastButtonAutoClicUp = PlayerPrefs.GetInt("coastButtonAutoClicUp", 1000);

        // gameManager load
        gameManager.playerTitleIndex = PlayerPrefs.GetInt("playerTitleIndex", 0);
        gameManager.playerClicDamage = PlayerPrefs.GetInt("playerClicDamage", 5);
        gameManager.playerClicSell = PlayerPrefs.GetInt("playerClicSell", 5);
        gameManager.playerHealth = PlayerPrefs.GetInt("playerHealth", 200);
        gameManager.playerLevel = PlayerPrefs.GetInt("playerLevel", 0);

        gameManager.levelAutoClic = PlayerPrefs.GetInt("levelAutoClic", 0);
        gameManager.levelLimitUpgrade = PlayerPrefs.GetInt("levelLimitUpgrade", 0);
        gameManager.levelPlayerDamage = PlayerPrefs.GetInt("levelPlayerDamage", 0);
        gameManager.levelPlayerSell = PlayerPrefs.GetInt("levelPlayerSell", 0);
        gameManager.levelPlayerHealth = PlayerPrefs.GetInt("levelPlayerHealth", 0);
        gameManager.levelShop = PlayerPrefs.GetInt("levelShop", 1);

        gameManager.playerGoldScore = PlayerPrefs.GetInt("playerGoldScore", 0);
        gameManager.playerSellScore = PlayerPrefs.GetInt("playerSellScore", 0);

        if (PlayerPrefs.GetInt("godMode", 0) == 0)
        {
            gameManager.godMode = false;
        }
        else
        {
            gameManager.godMode = true;
        }

        // boatManager load
        boatManager.refBoatMaxHealth = PlayerPrefs.GetInt("refBoatMaxHealth", 100);
        boatManager.refBoatDamage = PlayerPrefs.GetInt("refBoatDamage", 25);
        boatManager.refBoatValue = PlayerPrefs.GetInt("refBoatValue", 50);

        // autoClicManager load
        if (PlayerPrefs.GetInt("autoClic", 0) == 0)
        {
            autoClicManager.autoClic = false;
        }
        else
        {
            autoClicManager.autoClic = true;
        }

        autoClicManager.timer = PlayerPrefs.GetFloat("timer", 1.5f);
    }

    public void Reset()
    {
        // coastManager reset
        PlayerPrefs.SetInt("coastButtonLevelUp", 50);
        PlayerPrefs.SetInt("coastButtonTitleUp", 100);
        PlayerPrefs.SetInt("coastButtonDamageUp", 15);
        PlayerPrefs.SetInt("coastButtonSellUp", 10);
        PlayerPrefs.SetInt("coastButtonHealthUp", 20);
        PlayerPrefs.SetInt("coastButtonAutoClicUp", 1000);

        // gameManager reset
        PlayerPrefs.SetInt("playerTitleIndex", 0);
        PlayerPrefs.SetInt("playerClicDamage", 5);
        PlayerPrefs.SetInt("playerClicSell", 5);
        PlayerPrefs.SetInt("playerHealth", 200);
        PlayerPrefs.SetInt("playerLevel", 0);

        PlayerPrefs.SetInt("levelAutoClic", 0);
        PlayerPrefs.SetInt("levelLimitUpgrade", 0);
        PlayerPrefs.SetInt("levelPlayerDamage", 0);
        PlayerPrefs.SetInt("levelPlayerSell", 0);
        PlayerPrefs.SetInt("levelPlayerHealth", 0);
        PlayerPrefs.SetInt("levelShop", 1);

        PlayerPrefs.SetInt("playerGoldScore", 0);
        PlayerPrefs.SetInt("playerSellScore", 0);

        PlayerPrefs.SetInt("godMode", 0);

        // boatManager reset
        PlayerPrefs.SetInt("refBoatMaxHealth", 100);
        PlayerPrefs.SetInt("refBoatDamage", 25);
        PlayerPrefs.SetInt("refBoatValue", 50);

        // autoClicManager reset
        PlayerPrefs.SetInt("autoClic", 0);
        PlayerPrefs.SetFloat("timer", 1.5f);
    }
}
