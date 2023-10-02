using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Simulator.OnNewPlayer += MyMethod;
    }

    private void MyMethod(string arg1, string arg2, DateTime time)
    {
        throw new NotImplementedException();
    }
    //https://docs.unity3d.com/es/530/Manual/UnityWebRequest.html

}
