/*
 * The input manager is in charge of projecting the movement of the player mouse
 * on the plane where the cards are actually moving
 * 
 * */


using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool pressed { get; private set; }
    Vector2 pressPosition;

    public LayerMask mask;
    Ray ray;
    RaycastHit hit;

    public static InputManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public bool IsPressed()
    {
        return pressed;
    }

    public Vector3 GetPosition()
    {
        ray = Camera.main.ScreenPointToRay(pressPosition);

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, mask))
        {
            return hit.point;
        }
        else
        {
            Debug.LogError("Raycast Failed!");
            return Vector3.zero;
        }


        //return Camera.main.ScreenToWorldPoint(pressPosition);
    }

    public Vector2 GetPositionVec2()
    {
        ray = Camera.main.ScreenPointToRay(pressPosition);

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, mask))
        {
            return new Vector2(hit.point.x, hit.point.z);
        }
        else
        {
            Debug.LogError("Raycast Failed!");
            return Vector2.zero;
        }


        //return Camera.main.ScreenToWorldPoint(pressPosition);
    }

    public Vector3 GetPressPosition()
    {
        return pressPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;
        }

        if (pressed)
        {
            pressPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }

    }

}
