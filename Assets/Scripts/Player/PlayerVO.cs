using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVO{

    private string _id;

    public string id
    {
        get { return _id; }
        set { _id = "Player " + value; }
    }

    public PlayerVO() { }
}
