using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInputs;

namespace PlayerFunctions
{
    public class PlayerIdleAnimations : MonoBehaviour, IManageInput
    {
        private Animator playerAnim;

        void Start()
        {
            playerAnim = GetComponent<Animator>();
        }

        public void ReceiveInput(InputPackage input)
        {
            if (!input.changeGravity && input.lookUp)
            {
                playerAnim.SetBool("LookUp", true);
            }
                
            else if (!input.changeGravity && input.lookDown)
                playerAnim.SetBool("LookDown", true);
            else
            {
                playerAnim.SetBool("LookDown", false);
                playerAnim.SetBool("LookUp", false);
            }
        }
    }
}

