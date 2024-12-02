using CustomInputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IManageInput
{
    public List<TutorialText> tutorialSteps;
    public int currentStep = 0;

    public GameObject orb;
    public GameObject key;
    public GameObject portal;

    public void ReceiveInput(InputPackage input)
    {
        if ((input.moveLeft || input.moveRight) && currentStep == 0)
        {
            StartCoroutine(GoToNextStep());
        }
        else if (input.changeGravity && currentStep == 1)
        {
            StartCoroutine(GoToNextStep());
        }
    }

    void OnTriigerCollision2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Key") && currentStep == 3)
        {
            StartCoroutine(GoToNextStep());
        }
    }


    public IEnumerator GoToNextStep()
    {
        currentStep++;
        yield return new WaitForSeconds(0.5f);
        tutorialSteps[currentStep-1].HideText();
        yield return new WaitForSeconds(0.5f);
        tutorialSteps[currentStep].ShowText();

        if (currentStep == 2)
        {
            orb.SetActive(true);
        }
        else if (currentStep == 3)
        {
            key.SetActive(true);
            portal.SetActive(true);
        }
    }
}
