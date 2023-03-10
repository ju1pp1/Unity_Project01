using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EquipmentManager;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            attackSpeed.AddModifier(newItem.attackSpeedModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            attackSpeed.AddModifier(oldItem.attackSpeedModifier);
        }
    }
    public override void Die()
    {
        base.Die();
        //Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
