using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOrb : MonoBehaviour
{
    public float Speed = 3f;
    public int Damage = 20;
    public ParticleSystem HitVFX;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime );
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            return;
        }
        Character cc = other.gameObject.GetComponent<Character>();

        if( cc != null && cc.isPlayer )
        {
            cc.ApplyDamage(Damage);
        }
        Instantiate(HitVFX, transform.position, Quaternion.identity );
        Destroy(this.gameObject);
    }
}
