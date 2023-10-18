using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        // You can access the GameObject that you collided with
        GameObject collidedObject = collision.gameObject;

        // Log the name of the object
        Debug.Log("Collided with: " + collidedObject.name);

        CharacterStatLogic enemy = collidedObject.GetComponent<CharacterStatLogic>();
        if (enemy != null)
        {
            enemy.TakeDmg(15);
            Debug.Log("Dmg : " + enemy.GetHealth());

        }
    }
}
