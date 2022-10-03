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
        "Всего-то в ней два медных диска с чайное блюдце. А между ними, кроме расстояния в 40 см ничего нет.", // #
        "Пустота и пустота, один воздух.*",
        "Н-ну… Хорошо."
    };
    
    private readonly string[] _secondDialogRed =
    {
        "А дверь я выломать должен что ли?",
        "Ц…"
    };
    
    private readonly string[] _secondDialogKiril =
    {
        "Ой.. Карта была где-то тут, посмотри на столах."
    };
    
    private readonly string[] _thirdDialogRed =
    {
        "Нет ее здесь нигде.",
    };
    
    private readonly string[] _thirdDialogKiril =
    {
        "Попробуй поискать в соседней лаборатории. "
    };
    
    private readonly string[] _fourthDialogRed =
    {
        "И там ее тоже нет.",
        "Об стенку только не бейся. Найду твою карту. Пройдусь по этажу, может оставил где."
    };
    
    private readonly string[] _fourthDialogKiril =
    {
        "Как же так.. Неужели я потерял ее?.. Ее обязательно нужно найти."
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
        if (PlayerController.DialogWithKiril1)
        {
            StartCoroutine(Says());
            PlayerController.DialogWithKiril1 = false;
        } 
        
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (PlayerController.DialogWithKiril2)
        {
            StartCoroutine(Says());
            PlayerController.DialogWithKiril2 = false;
            PlayerController.PerspectiveOnDialogWithKiril2 = false;
        } else if (PlayerController.DialogWithKiril3)
        {
            StartCoroutine(Says());
            PlayerController.DialogWithKiril3 = false;
        } else if (PlayerController.DialogWithKiril4)
        {
            StartCoroutine(Says());
            PlayerController.DialogWithKiril4 = false;
            PlayerController.PerspectiveOnDialogWithKiril4 = false;
        }
    }

    private IEnumerator Says()
    {
        if (PlayerController.DialogWithKiril1)
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
            PlayerController.PerspectiveOnDialogWithKiril2 = true;
        } else if (PlayerController.DialogWithKiril2)
        {
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _secondDialogRed[0],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _secondDialogKiril[0],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _secondDialogRed[1],
                guiText));
            PlayerController.DialogWithKiril3 = true;
        } else if (PlayerController.DialogWithKiril3)
        {
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _thirdDialogRed[0],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _thirdDialogKiril[0],
                guiText, guiFace, kirilFace));
            PlayerController.PerspectiveOnDialogWithKiril4 = true;
        } else if (PlayerController.DialogWithKiril4)
        {
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fourthDialogRed[0],
                guiText));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fourthDialogKiril[0],
                guiText, guiFace, kirilFace));
            yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fourthDialogRed[1],
                guiText));
            PlayerController.PerspectiveOnTakingCard = true;
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
