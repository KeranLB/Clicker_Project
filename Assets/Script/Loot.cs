using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "loot", menuName = "Loot/loot", order =0)]
public class Loot : ScriptableObject
{
    public LootType Type;
    public LootRarity Rarety;

    public Sprite BaseImage;
    public Sprite RarityImage;

    public int Level;
}
