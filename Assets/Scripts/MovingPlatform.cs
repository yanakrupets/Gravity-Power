using UnityEngine;

public class MovingPlatform : MoveBetweenTwoPoints
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.PlayerTag) 
            || collision.gameObject.CompareTag(Consts.CrateTag))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.PlayerTag)
            || collision.gameObject.CompareTag(Consts.CrateTag))
        {
            if (this == null || collision.transform == null || gameObject.activeInHierarchy == false)
                return;
            
            collision.transform.SetParent(null);
        }
    }
}
