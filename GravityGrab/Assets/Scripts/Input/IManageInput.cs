using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInputs
{
    public interface IManageInput
    {
        public void ReceiveInput(InputPackage input);
    }

}
