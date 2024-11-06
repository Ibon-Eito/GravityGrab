using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInputs
{
    public class InputPackage
    {
        public bool lookUp;
        public bool lookDown;
        public bool moveRight;
        public bool moveLeft;
        public bool changeGravity;

        public void Reset()
        {
            lookUp = false;
            lookDown = false;
            moveRight = false;
            moveLeft = false;
            changeGravity = false;
        }
    }
}

