﻿using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public GameObject PauseMenu;
        public float speed;
        private Vector2 dir = Vector2.zero;
        private Animator animator;
        private PauseMenuController pauseMenuController;
        public Camera cam;
        public Rigidbody2D rb;
        public string deathSceneName;
        private Vector2 movement;
        bool flipped;

        private void Start()
        {
            animator = GetComponent<Animator>();
            pauseMenuController = PauseMenu.GetComponent<PauseMenuController>();
        }

        private void Update()
        {
            // Calculate the direction based on input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuController.PauseGame();
            }

            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;

            if(dir == Vector2.zero)
            {
                rb.velocity = new Vector2(0.001f,0f);
            }
            // Set animator parameters
            animator.SetBool("IsMoving", dir != Vector2.zero);
            if (dir != Vector2.zero)
            {

                // Flip sprite based on direction
                if (dir.x != 0)
                {
                    transform.localScale = new Vector3(Mathf.Sign(dir.x) * 0.2f, 0.2f, 1f);
                }
            }
        }

        private void FixedUpdate()
        {
            // Apply the velocity in FixedUpdate
            rb.velocity = speed * dir;
        }
    }
}
