using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    
    private Vector3 _nextPosition;
    private const float DistanceThreshold = 0.1f;

    private void Start()
    {
        _nextPosition = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _nextPosition, 
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _nextPosition) <= DistanceThreshold)
        {
            _nextPosition = _nextPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.PlayerTag))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.PlayerTag))
        {
            collision.transform.SetParent(null);
        }
    }
}
