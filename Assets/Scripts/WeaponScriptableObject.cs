using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName;
    public GameObject model;
    public float damage;
    public float fireRate;
    public int currentAmmo;
    public float reloadSpeed;
    public int ammoCapacity;
    public int magazine;
    public bool isReloading;
    public float maxDistance;
    public Sprite image;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;
    
}
