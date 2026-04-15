using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    public GameObject map;
    private bool isMapOpened = false;
    void Update()
    {
        if (Time.timeScale > 0f && Keyboard.current.mKey.wasPressedThisFrame)
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        isMapOpened = !isMapOpened;
        map.SetActive(isMapOpened);
    }
}
