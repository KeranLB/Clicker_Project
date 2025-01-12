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
        // Si l'auto Clic atteint le level 10 il désactive son bouton
        if (gameManager.levelAutoClic == 10)
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(false);
        }
        // Sinon si il possède juste l autoClic il désactive le bouton pour obtenir l auto Clic et désactive celui de l'amélioration
        else if (autoClic)
        {
            autoClicUpGameObject.SetActive(true);
            getAutoClicGameObject.SetActive(false);
        }
        // Sinon il active le bouton pour obtenir l auto clic et desactive celui de l'amélioration
        else
        {
            autoClicUpGameObject.SetActive(false);
            getAutoClicGameObject.SetActive(true);
        }
    }

    public void ButonGetAutoClic()
    {
        // obtient l auto clic et déduis son prix au gold score
        autoClic = true;
        gameManager.playerGoldScore -= coastManager.coastButtonGetAutoClic;
    }

    public void ButonAutoClicUp()
    {
        // améliore l'auitoClic et déduis son prix au gold score
        gameManager.levelAutoClic += 1;
        timer -= 0.1f;
        gameManager.playerGoldScore -= coastManager.coastButtonAutoClicUp;
        coastManager.coastButtonAutoClicUp += 10000;
    }

    public void SwitchAutoClicToShop()
    {
        // active l'autoclic de revente et arrete l auto clic de dégâts
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
        // active l autoClic de dégâts et arrete celui de revente
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
        //boucle pour réaliser l auto clic de revente
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
        // boucle pour réamiser l 'auto clic de dégâts
        while (autoClicDamaging)
        {
            yield return new WaitForSeconds(timer);
            boatManager.DamageToBoat();
        }
    }
}