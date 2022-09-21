using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundViewModel : MonoBehaviour
{
    public static BackgroundViewModel I { get; set; }

    public List<GameObject> BackgroundView;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
