using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HUD;

    private void Start()
    {
        // Play Main Menu Music
        AudioManager.Instance.PlayMusic("Main Menu");
    }

    // Play Button
    public void Play()
    {
        LevelManager.Instance.LoadScene("lvl_1", "CircleWipe");
        AudioManager.Instance.PlayMusic("Game Lvl 1");
        //LevelManager.Instance.GetComponent<Canvas>().sortingOrder = 100;
    }

    // Quit Button
    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        HUD.SetActive(true);
    }
}
