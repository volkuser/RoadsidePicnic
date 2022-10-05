using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private void AfterDarkening(string sceneName)
    {
        SceneManager.LoadScene(PlayerController.GamePassed ? "End" : sceneName);
    }
}
