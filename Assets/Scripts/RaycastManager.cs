using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cards;

public class RaycastManager : MonoBehaviour
{
    public LayerMask interactableMask;

    bool mouseBtnDown;

    Ray ray = new Ray();
    RaycastHit rayHit = new RaycastHit();

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
            if (Physics.Raycast(ray, out rayHit, float.PositiveInfinity, interactableMask))
            {
                if (rayHit.collider.tag == "Card")
                {
                    rayHit.collider.GetComponent<Card>().grabbed = true;
                }
                else if (rayHit.collider.tag == "Deck")
                {
                    rayHit.collider.GetComponent<Deck>().clicked = true;
                }
            }
        }
    }

}
