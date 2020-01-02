using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attack
{
    private int range;
    public RangeAttack(int newDamage,int newAccuracy, int newRange):base(newDamage,newAccuracy)
    {
        damage = newDamage;
        accuracy = newAccuracy;
        range = newRange;
    }

    public void MakeAttack(Character attacker, Enemy defender)
    {
        int numberOfSpaces = Pathfinding.CheckRange(attacker.transform.position, defender.transform.position);
        if(numberOfSpaces<=range)
        {
            MakeAttack(defender);
        }
    }

    public void MakeAttack(Enemy attacker, Character defender)
    {
        int numberOfSpaces = Pathfinding.CheckRange(attacker.transform.position, defender.transform.position);
        if (numberOfSpaces <= range)
        {
            MakeAttack(defender);
        }
    }
}
