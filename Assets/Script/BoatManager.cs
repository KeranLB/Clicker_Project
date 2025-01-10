using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatManager : MonoBehaviour
{
    #region Scripts
    [Header("ScriptsManager :")]
    [HideInInspector] private GameManager gameManager;
    private AutoClicManager autoClicManager;
    #endregion

    #region ScriptableObjects
    [Header("ScriptableObject :")]
    [SerializeField] private Boat Voyageur;
    [SerializeField] private Boat Marchand;
    [SerializeField] private Boat Pirate;
    [SerializeField] private Boat Military;
    [HideInInspector] private Boat activeBoat;
    #endregion

    #region Screen
    [Header("Image Boat Boutton :")]
    [SerializeField] private Image BoatButton;
    [Header("Text :")]
    [SerializeField] private Text textValue;
    [SerializeField] private Text textFaction;
    [SerializeField] private Text textHp;
    [SerializeField] private Text textAtk;
    #endregion

    #region Data
    private Dictionary<string, int> baseHealthStatsFromTitle;
    private Dictionary<string, int> baseBoatLootStats;
    private Dictionary<string, int> baseBoatDamageStats;
    private Dictionary<string, int> addStatsFromLevel;
    private List<Boat> factionList;
    #endregion

    #region StatsReferences
    [HideInInspector]public int refBoatMaxHealth;
    [HideInInspector] public int refBoatDamage;
    [HideInInspector] public int refBoatValue;
    #endregion

    #region StatsActives
    private int activeBoatMaxHealth;
    private int activeBoatCurrentHealth;
    private int activeBoatDamage;
    private int activeBoatValue;
    #endregion

    #region Slider
    [Header("Slider :")]
    [SerializeField] private Slider boatHealthBar;
    [SerializeField] public Slider playerHealthBar;
    #endregion

    private void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        autoClicManager = gameObject.GetComponent<AutoClicManager>();

        baseHealthStatsFromTitle = new Dictionary<string, int>()
        {
            { "Marin", 100},
            { "Musicien", 200},
            { "Cuisinier", 400},
            { "Canonnier", 800},
            { "Voilier", 1600},
            { "Tonnelier", 3200},
            { "Charpentier", 6400},
            { "Officier", 12800},
            { "MaitreEquipage", 25600},
            { "Navigateur", 51200},
            { "QuartierMaitre", 102400},
            { "Capitaine", 204800},
        };

        baseBoatLootStats = new Dictionary<string, int>()
        {
            { "Marin", 25},
            { "Musicien", 50},
            { "Cuisinier", 100},
            { "Canonnier", 200},
            { "Voilier", 400},
            { "Tonnelier", 800},
            { "Charpentier", 1600},
            { "Officier", 3200},
            { "MaitreEquipage", 6400},
            { "Navigateur", 12800},
            { "QuartierMaitre", 25600},
            { "Capitaine", 51200},
        };

        baseBoatDamageStats = new Dictionary<string, int>()
        {
            { "Marin", 25},
            { "Musicien", 50},
            { "Cuisinier", 100},
            { "Canonnier", 200},
            { "Voilier", 400},
            { "Tonnelier", 800},
            { "Charpentier", 1600},
            { "Officier", 3200},
            { "MaitreEquipage", 6400},
            { "Navigateur", 12800},
            { "QuartierMaitre", 25600},
            { "Capitaine", 51200},
        };

        addStatsFromLevel = new Dictionary<string, int>()
        {
            { "Marin", 1},
            { "Musicien", 2},
            { "Cuisinier", 4},
            { "Canonnier", 8},
            { "Voilier", 16},
            { "Tonnelier", 32},
            { "Charpentier", 64},
            { "Officier", 128},
            { "MaitreEquipage", 256},
            { "Navigateur", 512},
            { "QuartierMaitre", 1024},
            { "Capitaine", 2048},
        };

        factionList = new List<Boat>()
        {
            {Voyageur},
            {Marchand},
            {Pirate},
            {Military},
        };
    }

    private void SetBoatref()
    {
        int baseRef = baseHealthStatsFromTitle.GetValueOrDefault(gameManager.playerTitle[gameManager.playerTitleIndex]);
        int addRef = addStatsFromLevel.GetValueOrDefault(gameManager.playerTitle[gameManager.playerTitleIndex]);
        refBoatMaxHealth = baseRef + (addRef * gameManager.playerLevel);
        baseRef = baseBoatLootStats.GetValueOrDefault(gameManager.playerTitle[gameManager.playerTitleIndex]);
        refBoatValue = baseRef + (addRef * gameManager.playerLevel);
        baseRef = baseBoatDamageStats.GetValueOrDefault(gameManager.playerTitle[gameManager.playerTitleIndex]);
        refBoatDamage = baseRef + (addRef * gameManager.playerLevel);
    }

    public void SpawnBoat()
    {
        SetBoatref();
        StopAllCoroutines();

        var index = Random.Range(0, 4);
        activeBoat = factionList[index];

        activeBoatMaxHealth = refBoatMaxHealth * activeBoat.multiplicateurHealth;
        activeBoatCurrentHealth = activeBoatMaxHealth;
        activeBoatDamage = refBoatDamage * activeBoat.multiplicateurAttack;
        activeBoatValue = refBoatValue * activeBoat.multiplicateurValue;

        BoatButton.sprite = activeBoat.boatSprite;
        textValue.text = activeBoatValue.ToString();
        textFaction.text = activeBoat.boatFaction.ToString();
        textHp.text = activeBoatCurrentHealth.ToString() + "/" + activeBoatMaxHealth.ToString();
        textAtk.text = activeBoatDamage.ToString();
        
        boatHealthBar.maxValue = activeBoatMaxHealth;
        boatHealthBar.value = activeBoatCurrentHealth;

        playerHealthBar.maxValue = gameManager.playerHealth;
        playerHealthBar.value = gameManager.playerHealth;

        StartCoroutine(DamageToPlayer());
    }

    public void DamageToBoat()
    {
        if (activeBoatCurrentHealth < gameManager.playerClicDamage)
        {
            activeBoatCurrentHealth = 0;
        }
        else
        {
            activeBoatCurrentHealth -= gameManager.playerClicDamage;
        }
        boatHealthBar.value = activeBoatCurrentHealth;
    }

    IEnumerator DamageToPlayer()
    {
        while (playerHealthBar.value > 0 && boatHealthBar.value > 0)
        {
            yield return new WaitForSeconds(1);
            print("DamageToPlayer is running.");
            playerHealthBar.value -= activeBoatDamage;
        }

        if (playerHealthBar.value <= 0)
        {
            if (!autoClicManager.isSwitchingToShop)
            {
                gameManager.playerSellScore = 0;
            }
            else
            {
                autoClicManager.isSwitchingToShop = false;
            }
            SpawnBoat();
        }
        else if (playerHealthBar.value > 0)
        {
            gameManager.playerSellScore += activeBoatValue;
            SpawnBoat();
        }
    }
}