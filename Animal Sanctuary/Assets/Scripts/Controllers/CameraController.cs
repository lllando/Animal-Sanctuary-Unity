using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Tilemap levelTilemap;

    [SerializeField]
    private Transform cameraTarget;

    [SerializeField]
    private float cameraSmoothSpeed;

    [SerializeField]
    private Vector2 cameraOffset;

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = this.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 desiredPos = (Vector2)cameraTarget.transform.position + cameraOffset;

        Bounds bounds = GameManager.PlayerController.mapBounds;

        float posX = Mathf.Clamp(desiredPos.x, bounds.min.x + 10, bounds.max.x - 7.5f);
        float posY = Mathf.Clamp(desiredPos.y, bounds.min.y + 10, bounds.max.y - 7.5f);

        Vector2 smoothPos = Vector2.Lerp((Vector2)this.transform.position, new Vector2(posX, posY), cameraSmoothSpeed * Time.deltaTime);

        this.transform.position = new Vector3(smoothPos.x, smoothPos.y, -10);
    }
}
