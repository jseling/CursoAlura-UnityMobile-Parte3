using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BotaoAnalogico : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform imagemFundo;

    [SerializeField]
    private RectTransform imagemBolinha;

    [SerializeField]
    private MeuEventoDinamicoVector2 onChange;


    public void OnDrag(PointerEventData eventData)
    {
        var posicaoMouse = CalcularPosicaoMouse(eventData);
        var posicaoLimitada = LimitarPosicao(posicaoMouse);
        PosicionarJoystick(posicaoLimitada);
        onChange.Invoke(posicaoLimitada);
    }

    private Vector2 LimitarPosicao(Vector2 posicaoMouse)
    {
        var posicaoLimitada = posicaoMouse / TamanhoImagem();

        if(posicaoLimitada.magnitude > 1)
        {
            posicaoLimitada.Normalize();
        }

        return posicaoLimitada;
    }

    private float TamanhoImagem()
    {
        return imagemFundo.rect.width / 2;
    }

    private void PosicionarJoystick(Vector2 posicaoMouse)
    {
        imagemBolinha.localPosition = posicaoMouse * TamanhoImagem();
        Debug.Log(posicaoMouse);
    }

    private Vector2 CalcularPosicaoMouse(PointerEventData eventData)
    {
        Vector2 posicao;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imagemFundo,
            eventData.position,
            eventData.enterEventCamera,
            out posicao
            );

        return posicao;
    }
}

[Serializable]
public class MeuEventoDinamicoVector2 : UnityEvent<Vector2> { }