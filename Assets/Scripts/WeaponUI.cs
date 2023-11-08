using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Transform weaponTransform;
    [SerializeField] Image image;
    [SerializeField] TMP_Text name, ammo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject weapon = weaponTransform.GetChild(SelectWeapon.selectIndex).gameObject;
        WeaponScriptableObject weaponSO = weapon.GetComponent<Weapon>().weaponSO;
        //weaponSO = weapon.
        //WeaponScriptableObject objSO = obj.GetComponent<WeaponScriptableObject>();
     
        image.sprite = weaponSO.image;
        name.SetText(weaponSO.name);
        ammo.SetText(weaponSO.currentAmmo + " / " + weaponSO.magazine);

    }
}
