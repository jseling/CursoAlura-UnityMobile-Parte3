using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel, IReservavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    public GameObject KitMedicoPrefab;
    public Slider sliderVidaChefe;
    public Image ImagelSlider;
    public Color CorDaVidaMaxima, CorDaVidaMinima;
    public GameObject ParticulaSangueZumbi;

    private IReserva reserva;

    public void SetReserva(IReserva reserva)
    {
        this.reserva = reserva;
    }

    private void Awake()
    {
        statusChefe = GetComponent<Status>();
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        agente = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;


        agente.speed = statusChefe.Velocidade;
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();
    }

    public void SetPosicao(Vector3 posicao)
    {
        transform.position = posicao;
        agente.Warp(posicao);
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true)
        {
            Vector3 direcao = jogador.position - transform.position;
            movimentoChefe.Rotacionar(direcao);

            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (estouPertoDoJogador)
            {
                animacaoChefe.Atacar(true);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador ()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if (statusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        //Destroy(gameObject, 2);
        Invoke("VoltarParaReserva", 2);
    }

    private void VoltarParaReserva()
    {
        reserva.DevolverObjeto(gameObject);
    }

    void AtualizarInterface ()
    {
        sliderVidaChefe.value = statusChefe.Vida;
        float porcentagemDaVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
        ImagelSlider.color = corDaVida;
    }

    public void AoEntrarNaReserva()
    {
        gameObject.SetActive(false);
        movimentoChefe.Reiniciar();
        this.enabled = true;
        agente.enabled = true;
        statusChefe.Vida = statusChefe.VidaInicial;
        SetPosicao(Vector3.zero);
    }

    public void AoSairDaReserva()
    {
        gameObject.SetActive(true);
    }
}
