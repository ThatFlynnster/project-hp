using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject tab;

    public void OnPointerClick(PointerEventData eventData)
    {
        tab.SetActive(false);
    }
}
