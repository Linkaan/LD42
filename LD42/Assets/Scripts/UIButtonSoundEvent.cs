using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    public bool onHover = true;

    private SoundPlayer player;

    void Start() {
        player = FindObjectOfType<SoundPlayer>();
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        if (onHover)
            player.PlayHover();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        player.PlayClick();
    }
}