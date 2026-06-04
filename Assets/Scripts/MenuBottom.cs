using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler
{
    public enum ButtonAction
    {
        Play,
        Quit
    }

    [SerializeField] private ButtonAction buttonAction;
    [SerializeField] private string sceneName = "Game";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonAction == ButtonAction.Play)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}