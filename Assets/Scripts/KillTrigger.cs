using UnityEngine;
using UnityEngine.SceneManagement;

public class KillTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}