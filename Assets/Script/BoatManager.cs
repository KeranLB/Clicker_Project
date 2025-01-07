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
    private Dictionary<int, float> levelMultiple;
    private Dictionary<string, float> titleMultiple;
    private List<Boat> factionList;
    #endregion

    #region StatReferences
    [HideInInspector]public int refBoatMaxHealth;
    [HideInInspector] public int refBoatDamage;
    [HideInInspector] public int refBoatValue;
    #endregion

    #region StatActive
    private int activeBoatMaxHealth;
    private int activeBoatCurrentHealth;
    private int activeBoatDamage;
    private int activeBoatValue;
    #endregion

    public void StartBoatManager()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        levelMultiple = new Dictionary<int, float>()
        {
            { 1, 1.1f},
            { 2, 1.2f},
            { 3, 1.3f},
            { 4, 1.4f},
            { 5, 1.5f},
            { 6, 1.6f},
            { 7, 1.7f},
            { 8, 1.8f},
            { 9, 1.9f},
            { 10, 2f},
        };

        titleMultiple = new Dictionary<string, float>()
        {
            { "marin", 0.25f},
            { "musicien", 0.5f},
            { "cuisinier", 0.75f},
            { "canonnier", 1f},
            { "voilier", 1.25f},
            { "tonnelier", 1.5f},
            { "charpentier", 1.75f},
            { "officier", 2f},
            { "maitreEquipage", 2.25f},
            { "Navigateur", 2.5f},
            { "QuartierMaitre", 2.75f},
            { "Capitaine", 3f},
        };

        factionList = new List<Boat>()
        {
            {Voyageur},
            {Marchand},
            {Pirate},
            {Military},
        };
    }

    public void OnClic()
    {
        print(levelMultiple.GetValueOrDefault(2));
        print(titleMultiple.GetValueOrDefault("marin"));
        print(factionList[0]);
    }

    public void SpawnBoat()
    {
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
    }
}
