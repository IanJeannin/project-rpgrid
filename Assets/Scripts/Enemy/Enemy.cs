using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //basic stats
    [Tooltip("How much health the character starts with.")]
    [SerializeField]
    private float hitPoints = 10;
    [Tooltip("How many Action Points the character starts with.")]
    [SerializeField]
    private int actionPoints = 5;
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float defense = 1;
    [SerializeField]
    private int dodge = 0;

    private bool isTurn = false;

    public int GetAP()
    {
        return actionPoints;
    }

    public void AdjustAP(int adjustment)
    {
        actionPoints = actionPoints + adjustment;
    }
    public void UpdateAP(int update)
    {
        actionPoints = update;
    }
    public float GetPositionX()
    {
        return transform.position.x;
    }
    public void UpdatePositionX(float newX)
    {
        transform.position = new Vector2(newX, transform.position.y);
    }
    public float GetPositionY()
    {
        return transform.position.y;
    }
    public void TakeDamage(float appliedDamage)
    {
        float ChanceToHit = Random.Range(0.0f, 100.0f);
        if (ChanceToHit >= dodge && appliedDamage > defense)
        {
            hitPoints = hitPoints - (appliedDamage-defense);
        }
    }

    public int GetDodge()
    {
        return dodge;
    }

    public void ChangeTurn(bool active)
    {
        isTurn = active;
    }
}
