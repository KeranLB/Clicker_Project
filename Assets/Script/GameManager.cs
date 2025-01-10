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

    private void Start()
    {
        // Récupère les autres scripts
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
        levelShop = 1;

        playerClicDamage = 1;
        playerClicSell = 1;
        playerHealth = 200;


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
        // enlève le bouton de d'amélioration de nitveau et de titre au niveau max
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
        // affiche les niveaux des différentes stats du joueur
        textLevelPlayer.text = playerTitle[playerTitleIndex].ToString() + " level" + playerLevel.ToString();
        textLevelShop.text = levelShop.ToString();
        textLevelDamage.text = "ATK Level" + levelPlayerDamage.ToString();
        textLevelSell.text = "Sell Level" + levelPlayerSell.ToString();
        textLevelHealth.text = "HP Level" + levelPlayerHealth.ToString();
        textLevelAutoClic.text = "Auto Clic Level" + levelAutoClic.ToString();
        // affiche le prix de chaque amélioration
        coastManager.textCoastLevelUp.text = $"Level Up : {coastManager.coastButtonLevelUp}$";
        coastManager.textCoastTitleUp.text = $"Title Up : {coastManager.coastButtonTitleUp}$";
        coastManager.textCoastDamage.text = $"Damage Up : {coastManager.coastButtonDamageUp}$";
        coastManager.textCoastSell.text = $" Sell Up : {coastManager.coastButtonSellUp}$";
        coastManager.textCoastHealth.text = $"Health Up : {coastManager.coastButtonHealthUp}$";
        coastManager.textCoastGetAutoClic.text = $"Get Auto Clic : {coastManager.coastButtonGetAutoClic}$";
        coastManager.textCoastAutoClic.text = $"Auto Clic Up : {coastManager.coastButtonAutoClicUp}$";
    }
    
    public void ButtonShop()
    {
        SellScoreToGoldScore(playerClicSell);
        print(playerClicSell);
    }

    public void SellScoreToGoldScore(int sell)
    {
        if (playerSellScore < playerClicSell)
        {
            playerGoldScore += playerSellScore;
            playerSellScore = 0;
        }
        else if (playerSellScore == 0)
        {
            print("Vous n'avez plus de loot.");
        }
        else
        {
            playerSellScore -= sell;
            playerGoldScore += sell;
        }
    }
}
