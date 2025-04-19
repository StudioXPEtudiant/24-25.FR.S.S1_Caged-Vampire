using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {
    [FormerlySerializedAs("playerCamera")] [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPoint;
    
    private GameObject _playerInstance;
    
    private void Awake() {
        SpawnPlayer();
    }
    
    public void KillPlayer() {
        Destroy(_playerInstance);
        SpawnPlayer();
    }

    private void SpawnPlayer() {
        _playerInstance = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        _playerInstance.name = "Player";
        
        cameraController.SetupCamera(_playerInstance.transform);
    }
}