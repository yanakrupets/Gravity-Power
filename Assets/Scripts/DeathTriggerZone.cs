using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Consts.PlayerTag))
        {
            SoundController.Play(SoundType.Death);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
