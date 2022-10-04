using System.Collections;
using TMPro;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask whatIsPlayer;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;
    
    [SerializeField] private GameObject window;
    
    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) window.SetActive(false);
        if (!PlayerController.PerspectiveWindow) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        window.SetActive(true);

        StartCoroutine(Thinks());
        PlayerController.PerspectiveWindow = false;
    }

    private IEnumerator Thinks()
    {
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, "*С Зоной ведь так: " +
            "с хабаром вернулся - чудо, живой - удача, патрульная пуля мимо - везенье…", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow,
            "…а все остальное - судьба… *", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow,
            "*Чего это Кирилл так пристально смотрит?*", guiText));
        PlayerController.DialogWithKiril8 = true;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
