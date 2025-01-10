using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Scripts
    [HideInInspector] private CoastManager coastManager;
    [HideInInspector] private UpgradeManager upgradeManager;
    [HideInInspector] private BoatManager boatManager;
    [HideInInspector] private AutoClicManager autoClicManager;
    #endregion

    #region Player
    [Header("Player Informations :")]
    [HideInInspector] public List<string> playerTitle;
    public int playerTitleIndex;
    [HideInInspector] public int playerClicDamage;
    [HideInInspector] public int playerClicSell;
    [HideInInspector] public int playerHealth;
    [HideInInspector] public int playerLevel;
    [HideInInspector] public bool godMode;
    #endregion

    #region Level
    [HideInInspector] public int levelAutoClic;
    [HideInInspector] public int levelLimitUpgrade;
    [HideInInspector] public int levelPlayerDamage;
    [HideInInspector] public int levelPlayerSell;
    [HideInInspector] public int levelPlayerHealth;
    [HideInInspector] public int levelShop;
    #endregion

    #region score
    public long playerGoldScore;
    public long playerSellScore;
    #endregion

    #region screenInfos
    [Header("Screen :")]
    [SerializeField] public Text textLevelPlayer;
    [SerializeField] public Text textGoldScore;
    [SerializeField] public Text textSellScore;
    [SerializeField] public Text textLevelShop;
    [SerializeField] public Text textLevelDamage;
    [SerializeField] public Text textLevelSell;
    [SerializeField] public Text textLevelHealth;
    [SerializeField] public Text textLevelAutoClic;
    [SerializeField] public GameObject levelUpButton;
    #endregion

    void Start()
    {
        StartGameManager();
        //coastManager.StartCoastManager();
        //boatManager.StartBoatManager();
        //upgradeManager.StartUpgradeManager();
        //autoClicManager.StartAutoClicManager();
    }

    private void StartGameManager()
    {
        // Get OtheScripts
        coastManager = gameObject.GetComponent<CoastManager>();
        upgradeManager = gameObject.GetComponent<UpgradeManager>();
        boatManager = gameObject.GetComponent<BoatManager>();
        autoClicManager = gameObject.GetComponent<AutoClicManager>();

        
        playerTitle = new List<string>
        {
            "Marin",
            "Musicien",
            "Cuisinier",
            "Canonnier",
            "Voilier",
            "Tonnelier",
            "Charpentier",
            "Officier",
            "Maitre d'Equipage",
            "Navigateur",
            "Quartier-Ma�tre",
            "Capitaine",
        };

        playerLevel = 0;
        playerTitleIndex = 0;
        playerGoldScore = 0;
        playerSellScore = 0;
        levelShop = 1;

        levelLimitUpgrade = 9;
        levelPlayerDamage = 0;
        levelPlayerSell = 0;
        levelPlayerHealth = 0;
    }

    private void Update()
    {
        //test mode
        if (godMode)
        {
            playerGoldScore = 999999999;
        }

        if (playerTitleIndex == 11 && playerLevel == 10)
        {
            levelUpButton.SetActive(false);
        }
        levelTextUpdate();
        coastManager.PlayVerifCoast();
    }

    public void SetGodMode()
    {
        if (!godMode)
        {
            godMode = true;
        }
        else
        {
            godMode = false;
        }
    }
    public void levelTextUpdate()
    {
        // affiche les Scores du joueur
        textGoldScore.text = $"Gold : {playerGoldScore}";
        textSellScore.text = "Loot Value : " + playerSellScore.ToString();
        // affiche les niveaux des diff�rentes stats du joueur
        textLevelPlayer.text = playerTitle[playerTitleIndex].ToString() + " level" + playerLevel.ToString();
        textLevelShop.text = levelShop.ToString();
        textLevelDamage.text = "ATK Level" + levelPlayerDamage.ToString();
        textLevelSell.text = "Sell Level" + levelPlayerSell.ToString();
        textLevelHealth.text = "HP Level" + levelPlayerHealth.ToString();
        textLevelAutoClic.text = "Auto Clic Level" + levelAutoClic.ToString();
        // affiche le prix de chaque am�lioration

    }
    
    public void ButtonShop()
    {
        SellScoreToGoldScore(playerClicSell);
    }

    public void SellScoreToGoldScore(int sell)
    {
        if (playerSellScore < playerClicSell)
        {
            playerGoldScore += playerSellScore;
            playerSellScore = 0;
        }
        else
        {
            playerSellScore -= sell;
            playerGoldScore += sell;
        }
    }
}
