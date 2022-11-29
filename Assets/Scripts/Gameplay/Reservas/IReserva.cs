using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReserva 
{
    void DevolverObjeto(GameObject objeto);
    GameObject PegarObjeto();
    bool TemObjeto();
}
