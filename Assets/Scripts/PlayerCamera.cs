using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField] private bool autoScroll;
    [SerializeField] private float cameraScrollSpeed;
    [SerializeField] private List<float3> path = new();

    private int _index;
    private Transform _player;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if(!_player) Debug.LogError("Player not found");
    }

    private void Start() {
        transform.position = path[0];
    }

    private void Update() {
        if(!autoScroll || _index >= path.Count) return;
        if (Vector2.Distance(transform.position, (Vector3) path[_index]) < 0.001f) _index++;
    }

    private void FixedUpdate() {
        if(!autoScroll || _index >= path.Count) return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, path[_index], cameraScrollSpeed * Time.deltaTime);
    }

    private void LateUpdate() {
        if(autoScroll) return;
        var x = Mathf.Clamp(_player.position.x, path[0].x, path[^1].x);
        transform.localPosition = new float3(x, transform.position.y, 0f);
    }

    private void OnDrawGizmosSelected() {
        if(path.Count <= 1) return;
        
        for (var i = 0; i < path.Count - 1; i++) {
            Handles.color = Color.red;
            Handles.DrawWireDisc(path[i], transform.forward, 0.25f);
            Handles.DrawWireDisc(path[i + 1], transform.forward, 0.25f);
            
            Handles.color = Color.green;
            Handles.DrawLine(path[i], path[i + 1]);
        }
    }
}
