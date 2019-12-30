using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private GameObject characterOne;
    [SerializeField]
    private GameObject characterTwo;
    [Tooltip("The multiplier for AP difference conversion to percentage. 10 makes 1 AP =10% more likely to go first.")]
    [SerializeField]
    private float apWeight;

    private int lastFirstEnemyIndex; //Stores index of the first enemy to move
    private int lastSecondEnemyIndex; //Stores index of the second enemy to move
    private List<int> turnOrder;

    //returns a list of the active game objects in turn order. 
    public List<GameObject> SetOrder()
    {
        GameObject firstCharacter;
        GameObject secondCharacter;
        int numberOfEnemies = enemies.Count;
        int firstEnemyIndex = Random.Range(0, enemies.Count);
        int secondEnemyIndex = Random.Range(0, enemies.Count);
        if (numberOfEnemies == 2) //If there are only two enemies, ensures that the same one doesn't move twice. 
        {
            while(firstEnemyIndex==secondEnemyIndex)
            {
                secondEnemyIndex = Random.Range(0, enemies.Count);
            }
        }
        else if(numberOfEnemies==3) //If there are 3 enemies, ensures the first enemy to move last turn will not move this turn. 
        {
            while(firstEnemyIndex==lastFirstEnemyIndex)
            {
                firstEnemyIndex = Random.Range(0, enemies.Count);
            }
            while(secondEnemyIndex==firstEnemyIndex||secondEnemyIndex==lastFirstEnemyIndex)
            {
                secondEnemyIndex = Random.Range(0, enemies.Count);
            }
        }
        else if(numberOfEnemies>=4) //If there are 4 or more enemies, ensures the two enemies that moved last turn do not move this turn
        {
            while (firstEnemyIndex == lastFirstEnemyIndex||firstEnemyIndex==lastSecondEnemyIndex)
            {
                firstEnemyIndex = Random.Range(0, enemies.Count);
            }
            while (secondEnemyIndex == firstEnemyIndex || secondEnemyIndex == lastFirstEnemyIndex||secondEnemyIndex==lastSecondEnemyIndex)
            {
                secondEnemyIndex = Random.Range(0, enemies.Count);
            }
        }
        int characterOneAP = characterOne.GetComponent<Character>().GetAP();
        int characterTwoAP = characterTwo.GetComponent<Character>().GetAP();
        int apDifference = Mathf.Abs(characterOneAP - characterTwoAP);
        if (characterOneAP>characterTwoAP) //If characterOne has more ap than character two
        {
            if(Random.Range(0,100)<=50+(apDifference*apWeight)) //Determines if the characterOne goes first 
            {
                firstCharacter = characterOne;
                secondCharacter = characterTwo;
            }
            else
            {
                firstCharacter = characterTwo;
                secondCharacter = characterOne;
            }
        }
        else //if characterTwo has >= AP than characterOne
        {
            if (Random.Range(0, 100) <= 50 + (apDifference * apWeight)) //Determines if characterTwo goes first
            {
                firstCharacter = characterTwo;
                secondCharacter = characterOne;
            }
            else
            {
                firstCharacter = characterOne;
                secondCharacter = characterTwo;
            }
        }
        //Creates a list of the game objects in turn order. One of the characters will always go first, followed by an enemy, then the next character, then the last enemy. 
        List<GameObject> turnOrder = new List<GameObject>()
        {
            firstCharacter,
            enemies[firstEnemyIndex],
            secondCharacter,
            enemies[secondEnemyIndex]
        };
        return turnOrder;
    }
}
