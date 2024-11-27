using DG.Tweening;
using PlayerFunctions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Ease ease;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<VFXPlayer>().PlayDamageSFX();
        playerMovement.canMove = false;
        anim.SetBool("Fly", false);
        anim.SetBool("Dead", true);

        playerMovement.ChangeGravityMultiplier(playerMovement.baseGravityMultiplier);
        playerMovement.ChangeGravityToDirection(PlayerGravity.Down, new Vector2(0, -1), new Vector3(0, 0, 0));

        Sequence dieSequence = DOTween.Sequence();
        dieSequence.Append(spriteRenderer.DOColor(Color.red, 0.2f));
        dieSequence.Append(spriteRenderer.DOColor(Color.white, 0.2f));
        dieSequence.Append(spriteRenderer.DOColor(Color.red, 0.2f));
        dieSequence.Append(spriteRenderer.DOColor(Color.white, 0.2f));
        dieSequence.Append(spriteRenderer.DOColor(Color.red, 0.2f));
        dieSequence.Append(spriteRenderer.DOColor(Color.white, 0.2f));
        dieSequence.Insert(0, transform.DOMove(respawnPoint.position, 1.5f).SetEase(ease).OnComplete(() => EndDeath()));
        dieSequence.Play();

    }

    private void EndDeath()
    {
        playerMovement.canMove = true;
        anim.SetBool("Dead",false);
    }
}
