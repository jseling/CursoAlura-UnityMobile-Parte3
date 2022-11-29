using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReservavel
{
    void SetReserva(IReserva reserva);
    void AoEntrarNaReserva();
    void AoSairDaReserva();
}
