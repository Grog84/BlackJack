using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public LayerMask interactableMask;

    bool mouseBtnDown;

    Ray ray = new Ray();

    private void Start()
    {
        mouseBtnDown = false;
        ray.direction = Vector3.down;
    }

    private void Update()
    {
        if (InputManager.instance.pressed && !mouseBtnDown)
        {
            mouseBtnDown = true;
            ray.origin = InputManager.instance.GetPosition();
            if (Physics.Raycast(ray, float.PositiveInfinity, interactableMask))
            {

            }
        }
    }

}
