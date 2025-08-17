using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private bool isActive = true;
    
    private Vector3 _nextPosition;
    private const float DistanceThreshold = 0.1f;

    private void Start()
    {
        _nextPosition = pointB.position;
    }

    private void Update()
    {
        if (!isActive)
            return;
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _nextPosition, 
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _nextPosition) <= DistanceThreshold)
        {
            _nextPosition = _nextPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    public void Activate(bool isOn)
    {
        isActive = isOn;
    }
}
