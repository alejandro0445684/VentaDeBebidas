using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager
{
    public void CargarEscena(string MainMenu)
    {
        SceneManager.LoadScene (MainMenu) ;
    }
}
