using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System.Collections;

public class MenuButton : MonoBehaviour, IPointerClickHandler
{
    public enum ButtonAction
    {
        Play,
        Quit
    }

    [Header("Button Settings")]
    [SerializeField] private ButtonAction buttonAction;
    [SerializeField] private string sceneName = "Game";

    [Header("Video Settings")]
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private VideoPlayer videoPlayer;

    private static bool isPlayingIntro = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonAction)
        {
            case ButtonAction.Play:

                if (!isPlayingIntro)
                {
                    StartCoroutine(PlayIntro());
                }

                break;

            case ButtonAction.Quit:

                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif

                break;
        }
    }

    private IEnumerator PlayIntro()
    {
        isPlayingIntro = true;

        if (videoPanel != null)
            videoPanel.SetActive(true);

        if (videoPlayer != null)
        {
            videoPlayer.Prepare();

            while (!videoPlayer.isPrepared)
                yield return null;

            videoPlayer.Play();

            while (videoPlayer.isPlaying)
                yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}