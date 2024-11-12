using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInputs;
using DG.Tweening;
using System;

namespace PlayerFunctions
{
    public enum PlayerGravity
    {
        Left, Right, Up, Down
    }

    public enum Actions
    {
        MoveRight, MoveLeft, LookUp, LookDown, Nothing
    }

    public class PlayerMovement : MonoBehaviour, IManageInput
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravityMultiplier;
        public ParticleSystem dustPsRight;
        public ParticleSystem dustPsLeft;

        private SpriteRenderer spriteRenderer;
        private Animator playerAnim;
        //private ConstantForce2D gravityForce;

        private bool lastMoveLeft = false;
        private PlayerGravity playerGravity = PlayerGravity.Down;
        private bool flying = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerAnim = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                flying = false;
                playerAnim.SetBool("Fly", false);
            }
                
        }

        public void ReceiveInput(InputPackage input)
        {
            if (input.changeGravity)
            {
                ChangeGravity(input);
            }

            Actions currentAction = CheckAction(input);

            switch (currentAction)
            {
                case Actions.MoveRight:
                    MovePlayer(false);
                    break;
                case Actions.MoveLeft:
                    MovePlayer(true);
                    break;
                case Actions.LookDown:
                    LookDown();
                    break;
                case Actions.LookUp:
                    LookUp();
                    break;
                case Actions.Nothing:
                    playerAnim.SetBool("Fly", flying);
                    playerAnim.SetBool("LookUp", false);
                    playerAnim.SetBool("LookDown", false);
                    playerAnim.SetBool("Walk", false);
                    break;
            }

        }

        private Actions CheckAction(InputPackage input)
        {
            if (!input.changeGravity)
            {
                switch (playerGravity)
                {
                    case PlayerGravity.Down:
                        if (input.moveRight)
                            return Actions.MoveRight;
                        else if (input.moveLeft)
                            return Actions.MoveLeft;
                        else if (input.lookUp)
                            return Actions.LookUp;
                        else if (input.lookDown)
                            return Actions.LookDown;
                        else
                            return Actions.Nothing;

                    case PlayerGravity.Up:
                        if (input.moveRight)
                            return Actions.MoveLeft;
                        else if (input.moveLeft)
                            return Actions.MoveRight;
                        else if (input.lookUp)
                            return Actions.LookDown;
                        else if (input.lookDown)
                            return Actions.LookUp;
                        else
                            return Actions.Nothing;

                    case PlayerGravity.Left:
                        if (input.moveRight)
                            return Actions.LookUp;
                        else if (input.moveLeft)
                            return Actions.LookDown;
                        else if (input.lookUp)
                            return Actions.MoveLeft;
                        else if (input.lookDown)
                            return Actions.MoveRight;
                        else
                            return Actions.Nothing;

                    case PlayerGravity.Right:
                        if (input.moveRight)
                            return Actions.LookDown;
                        else if (input.moveLeft)
                            return Actions.LookUp;
                        else if (input.lookUp)
                            return Actions.MoveRight;
                        else if (input.lookDown)
                            return Actions.MoveLeft;
                        else
                            return Actions.Nothing;

                    default:
                        return Actions.Nothing;
                }
            }
            else
                return Actions.Nothing;  
        }

        private void MovePlayer(bool left)
        {
            playerAnim.SetBool("Fly", flying);
            playerAnim.SetBool("LookUp", false);
            playerAnim.SetBool("LookDown", false);
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

        public void LookUp()
        {
            playerAnim.SetBool("Fly", flying);
            playerAnim.SetBool("LookUp", true);
            playerAnim.SetBool("LookDown", false);
            playerAnim.SetBool("Walk", false);
        }

        public void LookDown()
        {
            playerAnim.SetBool("Fly", flying);
            playerAnim.SetBool("LookUp", false);
            playerAnim.SetBool("LookDown", true);
            playerAnim.SetBool("Walk", false);
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

        private void ChangeGravity(InputPackage input)
        {
            flying = true;
            playerAnim.SetBool("Fly", flying);
            playerAnim.SetBool("LookUp", false);
            playerAnim.SetBool("LookDown", false);
            playerAnim.SetBool("Walk", false);

            if (input.moveRight && playerGravity != PlayerGravity.Right)
            {
                ChangeGravityToDirection(PlayerGravity.Right, new Vector2(1,0) * gravityMultiplier, new Vector3(0,0,90));
            }
            if (input.moveLeft && playerGravity != PlayerGravity.Left)
            {
                ChangeGravityToDirection(PlayerGravity.Left, new Vector2(-1, 0) * gravityMultiplier, new Vector3(0, 0, -90));
            }
            if (input.lookUp && playerGravity != PlayerGravity.Up)
            {
                ChangeGravityToDirection(PlayerGravity.Up, new Vector2(0, 1) * gravityMultiplier, new Vector3(0, 0, 180));
            }
            if (input.lookDown && playerGravity != PlayerGravity.Down)
            {
                ChangeGravityToDirection(PlayerGravity.Down, new Vector2(0, -1) * gravityMultiplier, new Vector3(0, 0, 0));
            }
        }

        private void ChangeGravityToDirection(PlayerGravity pG, Vector2 gravDir, Vector3 rotDir)
        {
            playerGravity = pG;
            Physics2D.gravity = gravDir;
            transform.DORotate(rotDir, 0.1f).Play();
        }

    }
}
