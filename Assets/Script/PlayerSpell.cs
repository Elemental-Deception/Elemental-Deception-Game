using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSpell : MonoBehaviour
{
    public GameObject player;
    private CharacterStatLogic playerLogic;

    void Start()
    {
        playerLogic = player.GetComponent<CharacterStatLogic>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // You can access the GameObject that you collided with
        GameObject collidedObject = collision.gameObject;

        // Log the name of the object
        //Debug.Log("Collided with: " + collidedObject.name);

        EnemyStatLogic enemy = collidedObject.GetComponent<EnemyStatLogic>();
        if (enemy != null)
        {
            enemy.TakeDmg((int)Math.Round(15 * playerLogic.damageSystem.DamageMultiplier));
        }
    }
}
