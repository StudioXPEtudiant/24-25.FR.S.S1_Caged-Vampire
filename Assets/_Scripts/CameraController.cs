using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class CameraController : MonoBehaviour {
    [SerializeField] private bool autoScroll;
    [SerializeField] private float cameraScrollSpeed;

    private SplineContainer _splineContainer;
    
    private float _time;
    private Transform _player;

    private void Awake() {
        _splineContainer = GetComponent<SplineContainer>();
    }

    private void Start() {
        if (!_splineContainer || _splineContainer.Spline == null) return;
        transform.position = _splineContainer.Spline.EvaluatePosition(0f);
    }

    private void Update() {
        if(!autoScroll || _time >= 1) return;
        _time += cameraScrollSpeed / _splineContainer.CalculateLength() * Time.deltaTime;
        _time = math.clamp(_time, 0f, 1f);
    }

    private void FixedUpdate() {
        if(!autoScroll || _time >= 1) return;
        transform.position = _splineContainer.Spline.EvaluatePosition(_time);
    }

    private void LateUpdate() {
        if(autoScroll || !_player) return;
        
        float3 playerPos = _player.position;
        SplineUtility.GetNearestPoint(_splineContainer.Spline, playerPos, out var nearest, out _);
        transform.position = new float3(nearest.x, nearest.y, 0f);
    }
    
    public void SetupCamera(Transform player) {
        _player = player;
        _time = 0f;
    }
}
