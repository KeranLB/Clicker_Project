using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClicManager : MonoBehaviour
{
    #region Scripts
    [HideInInspector] private GameManager gameManager;
    [HideInInspector] private BoatManager boatManager;
    private CoastManager coastManager;
    #endregion

    #region Variable
    [HideInInspector] public bool autoClic;
    private bool autoClicSelling;
    private bool autoClicDamaging;
    private float timer = 1f;
    [HideInInspector] public bool isSwitchingToShop = false;
    #endregion

    #region Objects
    [SerializeField] private GameObject getAutoClicGameObject;
    [SerializeField] private GameObject autoClicUpGameObject;
    [HideInInspector] public Button getAutoClicButton;
    [HideInInspector] public Button autoClicUpButton;
    #endregion

    public void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        boatManager = gameObject.GetComponent<BoatManager>();
        coastManager = gameObject.GetComponent<CoastManager>();

        gameManager.levelAutoClic = 0;
        autoClic = false;
        autoClicSelling = false;
        autoClicDamaging = false;
        getAutoClicButton = getAutoClicGameObject.GetComponent<Button>();
        autoClicUpButton = autoClicUpGameObject.GetComponent<Button>();
        autoClicUpGameObject.SetActive(false);
    }

    public void ButonGetAutoClic()
    {
        autoClic = true;
        gameManager.playerGoldScore -= coastManager.coastButtonGetAutoClic;
    }
    public void ButonAutoClicUp()
    {
        gameManager.levelAutoClic += 1;
        gameManager.playerGoldScore -= coastManager.coastButtonAutoClicUp;
        coastManager.coastButtonAutoClicUp *= 10;
    }

    public void SwitchAutoClicToShop()
    {
        autoClicSelling = true;
        autoClicDamaging = false;
        isSwitchingToShop = true;
        StopAllCoroutines();
        boatManager.StopAllCoroutines();
        if (autoClic)
        {
            StartCoroutine(SellingAutoClic());
        }
    }

    public void SwitchAutoClicToFight()
    {
        autoClicSelling = false;
        autoClicDamaging = true;
        isSwitchingToShop = false;
        StopAllCoroutines();
        if (autoClic)
        {
            StartCoroutine(DamagingAutoClic());
        }
    }

    IEnumerator SellingAutoClic()
    {
        while (autoClicSelling)
        {
            yield return new WaitForSeconds(timer);
            if (gameManager.playerSellScore > 0)
            {
                gameManager.SellScoreToGoldScore(gameManager.levelAutoClic);
            }
        }
    }

    public IEnumerator DamagingAutoClic()
    {
        while (autoClicDamaging)
        {
            yield return new WaitForSeconds(timer);
            print("DamageToBoat is running.");
            boatManager.DamageToBoat();
        }
    }
}