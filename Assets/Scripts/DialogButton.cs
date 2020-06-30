using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
