using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponScriptableObject weaponSO;
    float timeSinceLastShot;
    Rigidbody player;
    float distanceToMouse;
    Texture2D cursorTextureDisable, cursorTextureEnable;
    
    bool cursorEnabled = true; //used to refrain from seting the cursor every frame;
    bool cursorDisabled = false;

    AudioSource audioSource;
    ParticleSystem muzzleFlash;


    private void Start()
    {
        muzzleFlash = transform.GetChild(0).GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").GetComponent<Rigidbody>();

        PlayerShoot.shootEvent += Shoot;
        PlayerShoot.reloadEvent += StartReloading;

        cursorTextureEnable = Resources.Load<Texture2D>("Cursor/CursorEnable");
        cursorTextureDisable = Resources.Load<Texture2D>("Cursor/CursorDisable");

        audioSource = GetComponent<AudioSource>();
        //Debug.Log(player.position + " " + player.gameObject.transform.position);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        

        //Change cursor texture
        if (CanShoot() && !cursorEnabled)
        {
            //Debug.Log(CanShoot() + " 1 " + cursorEnabled);
            Cursor.SetCursor(cursorTextureEnable, new Vector2(16, 16), CursorMode.ForceSoftware);
            cursorEnabled = true;
        }
        else if (!CanShoot() && cursorEnabled)
        {
            //Debug.Log(CanShoot() + " 2 " + cursorEnabled);
            Cursor.SetCursor(cursorTextureDisable, new Vector2(16, 16), CursorMode.ForceSoftware);
            cursorEnabled = false;
        }



        //Debug.DrawLine(transform.position, transform.forward* Vector3.Distance(GetMousePos(), transform.position), Color.green);
        //Debug.DrawLine(transform.position, transform.forward * Vector3.Distance(GetMousePos(), transform.position), Color.green);
        
         Debug.DrawLine(transform.position, transform.position + transform.forward * 1000f, Color.green);
        Debug.DrawLine(player.position, player.transform.forward * weaponSO.maxDistance, Color.red);
        //Debug.DrawLine(player.position + new Vector3(0, 0.3f, 0), player.position + (GetMousePos() - player.position).normalized * 7, Color.red);
        //Debug.DrawRay(player.position, player.transform.forward, Color.red);
        //Physics.Raycast(player.position+new Vector3(0,0.3f,0), player.transform.forward, out RaycastHit hitInfo, distanceToMouse);
        //Debug.Log(hitInfo.transform.name);
    }



    public void StartReloading()
    {
        if (gameObject.activeSelf && !weaponSO.isReloading && weaponSO.magazine>0)
        {
            StartCoroutine(Reloading());
           
        }
    }

    public IEnumerator Reloading()
    {
        weaponSO.isReloading = true;
        weaponSO.currentAmmo = weaponSO.ammoCapacity;
        weaponSO.magazine--;
        audioSource.PlayOneShot(weaponSO.reloadAudio);

        yield return new WaitForSeconds(weaponSO.reloadSpeed);

        weaponSO.isReloading = false;
    }

    private bool CanShoot()
    {
        distanceToMouse = Vector3.Distance(GetMousePos(), transform.position);

        if (distanceToMouse < weaponSO.maxDistance)
        {
            if (weaponSO.currentAmmo > 0)
            {

                //fireRate = nr. of bullets per minute | fireRate/60 = nr. bullets per second
                //1 second / (fireRate/60) = time between bullets shot
                if (!weaponSO.isReloading && timeSinceLastShot > 1f / (weaponSO.fireRate / 60))
                    return true;
            }
        }

        return false;
    }

    public void Shoot()
    {
       //Debug.Log(gameObject.name + " " + gameObject.activeSelf);
        if (gameObject.activeSelf && CanShoot())
        {
            muzzleFlash.Play();
            
            //Debug.Log("shooting");
            //Debug.DrawRay(player.position, player.transform.forward, Color.green, distanceToMouse);
            if (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out RaycastHit hitInfo, distanceToMouse))
            {
                Debug.Log(hitInfo.transform.name);
                Damageable damageable = hitInfo.transform.GetComponent<Damageable>();
                damageable.TakeDamage(weaponSO.damage);
            }

            weaponSO.currentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }
        
    }

    //Mouse position in world space
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.y = 0;

        return mousePos+Vector3.one; //vector3.one helps with aim
    }

    private void OnGunShot()
    {
        audioSource.PlayOneShot(weaponSO.shootAudio);
    }
}