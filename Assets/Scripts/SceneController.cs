using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


public class SceneController : MonoBehaviour
{
    private Controls _controls;

    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
    private void Awake()
    {
        _controls = new Controls();
        Debug.Log("SceneController set hi");
        _controls.Base.OpenScene1.performed += context => OpenMainScene();
        _controls.Base.OpenScene2.performed += context => OpenTopDownScene();
        _controls.Base.OpenScene3.performed += context => OpenIsoScene();

    }
    private void OpenMainScene()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("Loaded Main Scene - Front Scene");
            SceneManager.LoadScene(0);
        }
    }
    private void OpenTopDownScene()
    {
        if (SceneManager.GetActiveScene().name != "TopDownScene")
        {
            Debug.Log("Loaded Top-Down Scene");
            SceneManager.LoadSceneAsync(1);
        }
    }
    private void OpenIsoScene()
    {
        if (SceneManager.GetActiveScene().name != "IsoScene")
        {
            Debug.Log("Loaded Iso Scene");
            SceneManager.LoadSceneAsync(2);


        }
    }

}
