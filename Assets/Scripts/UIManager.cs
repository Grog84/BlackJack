using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject[] labels;

    public void Init()
    {
        for (int i = 1; i < GameManager.instance.playersNumber + 1; i++)
        {
            labels[i].SetActive(true);
        }
    }

}
