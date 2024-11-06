using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                inputPackage.changeGravity = true;
            foreach (var component in components)
            {
                ((IManageInput)component).ReceiveInput(inputPackage);
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

