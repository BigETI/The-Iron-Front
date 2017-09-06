using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Click away controller
public class ClickAwayControllerScript : MonoBehaviour
{
    // Scene name
    [SerializeField]
    private string sceneName;

    // Update
    private void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(sceneName);
    }
}
