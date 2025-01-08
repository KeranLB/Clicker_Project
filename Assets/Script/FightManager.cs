using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    #region Scripts
    private GameManager gameManager;
    private BoatManager boatManager;
    #endregion

    #region Slider
    [Header("sdze")]
    [SerializeField] private Slider boatHealthBar;
    [SerializeField] private Slider playerHealthBar;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        boatManager = gameObject.GetComponent<BoatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
