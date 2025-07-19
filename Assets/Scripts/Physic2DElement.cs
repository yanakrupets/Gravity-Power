using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physic2DElement : MonoBehaviour
{
    private Rigidbody2D _rb;

    public Rigidbody2D Rb => _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}