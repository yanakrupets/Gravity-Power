using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] private Vector2 keyPosition;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Consts.KeyTag)) 
        {
            other.transform.SetParent(transform);
            other.transform.localPosition = keyPosition;
            other.transform.localRotation = Quaternion.identity;
        }
    }
}
