using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomWeapon : MonoBehaviour
{
    [SerializeField] WeaponScriptableObject[] weapons;
    public WeaponScriptableObject weaponType;

    private void Start()
    {
        int randomInt = Random.Range(0, weapons.Length);
        Debug.Log("sad");
        GameObject scObject = Instantiate(weapons[randomInt].model, transform);
        //weapons[randomInt] = ScriptableObject.Instantiate(weapons[randomInt]);
        scObject.AddComponent<BoxCollider>().isTrigger=true;
        scObject.AddComponent<Weapon>().weaponSO = weapons[randomInt];
        
        scObject.AddComponent<AudioSource>();
       // weaponType = weapons[randomInt];
        scObject.tag = "Weapon";

    }
}
