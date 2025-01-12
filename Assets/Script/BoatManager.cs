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
    [SerializeField] private Text textHp;
    #endregion

    #region Data
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

        factionList = new List<Boat>()
        {
            {Voyageur},
            {Marchand},
            {Pirate},
            {Military},
        };

        refBoatDamage = 25;
        refBoatValue = 50;
        refBoatMaxHealth = 100;
    }

    public void SpawnBoat()
    {
        // gère le spawn de bâteau aléatoire et met a jour les textes
        StopAllCoroutines();

        var index = Random.Range(0, 4);
        activeBoat = factionList[index];

        activeBoatMaxHealth = refBoatMaxHealth * activeBoat.multiplicateurHealth;
        activeBoatCurrentHealth = activeBoatMaxHealth;
        activeBoatDamage = refBoatDamage * activeBoat.multiplicateurAttack;
        activeBoatValue = refBoatValue * activeBoat.multiplicateurValue;

        BoatButton.sprite = activeBoat.boatSprite;
        textHp.text = $"{activeBoatMaxHealth} Hp";
        
        boatHealthBar.maxValue = activeBoatMaxHealth;
        boatHealthBar.value = activeBoatCurrentHealth;

        playerHealthBar.maxValue = gameManager.playerHealth;
        playerHealthBar.value = gameManager.playerHealth;

        StartCoroutine(DamageToPlayer());
    }

    public void DamageToBoat()
    {
        // fonction qui applique les dégats du joueur au bateau afficher
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
        // permet de réaliser les dégats des bâteau aux joueurs et de vérifier si c le bateau ennemie ou le joueur qui gagne le duel
        // et agir en conséquence
        while (playerHealthBar.value > 0 && boatHealthBar.value > 0)
        {
            yield return new WaitForSeconds(1);
            print("DamageToPlayer is running.");
            playerHealthBar.value -= activeBoatDamage;
        }

        // Si le joueur perd le duel : il perd tout son lootValue et refait spawn un autre bâteau aléatoire
        if (playerHealthBar.value <= 0)
        {
            // Verifie si il n'est pas en train de changer de canvas
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
        // Si le joueur gagne le duel, il récupére le loot du bateau et en refait spawn un autre
        else if (playerHealthBar.value > 0)
        {
            gameManager.playerSellScore += activeBoatValue;
            SpawnBoat();
        }
    }
}