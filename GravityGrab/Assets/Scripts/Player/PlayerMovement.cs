using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInputs;

namespace PlayerFunctions
{
    public class PlayerMovement : MonoBehaviour, IManageInput
    {
        [SerializeField] private float moveSpeed;
        public ParticleSystem dustPsRight;
        public ParticleSystem dustPsLeft;

        private SpriteRenderer spriteRenderer;
        private Animator playerAnim;
        private ConstantForce2D gravityForce;

        private bool lastMoveLeft = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerAnim = GetComponent<Animator>();
            gravityForce = GetComponent<ConstantForce2D>();
        }

        public void ReceiveInput(InputPackage input)
        {
            if (input.moveRight)
            {
                MovePlayer(false);
            }
            else if (input.moveLeft)
            {
                MovePlayer(true);
            }
            else
            {
                playerAnim.SetBool("Walk", false);
                if (input.changeGravity)
                {
                    playerAnim.SetBool("Fly", true);
                    if (transform.rotation.x == 0)
                    {
                        transform.Rotate(new Vector3(180, 0, 0));
                        gravityForce.force = new Vector2(0, 18);
                    }
                    else
                    {
                        Debug.Log("Hola");
                        transform.Rotate(new Vector3(-180, 0, 0));
                        gravityForce.force = new Vector2(0, 0);
                    }
                       
                }
            }
        }

        private void MovePlayer(bool left)
        {
            playerAnim.SetBool("Fly", false);
            playerAnim.SetBool("Walk", true);
            if (left != lastMoveLeft)
            {
                ChangeWalkDirection();
            }
            Vector3 direction;
            if (!left)
                direction = Vector3.right;
            else
                direction = Vector3.left;

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            lastMoveLeft = left;
        }

        private void ChangeWalkDirection()
        {
            if (spriteRenderer.flipX)   // Changing to right
            {
                spriteRenderer.flipX = false;
                CreateDustRight();
            }
            else
            {
                spriteRenderer.flipX = true;
                CreateDustLeft();
            }

            
        }

        private void CreateDustRight()
        {
            dustPsRight.Play();
        }

        private void CreateDustLeft()
        {
            dustPsLeft.Play();
        }
    }
}
