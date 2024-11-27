using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool isOpened = false;

    private Animator anim;
 
    void Start()
    {
        anim = GetComponent<Animator>();

        if (isOpened)
            anim.SetTrigger("OpenAtStart");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CompleteLevel();
        }
    }

    public void OpenPortal()
    {
        anim.SetBool("Open",true);
    }

    public void SetIsOpened()
    {
        isOpened = true;
    }
}
