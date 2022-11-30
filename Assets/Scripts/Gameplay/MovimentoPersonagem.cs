using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    public Vector3 Direcao { get; protected set; }

    void Awake ()
    {
        meuRigidbody = GetComponent<Rigidbody>();
        //this.Direcao = Vector3.forward;
    }

    public void SetDirecao(Vector2 direcao)
    {
        this.Direcao = new Vector3(direcao.x, 0, direcao.y);
    }

    public void SetDirecao(Vector3 direcao)
    {
        //if(direcao != Vector3.zero && direcao.magnitude > 0.05)
        if (direcao.sqrMagnitude > 0.05)
        {
            this.Direcao = direcao.normalized;
            this.Direcao = new Vector3(Direcao.x, 0, Direcao.z);
        }
        else
        {
            this.Direcao = Vector3.forward;
        }
    }

    public void Movimentar (float velocidade)
    {
        meuRigidbody.MovePosition(
                meuRigidbody.position +
                Direcao * velocidade * Time.deltaTime);

    }

    public void Rotacionar (Vector3 direcao)
    {
        //if (direcao != Vector3.zero)
        if (direcao.sqrMagnitude > 0.05)
        {
            var direcaoNormalizada = direcao.normalized;
            direcaoNormalizada.y = 0;
            Quaternion novaRotacao = Quaternion.LookRotation(direcaoNormalizada);
            meuRigidbody.MoveRotation(novaRotacao);
        }
    }

    public void Morrer ()
    {
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        meuRigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void Reiniciar()
    {
        meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = true;
        //this.Direcao = Vector3.forward;
    }
}
