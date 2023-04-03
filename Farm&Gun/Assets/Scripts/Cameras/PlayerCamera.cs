using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;    
    [SerializeField]
    float xFromPlayer, yFromPlayer, zFromPlayer, cameraMovementSpeed, cameraPadding, maxDiversion;
    float minXDiversion, maxXDiversion, minZDiversion, maxZDiversion;
    Vector2 initialXZFromPlayer;
    [SerializeField]
    float cameraMinZoomout = 1.5f, cameraMaxZoomout = 3, zoomStep = 0.1f;
    [SerializeField]
    private float defaultZoom;
    private Camera thisCamera;


    private void Awake()
    {
        maxXDiversion = xFromPlayer + maxDiversion;
        minXDiversion = xFromPlayer - maxDiversion;
        maxZDiversion = zFromPlayer + maxDiversion;
        minZDiversion = zFromPlayer - maxDiversion;
        initialXZFromPlayer = new Vector2(xFromPlayer, zFromPlayer);

        thisCamera = this.GetComponent<Camera>();
        defaultZoom = thisCamera.orthographicSize;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xFromPlayer, player.transform.position.y + yFromPlayer, player.transform.position.z + zFromPlayer);
        ZoomInOut();
        float distance = cameraMovementSpeed * Time.deltaTime;
        Comeback(TryMove(distance), distance);

    }

    //camera shake on attack
    private void ZoomInOut()
    {
        if (Input.GetKey(KeyCode.Alpha2) && Camera.main.orthographicSize < cameraMaxZoomout)
        {
            Camera.main.orthographicSize += zoomStep;
            if (Camera.main.orthographicSize > cameraMaxZoomout) Camera.main.orthographicSize = cameraMaxZoomout;
            defaultZoom = thisCamera.orthographicSize;
        }
        else if (Input.GetKey(KeyCode.Alpha1) && Camera.main.orthographicSize > cameraMinZoomout)
        {
            Camera.main.orthographicSize -= zoomStep;
            if (Camera.main.orthographicSize < cameraMinZoomout) Camera.main.orthographicSize = cameraMinZoomout;
            defaultZoom = thisCamera.orthographicSize;
        }
    }

    //checks if the mouse is on a border and moves the camera appropriately
    private bool TryMove(float distance)
    {
        bool wasMoved = false;

        if (Input.mousePosition.x > (1 - cameraPadding) * Screen.width)
        {
            if (xFromPlayer < maxXDiversion) xFromPlayer += distance;
            if (zFromPlayer > minZDiversion) zFromPlayer -= distance;
            wasMoved = true;
        }
        else if (Input.mousePosition.x < cameraPadding * Screen.width)
        {
            if (xFromPlayer > minXDiversion) xFromPlayer -= distance;
            if (zFromPlayer < maxZDiversion) zFromPlayer += distance;
            wasMoved = true;
        }
        if (Input.mousePosition.y > (1 - cameraPadding) * Screen.height)
        {
            if (xFromPlayer < maxXDiversion) xFromPlayer += distance;
            if (zFromPlayer < maxZDiversion) zFromPlayer += distance;
            wasMoved = true;
        }
        else if (Input.mousePosition.y < cameraPadding * Screen.height)
        {
            if (xFromPlayer > minXDiversion) xFromPlayer -= distance;
            if (zFromPlayer > minZDiversion) zFromPlayer -= distance;
            wasMoved = true;
        }

        return wasMoved;
    }

    //camera comes back if mouse is in the middle of the screen
    private void Comeback(bool wasMoved, float distance)
    {
        if (!wasMoved)
        {
            if (xFromPlayer - initialXZFromPlayer.x > 0.1f) xFromPlayer -= distance;
            else if (xFromPlayer - initialXZFromPlayer.x < -0.1f) xFromPlayer += distance;
            if (zFromPlayer - initialXZFromPlayer.y > 0.1f) zFromPlayer -= distance;
            else if (zFromPlayer - initialXZFromPlayer.y < -0.1f) zFromPlayer += distance;
        }
    }

    //camera shake coroutine
    public IEnumerator ZoomCoroutine(float stepForward, float stepBackwards, float maxZoom)
    {
        while (thisCamera.orthographicSize > maxZoom)
        {
            thisCamera.orthographicSize -= stepForward * Time.deltaTime;
            yield return null;
        }
        while (thisCamera.orthographicSize < defaultZoom)
        {
            thisCamera.orthographicSize += stepBackwards * Time.deltaTime;
            yield return null;
        }
    }
}
