using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject Menu;


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        Menu.SetActive(false);
    }


    public void ReturnToMainMenu()
    {
        settingsMenu.SetActive(false);
        Menu.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}

