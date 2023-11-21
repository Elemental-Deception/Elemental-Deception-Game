﻿using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        private Vector2 dir = Vector2.zero;
        private Animator animator;
        public Camera cam;
        public Rigidbody2D rb;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Calculate the direction based on input
            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // Set animator parameters
            animator.SetBool("IsMoving", dir != Vector2.zero);
            if (dir != Vector2.zero)
            {
                animator.SetFloat("Horizontal", dir.x);
                animator.SetFloat("Vertical", dir.y);

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
