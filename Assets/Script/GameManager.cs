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
    [HideInInspector] public int playerTitleIndex;
    [HideInInspector] public int playerClicDamage;
    [HideInInspector] public int playerClicSell;
    [HideInInspector] public int playerHealth;
    [HideInInspector] public int playerLevel;
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
    [HideInInspector] public int playerGoldScore;
    [HideInInspector] public int playerSellScore;
    #endregion

    #region screenInfos
    [Header("Screen :")]
    [SerializeField] private Text textLevelPlayer;
    [SerializeField] private Text textGoldScore;
    [SerializeField] private Text textSellScore;
    [SerializeField] private Text textLevelShop;
    [SerializeField] private Text textLevelDamage;
    [SerializeField] private Text textLevelSell;
    [SerializeField] private Text textLevelHealth;
    [SerializeField] private Text textLevelAutoClic;
    #endregion

    void Start()
    {
        StartGameManager();
        coastManager.StartCoastManager();
        boatManager.StartBoatManager();
        upgradeManager.StartUpgradeManager();
        autoClicManager.StartAutoClicManager();
    }

    private void StartGameManager()
    {
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
            "Quartier-Maître",
            "Capitaine",
        };

        playerLevel = 0;
        playerTitleIndex = 0;
        playerGoldScore = 0;
        playerSellScore = 0;
        levelShop = 0;

        levelLimitUpgrade = 9;
        levelPlayerDamage = 0;
        levelPlayerSell = 0;
        levelPlayerHealth = 0;
    }

    private void Update()
    {
        levelTextUpdate();
        coastManager.PlayVerifCoast();
    }
    /*
    private void Test()
    {
        print(coastManager.coastDictionaire.GetValueOrDefault(playerTitleIndex));
    }
    */
    public void levelTextUpdate()
    {
        // affiche les Scores du joueur
        textGoldScore.text = "Gold : " + playerGoldScore.ToString();
        textSellScore.text = "Loot Value : " + playerSellScore.ToString();
        // affiche les niveaux des différentes stats du joueur
        textLevelPlayer.text = playerTitle[playerTitleIndex].ToString() + " level" + playerLevel.ToString();
        textLevelShop.text = "Level : " + levelShop.ToString();
        textLevelDamage.text = "ATK Level" + levelPlayerDamage.ToString();
        textLevelSell.text = "Sell Level" + levelPlayerSell.ToString();
        textLevelHealth.text = "HP Level" + levelPlayerHealth.ToString();
        textLevelAutoClic.text = "Auto Clic Level" + levelAutoClic.ToString();
        // affiche le prix de chaque amélioration

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
