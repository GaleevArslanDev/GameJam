using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EscapeToMenu : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}