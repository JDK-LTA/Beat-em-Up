using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxTimer : Singleton<AuxTimer>
{
    public delegate void OnUpdate(float delta);
    public static event OnUpdate AuxUpdate;

    //public delegate void OnStart();
    //public event OnStart AuxStart;

    //void Start()
    //{
    //    AuxStart();
    //}

    void Update()
    {
        if (AuxUpdate != null)
        {
            AuxUpdate(Time.deltaTime);
        }
    }
}
