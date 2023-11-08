using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootEvent;
    public static Action reloadEvent;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootEvent?.Invoke();
        if (Input.GetKeyDown(KeyCode.R))
            reloadEvent?.Invoke();
    }
}
