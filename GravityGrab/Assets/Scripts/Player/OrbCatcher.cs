using PlayerFunctions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbCatcher : MonoBehaviour
{
    [SerializeField] private int orbsAbsorbed;
    
    private bool canCatch = true;
 
    private PlayerMovement playerMovement;

    void Start()
    {
        orbsAbsorbed = 0;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ResetOrbsAbsorbed()
    {
        orbsAbsorbed = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (canCatch)
        {
            if (collider.gameObject.CompareTag("Orb"))
            {
                orbsAbsorbed++;
                GetComponent<VFXPlayer>().PlayOrbSFX(orbsAbsorbed);
                CalculateNewGravity();
                StartCoroutine(collider.transform.parent.parent.gameObject.GetComponent<Orb>().DestroyOrb());
                try
                {
                    TutorialManager tut = GetComponent<TutorialManager>();
                    if (tut != null)
                    {
                        if (tut.currentStep == 2)
                            StartCoroutine(tut.GoToNextStep());
                    }
                }
                catch
                {
                    Debug.LogWarning("You are not in tutorial");
                }
            }
        }
    }

    private void CalculateNewGravity()
    {
        float newGravity = playerMovement.baseGravityMultiplier + Mathf.Pow(2,orbsAbsorbed/2);
        playerMovement.ChangeGravityMultiplier(newGravity);
    }

    public void StopCatching()
    {
        canCatch = false;
    }

    public void ResumeCatching()
    {
        canCatch = true;
    }
}
