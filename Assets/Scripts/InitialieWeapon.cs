using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialieWeapon : MonoBehaviour
{
    [SerializeField] WeaponScriptableObject[] weaponInit;
    [SerializeField] WeaponScriptableObject[] weapon;


    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < weapon.Length; i++)
        {
            weapon[i].currentAmmo = weaponInit[i].currentAmmo;
            weapon[i].magazine = weaponInit[i].magazine;
        }
    }
    
}
