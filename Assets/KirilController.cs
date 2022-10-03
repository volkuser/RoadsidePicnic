using System.Collections;
using TMPro;
using UnityEngine;

public class KirilController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundPosition;

    private Animator _animator;
    private static readonly int Listen = Animator.StringToHash("Listen");
    private static readonly int Write = Animator.StringToHash("Write");

    private bool _playerDetectInDialogZone;
    [SerializeField] private float dialogWidth;
    [SerializeField] private float dialogHeight;
    [SerializeField] private Transform dialogCenter;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;
    [SerializeField] private GameObject guiFace;
    [SerializeField] private GameObject kirilFace;

    private readonly string[] _firstDialogKiril =
    {
        "О! Ты как раз вовремя.",
        "Да. Можешь перенести еще две сюда? Мне нужно досчитать тут кое-что, пока не забыл.",
        "Они лежат в шкафу в моем новом кабинете. Это соседняя комната.", // #
        "Там все еще много неразобранных коробок после ремонта, так что осторожнее."
    };
    
    private readonly string[] _firstDialogRed =
    {
        "Снова бьешься с этими «пустышками»?",
        "*«Пустышка» - действительно штука загадочная. Каждый раз как увижу – не могу, поражаюсь.", // #
        "Всего-то в ней два медных диска с чайное блюдце. А между ними, кроме расстояния в 40 мм ничего нет.", // #
        "Пустота и пустота, один воздух.*",
        "Н-ну… Хорошо."
    };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position,
            new Vector2(width, height), 0, whatIsPlayer);

        if (_playerDetect)
        {
            _animator.ResetTrigger(Write);
            _animator.SetTrigger(Listen);
        }
        else
        {
            _animator.ResetTrigger(Listen);
            _animator.SetTrigger(Write);
        }
        
        _playerDetectInDialogZone = Physics2D.OverlapBox(dialogCenter.position,
            new Vector2(dialogWidth, dialogHeight), 0, whatIsPlayer);
        if (!_playerDetectInDialogZone) return;
        if (PlayerController.dialogWithKiril1)
        {
            StartCoroutine(Says());
            PlayerController.dialogWithKiril1 = false;
        }
    }

    private IEnumerator Says()
    {
        if (PlayerController.dialogWithKiril1)
        {
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogKiril[0],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogRed[0],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogKiril[1],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogRed[1],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogRed[2],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogRed[3],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogKiril[2],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogKiril[3],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _firstDialogRed[4],
                guiText));
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));
        
        Gizmos.color = Color.green;
        position = dialogCenter.position;
        Gizmos.DrawWireCube(position, new Vector2(dialogWidth, dialogHeight));
    }
}
