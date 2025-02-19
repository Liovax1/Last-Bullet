using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Remplace par le nom de ta scène de jeu
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit(); // Quitte le jeu (ne fonctionne pas dans l'éditeur Unity)
    }
}
