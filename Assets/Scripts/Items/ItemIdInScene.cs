using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] [RequireComponent(typeof(Rigidbody2D))]
public class ItemIdInScene : MonoBehaviour
{
    [SerializeField] private int id = -1;
    
    public int Id { get => id; set => id = value; }

    private void Start()
    {
        if (!GetComponent<BoxCollider2D>().isTrigger)
        {
            throw new System.Exception("BoxCollider2D needs to be Trigger");
        }
        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            throw new System.Exception("Rigidbody2D needs to be Kinematic");
        }
    }
}
