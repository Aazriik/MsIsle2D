using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HUD;


    // Play Button
    public void Play()
    {
        LevelManager.Instance.LoadScene("lvl_1", "CircleWipe");
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
