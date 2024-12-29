using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoastManager : MonoBehaviour
{
    #region Scripts
    [HideInInspector] private GameManager gameManager;
    [HideInInspector] private UpgradeManager upgradeManager;
    [HideInInspector] private AutoClicManager autoClicManager;
    #endregion

    #region Gestion
    public Dictionary<int, int> coastDictionaire;
    public int riseCoast;
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
    // Start is called before the first frame update
    void Start()
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
        coastButtonAutoClicUp = 1000;
    }

    public void Update()
    {
        CoastVerif(upgradeManager.levelUpButton, coastButtonLevelUp);
        CoastVerif(upgradeManager.titleUpButton, coastButtonTitleUp);
        CoastVerif(upgradeManager.damageUpButton, coastButtonDamageUp);
        CoastVerif(upgradeManager.sellUpButton, coastButtonSellUp);
        CoastVerif(upgradeManager.healthUpButton, coastButtonHealthUp);
        CoastVerif(autoClicManager.getAutoClicButton, coastButtonGetAutoClic);
        CoastVerif(autoClicManager.autoClicUpButton, coastButtonAutoClicUp);
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
