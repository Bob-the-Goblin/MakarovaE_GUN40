using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    Controls _controls;

    [SerializeField]
    private Image image;

    private void Awake()
    {
        _controls = new Controls();
        GameObject game = GameObject.Find("ImageToFull");
        image = game.GetComponent<Image>();
        
        _controls.Game.Restart.started += Restart_started;
        _controls.Game.Restart.performed += Restart_performed;
        _controls.Game.Restart.canceled += Restart_canceled;
    }

    private void OnEnable()
    {
        _controls.Game.Enable();
    }
    void Start()
    {
    }
    void Update()
    {
        
    }

    private void Restart_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        image.fillAmount = 0;
    }

    private void Restart_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(1);
    }

    private void Restart_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        image.fillAmount = 1;
    }

    private void OnDisable()
    {
        _controls.Game.Disable();
    }
}
