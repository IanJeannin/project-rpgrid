using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : BasicAttack
{
    [Tooltip("The empty gameObject containing all units.")]
    [SerializeField]
    private GameObject unitsContainer;

    private Pathfinding pathfinding;
    private int aoeRadius;

    public AoEAttack(int newDamage, int newAccuracy, int newRange, int newRadius) : base(newDamage, newAccuracy, newRange)
    {
        damage = newDamage;
        accuracy = newAccuracy;
        range = newRange;
        aoeRadius = newRadius;
    }

    public void MakeAoEAttack(Character attacker, Vector3 endPosition)
    {
        int numberOfSpaces = pathfinding.CheckRange(attacker.transform.position, endPosition);
        if (numberOfSpaces <= range)
        {
            foreach(Vector3 tile in pathfinding.GetAoE(endPosition,aoeRadius))
            {
                for(int childIndex=0;childIndex<unitsContainer.transform.childCount;childIndex++)
                {
                    Transform child = unitsContainer.transform.GetChild(childIndex);
                    if(child.position==tile)
                    {
                        if (child.CompareTag("Player"))
                        {
                            MakeAttack(child.GetComponent<Character>());
                        }
                        else
                        {
                            MakeAttack(child.GetComponent<Enemy>());
                        }
                    }
                }
            }
        }
    }

    public void MakeAoEAttack(Enemy attacker, Vector3 endPosition)
    {
        int numberOfSpaces = pathfinding.CheckRange(attacker.transform.position, endPosition);
        if (numberOfSpaces <= range)
        {
            //Gets list of tiles from Shortest path, and checks compares all enemy positions to effected tiles. 
            foreach (Vector3 tile in pathfinding.GetAoE(endPosition, aoeRadius))
            {
                for (int x = 0; x < unitsContainer.transform.childCount; x++)
                {
                    Transform child = unitsContainer.transform.GetChild(x);
                    if (child.position == tile)
                    {
                        if (child.CompareTag("Player"))
                        {
                            MakeAttack(child.GetComponent<Character>());
                        }
                        else
                        {
                            MakeAttack(child.GetComponent<Enemy>());
                        }
                    }
                }
            }
        }
    }
}
