using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoastManager : MonoBehaviour
{
    #region Scripts
    private GameManager gameManager;
    private UpgradeManager upgradeManager;
    private AutoClicManager autoClicManager;
    #endregion

    #region Gestion
    [HideInInspector] public Dictionary<int, int> coastDictionaire;
    #endregion

    #region Variables
    [HideInInspector] public int coastButtonLevelUp;
    [HideInInspector] public int coastButtonTitleUp;
    [HideInInspector] public int coastButtonDamageUp;
    [HideInInspector] public int coastButtonSellUp;
    [HideInInspector] public int coastButtonHealthUp;
    [HideInInspector] public int coastButtonGetAutoClic;
    [HideInInspector] public int coastButtonAutoClicUp;
    #endregion

    #region Screen
    [Header("Texte co�ts des am�liorations :")]
    [SerializeField] public Text textCoastLevelUp;
    [SerializeField] public Text textCoastTitleUp;
    [SerializeField] public Text textCoastDamage;
    [SerializeField] public Text textCoastSell;
    [SerializeField] public Text textCoastHealth;
    [SerializeField] public Text textCoastGetAutoClic;
    [SerializeField] public Text textCoastAutoClic;
    #endregion
    // Start is called before the first frame update
    public void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        upgradeManager = gameObject.GetComponent<UpgradeManager>();
        autoClicManager = gameObject.GetComponent<AutoClicManager>();

        // { indexTitle , riseCoast }
        coastDictionaire = new Dictionary<int, int>()
        {
            { 0, 1 },
            { 1, 2 },
            { 2, 4 },
            { 3, 8 },
            { 4, 16 },
            { 5, 32 },
            { 6, 64 },
            { 7, 128 },
            { 8, 256 },
            { 9, 512 },
            { 10, 1024 },
            { 11, 2048 },
        };

        coastButtonLevelUp = 50;
        coastButtonTitleUp = 100;
        coastButtonDamageUp = 15;
        coastButtonSellUp = 10;
        coastButtonHealthUp = 20;
        coastButtonGetAutoClic = 10000;


    }

    public void PlayVerifCoast()
    {
        //Verifie si le prix des boutons est sup�rieur au goldScore du joueur et si oui
        //il les d�sactive tant que le gold score n'est pas sup�rieur
        CoastVerif(upgradeManager.levelUpButton, coastButtonLevelUp);
        CoastVerif(upgradeManager.titleUpButton, coastButtonTitleUp);
        CoastVerif(upgradeManager.damageUpButton, coastButtonDamageUp);
        CoastVerif(upgradeManager.sellUpButton, coastButtonSellUp);
        CoastVerif(upgradeManager.healthUpButton, coastButtonHealthUp);
        if (autoClicManager.autoClic == false)
        {
            CoastVerif(autoClicManager.getAutoClicButton, coastButtonGetAutoClic);
        }
        else
        {
            CoastVerif(autoClicManager.autoClicUpButton, coastButtonAutoClicUp);
        }
    }

    public void CoastVerif(Button test, int coast)
    {
        if (gameManager.playerGoldScore < coast)
        {
            test.interactable = false;
        }
        else
        {
            test.interactable = true;
        }
    }
}
