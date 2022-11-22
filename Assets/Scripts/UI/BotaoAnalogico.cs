using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoAnalogico : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform imagemFundo;

    [SerializeField]
    private RectTransform imagemBolinha;
    public void OnDrag(PointerEventData eventData)
    {
        var posicaoMouse = CalcularPosicaoMouse(eventData);
        PosicionarJoystick(posicaoMouse);
    }

    private void PosicionarJoystick(Vector2 posicaoMouse)
    {
        imagemBolinha.localPosition = posicaoMouse;
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
