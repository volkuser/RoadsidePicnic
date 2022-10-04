using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpWindow;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) helpWindow.SetActive(true);
        if (helpWindow.activeSelf && Input.GetKeyDown(KeyCode.N)) helpWindow.SetActive(false);
    }
}
