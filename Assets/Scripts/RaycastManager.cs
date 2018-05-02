/*
 * The raycast manager is performing the evalutaion of the raycasts
 * connected to the mouse clicking and release
 * 
 * */

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
    }

    private void Update()
    {
        if (InputManager.instance.pressed && !mouseBtnDown)
        {
            mouseBtnDown = true;
            ray = Camera.main.ScreenPointToRay(InputManager.instance.GetPressPosition());

            if (Physics.Raycast(ray, out rayHit, float.PositiveInfinity, interactableMask))
            {
                if (rayHit.collider.tag == "Card")
                {
                    if (!rayHit.collider.GetComponent<Card>().locked)
                    {
                        rayHit.collider.GetComponent<Card>().grabbed = true;
                    }
                }
                else if (rayHit.collider.tag == "Deck")
                {
                    rayHit.collider.GetComponent<Deck>().clicked = true;
                }
            }
        }
        else if (mouseBtnDown && !InputManager.instance.pressed)
        {
            mouseBtnDown = false;
            ray = Camera.main.ScreenPointToRay(InputManager.instance.GetPressPosition());

            if (Physics.Raycast(ray, out rayHit, float.PositiveInfinity, interactableMask))
            {
                if (rayHit.collider.tag == "Card")
                {
                    rayHit.collider.GetComponent<Card>().grabbed = false;
                }
            }
        }
    }

}
