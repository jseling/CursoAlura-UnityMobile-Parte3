using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaFixa : MonoBehaviour
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
        objeto.SetActive(false);
        reserva.Push(objeto);
    }

    public GameObject PegarObjeto()
    {
        var objeto = reserva.Pop();
        objeto.SetActive(true);
        return objeto;
    }

    public bool TemObjeto()
    {
        return reserva.Count > 0;
    }
}
