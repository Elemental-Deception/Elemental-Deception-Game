using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public bool PlayerIsHere;
    private StatsSystem statsSystem;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        statsSystem = new StatsSystem();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsHere)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
            statsSystem.setHealth(statsSystem.getMaxHealth());
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsHere = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsHere = false;
    }
}
