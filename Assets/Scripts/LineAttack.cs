using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttack : BasicAttack
{
    [Tooltip("The empty gameObject containing all units.")]
    [SerializeField]
    private GameObject unitsContainer;

    private Pathfinding pathfinding;

    public LineAttack(int newDamage, int newAccuracy, int newRange) : base(newDamage, newAccuracy, newRange)
    {
        damage = newDamage;
        accuracy = newAccuracy;
        range = newRange;
    }

    public void MakeLineAttack(Character attacker, Vector3 endPosition)
    {
        if (attacker.transform.position.x == endPosition.x || attacker.transform.position.y == endPosition.y) //Ensures the attack is in a line, one of these MUST be true for the rest to commence
        {
            Vector3 finalSpace = attacker.transform.position;
            if (endPosition.x>attacker.transform.position.x) 
            {
                finalSpace.x += range;
            }
            if(endPosition.x<attacker.transform.position.x)
            {
                finalSpace.x -= range;
            }
            if(endPosition.y>attacker.transform.position.y)
            {
                finalSpace.y += range;
            }
            if(endPosition.y<attacker.transform.position.y)
            {
                finalSpace.y -= range;
            }
            foreach (Vector3 tile in pathfinding.FindShortestPath(attacker.transform.position, finalSpace))
            {
                    for (int childIndex = 0; childIndex < unitsContainer.transform.childCount; childIndex++)
                    {
                        Transform child = unitsContainer.transform.GetChild(childIndex);
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

    public void MakeLineAttack(Enemy attacker, Vector3 endPosition)
    {
        if (attacker.transform.position.x == endPosition.x || attacker.transform.position.y == endPosition.y) //Ensures the attack is in a line, one of these MUST be true for the rest to commence
        {
            Vector3 finalSpace = attacker.transform.position;
            if (endPosition.x > attacker.transform.position.x)
            {
                finalSpace.x += range;
            }
            if (endPosition.x < attacker.transform.position.x)
            {
                finalSpace.x -= range;
            }
            if (endPosition.y > attacker.transform.position.y)
            {
                finalSpace.y += range;
            }
            if (endPosition.y < attacker.transform.position.y)
            {
                finalSpace.y -= range;
            }
            foreach (Vector3 tile in pathfinding.FindShortestPath(attacker.transform.position, finalSpace))
            {
                for (int childIndex = 0; childIndex < unitsContainer.transform.childCount; childIndex++)
                {
                    Transform child = unitsContainer.transform.GetChild(childIndex);
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
