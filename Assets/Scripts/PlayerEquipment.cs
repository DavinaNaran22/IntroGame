using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public bool gunEquipped = false;
    public bool knifeEquipped = false;

    // True if player has equipped a gun
    public void EquipGun()
    {
        gunEquipped = true;
    }

    // True if player has equipped a knife
    public void EquipKnife()
    {
        knifeEquipped = true;
    }

    // True if player has equipped a gun or knife
    public bool IsWeaponEquipped()
    {
        return gunEquipped || knifeEquipped;
    }

}
