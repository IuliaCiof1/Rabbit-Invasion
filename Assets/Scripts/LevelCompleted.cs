using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Debug.Log(audioSource.isPlaying);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.PlayOneShot(clip);
            StartCoroutine(Wait());

            //SceneManager.LoadScene(0);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => audioSource.isPlaying==false);
        SceneManager.LoadScene(0);
    }
}
