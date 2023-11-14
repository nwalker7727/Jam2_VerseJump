using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class EnemyScript : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    [field: SerializeField] 
    public EventReference thanosSoundEvent { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!thanosSoundEvent.IsNull)
            {
                EventInstance eventInstance = RuntimeManager.CreateInstance(thanosSoundEvent);
                eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform)); // Set 3D position.
                eventInstance.start();

                // Release the FMOD event instance when it's no longer needed.
                eventInstance.release();
            }
        Destroy(gameObject);
    }
}
