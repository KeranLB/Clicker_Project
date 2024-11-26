using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Boat", menuName = "Loot/Boat", order = 0)]
public class Boat : ScriptableObject
{
    public BoatType Type;
    public BoatFaction Faction;
    public int Level;
}
