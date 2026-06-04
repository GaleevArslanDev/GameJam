using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Тип кнопки")]
    [SerializeField] private ButtonType buttonType;

    [Header("Настройки для кнопки Play")]
    [Tooltip("Перетащите сцену из Project Window (вкладка Assets)")]
    [SerializeField] private SceneAsset sceneToLoadAsset; // видно только в редакторе

    // Храним имя сцены для рантайма
    private string sceneName;

    private enum ButtonType { Play, Quit }

    private void OnValidate()
    {
        // Когда в инспекторе меняют SceneAsset, автоматически обновляем имя сцены
        if (sceneToLoadAsset != null)
        {
#if UNITY_EDITOR
            sceneName = sceneToLoadAsset.name;
#endif
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Play:
                LoadGameScene();
                break;
            case ButtonType.Quit:
                QuitGame();
                break;
        }
    }

    private void LoadGameScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Сцена не назначена! Перетащите сцену в поле sceneToLoadAsset.");
            return;
        }

        Debug.Log($"Загрузка сцены: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    private void QuitGame()
    {
        Debug.Log("Выход из игры");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}