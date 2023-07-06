using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum ItemType
    {
        Health,Coin
    }
    public ItemType Type;
    public int value = 20;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Type == ItemType.Health)
            {
                other.gameObject.GetComponent<Character>().addHealth(value);
                Destroy(this.gameObject);
            }
            
        }
    }
}
