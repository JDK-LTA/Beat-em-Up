using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenZone : MonoBehaviour
{
    [SerializeField] private float regenAmount = 5f;

    public float RegenAmount { get => regenAmount; }
}
