using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace CustomInputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private List<Component> components = new List<Component>();
        private InputPackage inputPackage;

        private void Reset()
        {
            components = FindObjectsOfType<GameObject>()
                .SelectMany(go => go.GetComponents<IManageInput>())
                .Where(i => i != null)
                .Cast<Component>()
                .ToList();
        }

        void Start()
        {
            inputPackage = new InputPackage();
        }

        void Update()
        {
            inputPackage.Reset();

            ReadInput(KeyCode.W, ref inputPackage.lookUp);
            ReadInput(KeyCode.S, ref inputPackage.lookDown);
            ReadInput(KeyCode.A, ref inputPackage.moveLeft);
            ReadInput(KeyCode.D, ref inputPackage.moveRight);
            if (Input.GetKeyDown(KeyCode.Space))
                CheckIfChangeGravity();
            foreach (var component in components)
            {
                ((IManageInput)component).ReceiveInput(inputPackage);
            }
        }

        private void CheckIfChangeGravity()
        {
            if((inputPackage.lookUp ? 1 : 0) + (inputPackage.lookDown ? 1 : 0) + (inputPackage.moveLeft ? 1 : 0) + (inputPackage.moveRight ? 1 : 0) == 1)
            {
                inputPackage.changeGravity = true;
            }
            else
            {
                inputPackage.Reset();
            }
        }

        private void ReadInput(KeyCode key, ref bool pressed)
        {
            if (Input.GetKey(key))
            {
                pressed = true;
            }
        }
    }
}

