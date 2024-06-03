using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    public string scene;


    void Start() // Músicas do jogo
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        StartCoroutine(startMusic());

    }

    private IEnumerator startMusic()
    {
        switch (scene)
        {
            case "GameStart":
                yield return new WaitForSeconds(1f);
                audioSource.Play();
                break;
            case "Menu":
                yield return new WaitForSeconds(1f);
                audioSource.Play();
                break;
            case "Intro":
                yield return new WaitForSeconds(2f);
                audioSource.Play();
                break;

        }
    }

}
