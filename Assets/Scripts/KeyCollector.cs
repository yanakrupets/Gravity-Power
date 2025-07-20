using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] private Vector2 keyPosition;
    
    private const string KeyTag = "Key";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(KeyTag)) 
        {
            other.transform.SetParent(transform);
            other.transform.localPosition = keyPosition;
        }
    }
}
