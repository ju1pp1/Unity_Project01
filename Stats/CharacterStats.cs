using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat health;
    public Stat damage;
    public Stat armor;
    public Stat attackSpeed;

    public event System.Action<int, int> OnHealthChanged;
    
    //TODO:
    //create attackspeed stat here -- Equipment script -- EnemyController script -- Enemy script
    //public float attackSpeed = 1f;
    //public float attackCooldown = 0f;
    //public float attackDelay = .6f;
    //CharacterStats mystats;

    //public event System.Action OnAttack;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
      //  mystats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        //attackCooldown -= Time.deltaTime;
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        */
        
    }
    /*
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            //targetStats.TakeDamage(mystats.damage.GetValue());
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();
            
            attackCooldown = 1f / attackSpeed.GetValue();

        }

    }
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(mystats.damage.GetValue());
    }
    */
    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + "takes " + damage + " damage.");

        
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    public virtual void Die()
    {
        //Die in some way
        //This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
