using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
