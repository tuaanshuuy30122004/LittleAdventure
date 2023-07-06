using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public Character player;

    private bool gameISOver;
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(player.currentState == Character.CharacterState.Dead || player == null)
        {
            Debug.Log("GameisOver");
        }
    }
}
