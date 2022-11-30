using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private CaixaDeSom audioPasso;
    public void AudioPasso()
    {
        audioPasso.Tocar();
    }
    public void RotacaoJogador ()
    {
        if(Direcao != Vector3.zero)
        {
            Vector3 posicaoMiraJogador = Direcao;
            posicaoMiraJogador.y = transform.position.y;
            Rotacionar(posicaoMiraJogador);
        }
    }
}
