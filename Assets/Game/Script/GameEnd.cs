using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject Gamefinish;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Gamefinish.SetActive(true);
        }
    }
}
