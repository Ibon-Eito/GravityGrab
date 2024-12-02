using CustomInputs;
using DG.Tweening;
using PlayerFunctions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IManageInput
{
    [SerializeField] private Ease ease;
    [SerializeField] private float tweenTime; 

    private RectTransform r;
    private PlayerMovement playerMovement;
    private PortalKey key;
    private Rigidbody2D rb;

    [SerializeField] private bool onPause = false;

    void Start()
    {
        r = GetComponent<RectTransform>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        key = FindObjectOfType<PortalKey>();
        rb = playerMovement.GetComponent<Rigidbody2D>();
    }

    public void ReceiveInput(InputPackage input)
    {
        if (input.pauseGame)
        {
            if (!onPause)
                PauseGame();
        }
    }

    private void PauseGame()
    {
        onPause = true;

        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.canMove = false;
        if (key != null) key.CanMove(false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        r.DOMoveY(-1000,tweenTime).SetRelative().SetEase(ease).Play();
    }

    public void ResumeGame()
    {
        onPause = false;

        EventSystem.current.SetSelectedGameObject(null);
        playerMovement.canMove = true;
        if (key != null) key.CanMove(true);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(Physics2D.gravity, ForceMode2D.Impulse);
        r.DOMoveY(1000, tweenTime).SetRelative().SetEase(ease).Play();
    }

}
