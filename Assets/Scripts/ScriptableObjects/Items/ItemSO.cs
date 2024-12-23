using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject {

    public enum Type {
        NICE,
        NAUGHTY,
        SPECIAL
    }

    public string itemName;
    [NonSerialized] public Material material;
    [NonSerialized] public Gradient gradient;
    [NonSerialized] public Type type;
    public int guessedNiceReward;
    public int guessedNaughtyReward;
    public int guessedNicePenalty;
    public int guessedNaughtyPenalty;
}
