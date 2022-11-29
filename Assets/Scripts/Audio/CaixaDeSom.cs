using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CaixaDeSom : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] listaDeAudio;

    private AudioSource saidaAudio;

    private void Awake()
    {
        saidaAudio = GetComponent<AudioSource>();
    }

    public void Tocar()
    {
        var sorteado = Random.Range(0, listaDeAudio.Length);
        saidaAudio.PlayOneShot(listaDeAudio[sorteado]);
    }
}
