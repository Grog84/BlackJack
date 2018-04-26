using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool pressed { get; private set; }
    Vector2 pressPosition;

    public static InputManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
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
        return Camera.main.ScreenToWorldPoint(pressPosition);
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
