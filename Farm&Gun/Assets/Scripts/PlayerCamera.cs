using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    AnimatorSupport animatorSupport;
    [SerializeField]
    float xFromPlayer, yFromPlayer, zFromPlayer, cameraMovementSpeed, cameraPadding, maxDiversion;
    float minXDiversion, maxXDiversion, minZDiversion, maxZDiversion;
    Vector2 initialXZFromPlayer;
    [SerializeField]
    float cameraMinZoomout = 1.5f, cameraMaxZoomout = 3, zoomStep = 0.1f;

    private void Awake()
    {
        maxXDiversion = xFromPlayer + maxDiversion;
        minXDiversion = xFromPlayer - maxDiversion;
        maxZDiversion = zFromPlayer + maxDiversion;
        minZDiversion = zFromPlayer - maxDiversion;
        initialXZFromPlayer = new Vector2(xFromPlayer, zFromPlayer);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xFromPlayer, player.transform.position.y + yFromPlayer, player.transform.position.z + zFromPlayer);

        if (Input.GetKey(KeyCode.Alpha2) && Camera.main.orthographicSize < cameraMaxZoomout)
        {
            Camera.main.orthographicSize += zoomStep;
            if (Camera.main.orthographicSize > cameraMaxZoomout) Camera.main.orthographicSize = cameraMaxZoomout;
            animatorSupport.SetDefaultZoom(Camera.main.orthographicSize);
        }
        else if (Input.GetKey(KeyCode.Alpha1) && Camera.main.orthographicSize > cameraMinZoomout)
        {
            Camera.main.orthographicSize -= zoomStep;
            if (Camera.main.orthographicSize < cameraMinZoomout) Camera.main.orthographicSize = cameraMinZoomout;
            animatorSupport.SetDefaultZoom(Camera.main.orthographicSize);
        }

        float distance = cameraMovementSpeed * Time.deltaTime;
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

        if(!wasMoved)
        {
            if (xFromPlayer - initialXZFromPlayer.x > 0.1f) xFromPlayer -= distance;
            else if (xFromPlayer - initialXZFromPlayer.x < -0.1f) xFromPlayer += distance;
            if (zFromPlayer - initialXZFromPlayer.y > 0.1f) zFromPlayer -= distance;
            else if (zFromPlayer - initialXZFromPlayer.y < -0.1f) zFromPlayer += distance;
        }
    }
}
