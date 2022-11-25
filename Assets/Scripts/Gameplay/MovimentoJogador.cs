using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private AudioSource audioPasso;
    public void AudioPasso()
    {
        audioPasso.Play();
    }
    public void RotacaoJogador ()
    {

            Vector3 posicaoMiraJogador = Direcao;

            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);

    }
}
