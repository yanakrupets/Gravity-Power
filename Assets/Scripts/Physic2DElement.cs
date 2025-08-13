using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physic2DElement : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public event Action OnGravityChanged;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    
    public void GravityChanged()
    {
        OnGravityChanged?.Invoke();
    }
}