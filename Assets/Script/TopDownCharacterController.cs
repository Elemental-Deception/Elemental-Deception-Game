using UnityEngine;

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
