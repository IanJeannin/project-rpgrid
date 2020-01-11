using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected int damage;
    protected float accuracy;

    public Attack(int newDamage, float newAccuracy)
    {
        damage = newDamage;
        accuracy = newAccuracy;
    }

    /// <summary>
    /// Accuracy Formula: Roll (0,100) If Roll >100-Accuracy+Dodge, hits. 
    /// </summary>
    /// <returns></returns>
    public void MakeAttack(Enemy enemy)
    {
        int hit = Random.Range(0, 100);
        if(hit>=100-accuracy+enemy.GetDodge()) 
        {
            enemy.TakeDamage(damage);
        }
        else
        {
            
        }
    }

    public void MakeAttack(Character character)
    {
        int hit = Random.Range(0, 100);
        if (hit >= 100 - accuracy+character.GetDodge()) 
        {
            character.TakeDamage(damage);
        }
        else
        {

        }
    }
}