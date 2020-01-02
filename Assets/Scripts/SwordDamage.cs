using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    [SerializeField]
    private float damage=5;
    [SerializeField]
    private Character movingCharacter;
    [SerializeField]
    private Enemy closeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    //private void CharacterDamage(Character Player)
    //{
    //    Player.hitpoints -= damage;
    //}
    //private void EnemyDamage(Enemy enemy)
    //{
    //    enemy.hitpoints -= damage;
    //}
}
