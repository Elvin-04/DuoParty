using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    private float _zoom;
    private float _zoomMultiplier = 4f;
    private float _minZoom = 2f;
    private float _maxZoom = 8f;
    private float _velocity = 0f;
    private float _smoothTime = 0.25f;

    [SerializeField]private Camera _camera;


    // Start is called before the first frame update
    void Start()
    {
        _zoom = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _zoom -= scroll * _zoomMultiplier;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _zoom, ref _velocity, _smoothTime);
    }
}
