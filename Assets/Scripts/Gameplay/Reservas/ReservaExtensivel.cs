using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaExtensivel : MonoBehaviour, IReserva
{
    [SerializeField]
    private GameObject prefab;

    private Stack<GameObject> reserva;

    private void Awake()
    {
        reserva = new Stack<GameObject>();
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
        if (reserva.Count<=0)
        {
            CriarNovoObjeto();
        }

        var objeto = reserva.Pop();
        objeto.GetComponent<IReservavel>().AoSairDaReserva();


        return objeto;
    }

    public bool TemObjeto()
    {
        return true;
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
