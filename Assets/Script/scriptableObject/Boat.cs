using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Boat", menuName = "Loot/Boat", order = 0)]
public class Boat : ScriptableObject
{
    public Sprite boatSprite;
    public BoatFaction boatFaction; // inutile, c'est juste pour montrer que j ai compris les enums :)
    public int multiplicateurAttack;
    public int multiplicateurHealth;
    public int multiplicateurValue;
}
