using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;
    private Character character;
    public float healthPercent
    {
        get { return (float)health / (float)maxHealth; }
    }
    private void Awake()
    {
        health = maxHealth;
        character = GetComponent<Character>();
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            character.SwitchStateTo(Character.CharacterState.Dead);
            character.DropItem();
            Destroy(character.gameObject, 2f);
        }
    }

    public void addHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
