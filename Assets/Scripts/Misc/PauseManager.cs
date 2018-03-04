using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public bool isPaused = false;
    public GameObject pauseCanvas;
    public KeyCode pauseKey = KeyCode.Escape;
    public BlurOptimized blur;

    private Inventory inventory;

    private void Start()
    {
        isPaused = false;
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
            }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                CursorManager.ShowUnlockCursor();
                inventory.isShown = false;
                inventory.inventoryWindow.SetActive(false);
            }
            else if(isPaused == false)
            {
                CursorManager.HideLockCursor();
            }
        }
        if(isPaused == true)
        {
            Time.timeScale = 0.0f;
            pauseCanvas.SetActive(true);
            blur.enabled = true;

        }
        else if(isPaused == false)
        {
            Time.timeScale = 1.0f;
            pauseCanvas.SetActive(false);
            blur.enabled = false;
            
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
