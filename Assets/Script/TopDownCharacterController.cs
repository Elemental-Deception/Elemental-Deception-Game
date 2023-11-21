using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public GameObject PauseMenu;
        public string deathSceneName;
        public float speed;
        private Vector2 dir = Vector2.zero;
        private Animator animator;
        private PauseMenuController pauseMenuController;
        private Vector2 movement;
        bool flipped;

        private void Start()
        {
            animator = GetComponent<Animator>();
            pauseMenuController = PauseMenu.GetComponent<PauseMenuController>();
        }


        private void Update()
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
            dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
                flipped = movement.x < 0;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
                flipped = movement.x < 0;
            }
            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuController.PauseGame();
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));


            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
        /*private void FixedUpdate()
        {
            if(dir != Vector2.zero)
            {
                var xMovement = movement.x * speed * Time.deltaTime;
                this.transform.Translate(new Vector3(xMovement, 0), Space.World);
            }
        }*/
    }
}
