using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClicManager : MonoBehaviour
{
    #region Scripts
    private GameManager gameManager;
    private BoatManager boatManager;
    private CoastManager coastManager;
    #endregion

    #region Variable
    [HideInInspector] public bool autoClic;
    private bool autoClicSelling;
    private bool autoClicDamaging;
    public float timer = 1.5f;
    [HideInInspector] public bool isSwitchingToShop = false;
    #endregion

    #region Objects
    [SerializeField] public GameObject getAutoClicGameObject;
    [SerializeField] public GameObject autoClicUpGameObject;
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

    public void ButtonAutoClicPrint()
    {
        if (gameManager.levelAutoClic == 10)
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(false);
        }
        else if (autoClic)
        {
            autoClicUpGameObject.SetActive(true);
            getAutoClicGameObject.SetActive(false);
        }
        else
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(true);
        }
    }

    public void ButonGetAutoClic()
    {
        autoClic = true;
        gameManager.playerGoldScore -= coastManager.coastButtonGetAutoClic;
    }
    public void ButonAutoClicUp()
    {
        gameManager.levelAutoClic += 1;
        timer -= 0.1f;
        gameManager.playerGoldScore -= coastManager.coastButtonAutoClicUp;
        coastManager.coastButtonAutoClicUp += 10000;
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
                gameManager.SellScoreToGoldScore();
            }
        }
    }

    public IEnumerator DamagingAutoClic()
    {
        while (autoClicDamaging)
        {
            yield return new WaitForSeconds(timer);
            boatManager.DamageToBoat();
        }
    }
}