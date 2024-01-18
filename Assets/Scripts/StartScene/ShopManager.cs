using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopOverlay shopOverlay;

    void Start()
    {
        this.shopOverlay.Init();
    }
}
