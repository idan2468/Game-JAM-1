using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItemSorting : MonoBehaviour
{
    private int originalSort;
    private Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        originalSort = canvas.sortingOrder;
    }

    public void SetInFront()
    {
        canvas.sortingOrder = 20;
    }

    public void UnsetInFront()
    {
        canvas.sortingOrder = originalSort;
    }
}
