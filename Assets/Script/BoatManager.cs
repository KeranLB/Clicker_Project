using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatManager : MonoBehaviour
{
    #region Scripts
    [Header("ScriptsManager :")]
    [HideInInspector] private GameManager gameManager;
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
    [HideInInspector] private Image BoatButton;
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
    [SerializeField] private Slider playerHealthBar;
    #endregion

    private void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        baseHealthStatsFromTitle = new Dictionary<string, int>()
        {
            { "marin", 100},
            { "musicien", 200},
            { "cuisinier", 400},
            { "canonnier", 800},
            { "voilier", 1600},
            { "tonnelier", 3200},
            { "charpentier", 6400},
            { "officier", 12800},
            { "maitreEquipage", 25600},
            { "Navigateur", 51200},
            { "QuartierMaitre", 102400},
            { "Capitaine", 204800},
        };

        baseBoatLootStats = new Dictionary<string, int>()
        {
            { "marin", 25},
            { "musicien", 50},
            { "cuisinier", 100},
            { "canonnier", 200},
            { "voilier", 400},
            { "tonnelier", 800},
            { "charpentier", 1600},
            { "officier", 3200},
            { "maitreEquipage", 6400},
            { "Navigateur", 12800},
            { "QuartierMaitre", 25600},
            { "Capitaine", 51200},
        };

        baseBoatDamageStats = new Dictionary<string, int>()
        {
            { "marin", 25},
            { "musicien", 50},
            { "cuisinier", 100},
            { "canonnier", 200},
            { "voilier", 400},
            { "tonnelier", 800},
            { "charpentier", 1600},
            { "officier", 3200},
            { "maitreEquipage", 6400},
            { "Navigateur", 12800},
            { "QuartierMaitre", 25600},
            { "Capitaine", 51200},
        };

        addStatsFromLevel = new Dictionary<string, int>()
        {
            { "marin", 1},
            { "musicien", 2},
            { "cuisinier", 4},
            { "canonnier", 8},
            { "voilier", 16},
            { "tonnelier", 32},
            { "charpentier", 64},
            { "officier", 128},
            { "maitreEquipage", 256},
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

    /*
    public void OnClic()
    {
        print(levelMultiple.GetValueOrDefault(2));
        print(titleMultiple.GetValueOrDefault("marin"));
        print(factionList[0]);
    }
    */

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
            playerHealthBar.value -= activeBoatDamage;
        }

        if (playerHealthBar.value < 0)
        {
            SpawnBoat();
        }
        else if (playerHealthBar.value > 0)
        {
            gameManager.playerSellScore += activeBoatValue;
        }
    }
}