using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetOpacity(float opacity)
    {
        GetComponent<CanvasGroup>().alpha = opacity;
    }
}
