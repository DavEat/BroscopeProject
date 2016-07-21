using UnityEngine;
using System;

public class FliersInfo : MonoBehaviour {

    public int id;

    void Awake()
    {
        id = (int)Char.GetNumericValue(transform.name[0]);
    }
}
