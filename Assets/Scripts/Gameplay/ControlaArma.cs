using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

    //public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;

    [SerializeField]
    private ReservaExtensivel reserva;
	
	// Update is called once per frame
	void Update () {

        var toquesNaTela = Input.touches;

        foreach (var toque in toquesNaTela)
        {
            if(toque.phase == TouchPhase.Began)
            {
                Atirar();
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);
            }
        }
	}

    private void Atirar()
    {
        //Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
        if (reserva.TemObjeto())
        {
            GameObject bala = reserva.PegarObjeto();
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
            
        }
    }
}
