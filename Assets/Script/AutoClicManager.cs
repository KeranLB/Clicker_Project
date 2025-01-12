using System.Collections;
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
        // Si l'auto Clic atteint le level 10 il d�sactive son bouton
        if (gameManager.levelAutoClic == 10)
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(false);
        }
        // Sinon si il poss�de juste l autoClic il d�sactive le bouton pour obtenir l auto Clic et d�sactive celui de l'am�lioration
        else if (autoClic)
        {
            autoClicUpGameObject.SetActive(true);
            getAutoClicGameObject.SetActive(false);
        }
        // Sinon il active le bouton pour obtenir l auto clic et desactive celui de l'am�lioration
        else
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(true);
        }
    }

    public void ButonGetAutoClic()
    {
        // obtient l auto clic et d�duis son prix au gold score
        autoClic = true;
        gameManager.playerGoldScore -= coastManager.coastButtonGetAutoClic;
    }

    public void ButonAutoClicUp()
    {
        // am�liore l'auitoClic et d�duis son prix au gold score
        gameManager.levelAutoClic += 1;
        timer -= 0.1f;
        gameManager.playerGoldScore -= coastManager.coastButtonAutoClicUp;
        coastManager.coastButtonAutoClicUp += 10000;
    }

    public void SwitchAutoClicToShop()
    {
        // active l'autoclic de revente et arrete l auto clic de d�g�ts
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
        // active l autoClic de d�g�ts et arrete celui de revente
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
        //boucle pour r�aliser l auto clic de revente
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
        // boucle pour r�amiser l 'auto clic de d�g�ts
        while (autoClicDamaging)
        {
            yield return new WaitForSeconds(timer);
            boatManager.DamageToBoat();
        }
    }
}