using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private static event Action OnItemDrag;
    [SerializeField] private static event Action OnScreenSwipe;

    public static void OnItemDragInvoke()
    {
        OnItemDrag?.Invoke();
    }
    public static void OnScreenSwipeInvoke()
    {
        OnScreenSwipe?.Invoke();
    }
}
