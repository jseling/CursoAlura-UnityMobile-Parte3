using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaFixa : MonoBehaviour, IReserva
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int quantidade;

    private Stack<GameObject> reserva;

    private void Awake()
    {
        reserva = new Stack<GameObject> ();
        CriarTodosObjetos();
    }

    private void CriarTodosObjetos()
    {
        for(var i=0; i<quantidade; i++)
        {
            CriarNovoObjeto();
        }
    }

    private void CriarNovoObjeto()
    {
        var objeto = GameObject.Instantiate(prefab, transform);

        var controleObjeto = objeto.GetComponent<IReservavel>();
        controleObjeto.SetReserva(this);

        DevolverObjeto(objeto);
    }

    public void DevolverObjeto(GameObject objeto)
    {
        objeto.GetComponent<IReservavel>().AoEntrarNaReserva();
        reserva.Push(objeto);
    }

    public GameObject PegarObjeto()
    {
        var objeto = reserva.Pop();
        objeto.GetComponent<IReservavel>().AoSairDaReserva();
        return objeto;
    }

    public bool TemObjeto()
    {
        return reserva.Count > 0;
    }

    private void OnValidate()
    {
        var reservavel = this.prefab.GetComponent<IReservavel>();
        if (reservavel == null)
        {
            this.prefab = null;
            throw new Exception("Atributo [prefab] requer um objeto que contenha a implementação da interface [IReservavel]");
        }
    }
}
