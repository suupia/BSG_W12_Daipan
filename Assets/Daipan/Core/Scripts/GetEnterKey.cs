using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Core.Scripts
{
    public class GetEnterKey : Interfaces.IGetEnterKey
    {
        public bool GetEnterKeyDown()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.W)) return false;
                if (Input.GetKeyDown(KeyCode.A)) return false;
                if (Input.GetKeyDown(KeyCode.S)) return false;
                if (Input.GetKeyDown(KeyCode.Tab)) return false;

                return true;
            }
            return false;
        }
    }
}

namespace Daipan.Core.Interfaces
{
    public interface IGetEnterKey
    {
        public bool GetEnterKeyDown();
    }
}