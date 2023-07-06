using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    private Collider damageCasterCollider;
    public int damage = 30;
    public string TargetTag;
    private List<Collider> targetsList;

    private void Awake()
    {
        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
        targetsList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TargetTag && !targetsList.Contains(other))
        {
            Character targetCC = other.GetComponent<Character>();
            if(targetCC != null)
            {
                targetCC.ApplyDamage(damage);
                PlayerVFXManager playerVFX = transform.parent.GetComponent<PlayerVFXManager>();
                if(playerVFX != null)
                {
                    RaycastHit hit;
                    Vector3 orignalPos = transform.position + (damageCasterCollider.bounds.extents.z) * transform.forward;
                    bool isHit = Physics.BoxCast(orignalPos, damageCasterCollider.bounds.extents/2 , transform.forward, out hit,transform.rotation,damageCasterCollider.bounds.extents.z, 1<<6);
                    if(isHit)
                    {
                        playerVFX.PlayerSlash();
                        Debug.Log("Play VFX");
                    }
                }
            }
            targetsList.Add(other);
        }
        
    }
    public void EnableDamageCaster()
    {
        targetsList.Clear();
        damageCasterCollider.enabled=true;
    }
    public void DisnableDamageCaster()
    {
        targetsList.Clear();
        damageCasterCollider.enabled = false;
    }

   
}
