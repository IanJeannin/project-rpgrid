using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //basic stats
    [Tooltip("How much health the character starts with.")]
    [SerializeField]
    private float hitPoints=10;
    [Tooltip("How many Action Points the character starts with.")]
    [SerializeField]
    private int actionPoints=5;
    [SerializeField]
    private float damage = 2;
    [SerializeField]
    private float defense = 1;
    [SerializeField]
    private int Dodge = 0;
    [SerializeField]
    private Enemy enemy;

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
    public float GetPositionY()
    {
        return transform.position.y;
    }
    public void TakeDamage(float AppliedDamage)
    {
        float ChanceToHit = Random.Range(0.0f, 100.0f);
        if (ChanceToHit>=Dodge && AppliedDamage>defense)
        {
            hitPoints = hitPoints - (AppliedDamage-defense);
        }
    }



    //current list of actions the player can do

    //basic attack
    public void NormalAttack()
    {
        enemy.TakeDamage(damage);
        AdjustAP(-2);
    }
    //this attack does extra damage and displaces the enemy
    public void PushAttack()
    {
        enemy.TakeDamage(damage+1); //push attacks do an additional 1 damage 
        enemy.UpdatePositionX(enemy.GetPositionX() + 1);
        AdjustAP(-3);
    }
    //this protects the user from damage for 2 turns
    public void Shield()
    {
        defense += 1;
        AdjustAP(-4);
    }
    //gives the otehr player a boost to their defense
    //public void Protection (Other Player2)
    //{
    //    Player2.defense += 1;
    //    AdjustAP(-5);
    //}

}
