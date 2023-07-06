using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
   public void DisableGate()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
