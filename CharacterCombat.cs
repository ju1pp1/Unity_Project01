using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    Stat stat;
    //public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    const float combatCooldown = 5;
    float lastAttackTime;
    CharacterStats mystats;
    public event System.Action OnAttack;
    public bool inCombat { get; private set; }
    
    void Start()
    {
        mystats = GetComponent<CharacterStats>();
    }
    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat= false;
        }
        
    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            //targetStats.TakeDamage(mystats.damage.GetValue());
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();
            
            //Debug.Log(attackCooldown = 1f / mystats.attackSpeed.GetValue());
            attackCooldown = 1f / mystats.attackSpeed.GetValue();

            inCombat= true;
            lastAttackTime= Time.time;
        }

    }
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(mystats.damage.GetValue());
        if (stats.currentHealth <= 0)
        {
            inCombat= false;
        }
    }

    /*
    public void Attack (CharacterStats targetStats)
    {
        
        if (attackCooldown <= 0f)
        {
            //targetStats.TakeDamage(mystats.damage.GetValue());
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if(OnAttack != null)
                OnAttack();

                attackCooldown = 1f / attackSpeed;
            
        }
        
    }
    
    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds (delay);

        stats.TakeDamage(mystats.damage.GetValue());
    }
    */


}
