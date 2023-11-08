using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Transform itemHandLocation;
    [SerializeField] AudioClip pickUpSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //transform.rotation *= Quaternion.Euler(Vector3.up);
    }

    private void OnTriggerEnter(Collider item)
    {
        

        if (item.tag == "Weapon")
        {
            item.transform.parent = itemHandLocation.transform;
            item.transform.position = itemHandLocation.position;
            item.transform.localScale = itemHandLocation.localScale;
            item.transform.rotation = itemHandLocation.rotation;

            item.gameObject.SetActive(false);
            audioSource.PlayOneShot(pickUpSound);
        }

        if(item.tag == "MedKit")
        {
            gameObject.GetComponent<PlayerController>().Heal(10);
            audioSource.PlayOneShot(pickUpSound);
            Destroy(item.gameObject);
        }

        if(item.tag == "Ammo")
        {
            foreach(Transform child in itemHandLocation)
            {
                child.GetComponent<Weapon>().weaponSO.magazine += 5;
            }
            audioSource.PlayOneShot(pickUpSound);
            Destroy(item.gameObject);

        }

    }
}
