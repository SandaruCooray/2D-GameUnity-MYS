using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuEvents : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;

    private void Start()
    {
        Time.timeScale = 1;
        mixer.GetFloat("volume",out value);
        volumeSlider.value = value;
    }
    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);
    }

    public void LoadLevel(int index)
    {
        Debug.Log("Load scene " + index);

        // Reset the checkpoint position to the start position of each level
        switch (index)
        {
            case 1:  // Level 1
                PlayerManager.lastCheckPointPos = new Vector2(-3, 0);  // Starting position for Level 1
                break;
            case 2:  // Level 2
                PlayerManager.lastCheckPointPos = new Vector2(-2, 0);  // Starting position for Level 2
                break;
            // Add cases for other levels as needed
            default:
                PlayerManager.lastCheckPointPos = new Vector2(0, 0);  // Default starting position
                break;
        }

        // Reset player's health
        HealthManager.health = 3;  // Assuming 3 is the full health value

        SceneManager.LoadScene(index);
    }
}
