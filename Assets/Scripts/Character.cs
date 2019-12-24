using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Tooltip("How much health the character starts with.")]
    [SerializeField]
    private float hitPoints=10;
    [Tooltip("How many Action Points the character starts with.")]
    [SerializeField]
    private int actionPoints=5;

    public int GetAP()
    {
        return actionPoints;
    }

    public void AdjustAP(int adjustment)
    {
        actionPoints = actionPoints + adjustment;
    }
}
