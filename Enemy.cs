using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    public CharacterStats myStats;
    //testing below
    //Interactable interact;    
    
    void Start()
    {
        
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        //interact = GetComponent<Interactable>();
    }
    public override void Interact()
    {
        base.Interact();

        //attack the enemy
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        //CharacterStats playerCombat = playerManager.player.GetComponent<CharacterStats>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }

    }
}
