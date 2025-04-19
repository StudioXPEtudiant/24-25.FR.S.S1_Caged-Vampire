using UnityEngine;

public class KillTrigger : MonoBehaviour {
    [SerializeField] private GameManager manager;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")) return;
        manager.KillPlayer();
    }
}