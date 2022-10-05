using System.Collections;
using Persons.Player;
using TMPro;
using UI;
using UnityEngine;

namespace Persons
{
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
    
        private readonly string[] _fifthDialogRed =
        {
            "Вторая? В шкафу была лишь одна «пустышка»."
        };
        private readonly string[] _fifthDialogKiril =
        {
            "Отлично. Поставь ее на стол напротив раковины и иди за второй.",
            "Хм, странно… Я точно помню, что их было две. Недавно там была уборщица.", // #
            "Попробуй найти ее и расспросить."
        };
    
        private readonly string[] _sixthDialogRed =
        {
            "Слушай, Кирилл… А если бы у тебя была полная «пустышка», а?",
            "Ну да. С ерундой какой-то внутри, с синенькой.",
            "Знаю я как до одного гаража добраться. В нем и валяется твое сокровище.", // #
            "За пустую «пустышку» Эрнест дает четыреста монет наличными,", // #
            "а за полную я бы из него всю его поганую кровь выпил.",
            "Давай. А кто третий?",
            "Э, нет. Это тебе не пикник с девочками. А если что-нибудь с тобой случится? Зона - порядок должен быть.",
            "Староват. И дети у него… Но ладно, пусть будет Тендер.", // #
            "Ладно, Кирилл, я поскакал в «Боржч», а то жрать хочется и в глотке пересохло.",
            "Что?",
            "Ты бы еще ночью об этом сообщил.",
            "*Это что за новости? Чего это ради понадобился я капиту Херцогу в служебное время? Ладно, иду являться*"
        };
        private readonly string[] _sixthDialogKiril =
        {
            "Спасибо.",
            "Полная «пустышка»? Вот такая же штука, только полная?",
            "Где?",
            "Ай да ты! Ну что же, надо идти. Давай прямо завтра утром.", // #
            "В девять я закажу пропуска и «галошу», а в десять, благословясь, выйдем. Давай?",
            "А зачем нам третий?",
            "Как хочешь! Тебе виднее.", // #
            "Может быть Тендер? Он в зоне уже бывал.",
            "Постой. Я совсем забыл кое-что. Тебе просили кое что передать.",
            "Тебе сказано явиться к Херцогу."
        };
    
        private readonly string[] _seventhDialogRed =
        {
            "В Зону не иду. Какие будут распоряжения?", 
            "Нельзя мне. Нельзя, понимаешь? Херцог предупредил.",
            "Да ничего особенного, донес кто-то на меня, вот и все. Ну чего ты поник то сразу?", // #
            "*Опять он в апатию впадет. А кто виноват? Сам я и виноват.", // #
            "Поманил дитятю пряником, а пряник-то в заначке, а заначку сердитые дяди стерегут.*", // #
            "Думаешь, что я цену набиваю? После нашего возвращения все поймут, кто дал наводку на тот  гараж.", // #
            "Понимаешь, чем это для меня пахнет?",
            "*Кирилл явно о чем-то задумался.", // #
            "А Зона-матушка ведь совсем рядом, рукой подать. Вся как на ладони с окна тринадцатого этажа.*"
        };
        private readonly string[] _seventhDialogKiril =
        {
            "Что-нибудь случилось, Рэд? Ты что, раздумал?",
            "А что он тебе сказал?",
            "Слушай, Рэд, а сколько она может стоить, - полная «пустышка»?"
        };
    
        private readonly string[] _eightDialogRed =
        {
            "*Улыбаешься, Кирилл, как майская роза. Да и я не лучше.*",
            "Из каких же это официальных источников?",
            "А, от доктора Дугласа.. От какого же это Дугласа?",
            "*Ай да Кирилл.. Кто же перед выходом говорит о таких вещах. Ничего эти ученые не соображают.*", // #
            "Ладно. Где твой Тендер? Долго мы его еще ждать будем?",
            "Кирилл, начинай молиться.",
            "Молись! Дальше в Зону - ближе к небу.", // #
            "Молись. Сталкеров в рай без очереди пропускают."
        };
        private readonly string[] _eightDialogKiril =
        {
            "Лаборант Шухарт, из официальных, подчеркиваю: из официальных источников я получил важные сведения.", // #
            "Осмотр гаража может принести большую пользу науке. Есть предложение осмотреть гараж. Премиальные гарантирую.",
            "Это конфиденциальные источники. Но вам я могу сказать… Скажем, от доктора Дугласа.",
            "От Сэма Дугласа. Он погиб в прошлом году.",
            "Я позвоню в ППС, закажу «летучую галошу».",
            "Что?"
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
            } else if (PlayerController.DialogWithKiril8)
            {
                StartCoroutine(Says());
                PlayerController.DialogWithKiril8 = false;
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
            } else if (PlayerController.DialogWithKiril5)
            {
                StartCoroutine(Says());
                PlayerController.DialogWithKiril5 = false;
                PlayerController.PerspectiveLetFirstDummy = true;
            } else if (PlayerController.DialogWithKiril6)
            {
                StartCoroutine(Says());
                PlayerController.DialogWithKiril6 = false;
            } else if (PlayerController.DialogWithKiril7)
            {
                StartCoroutine(Says());
                PlayerController.DialogWithKiril7 = false;
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
            } else if (PlayerController.DialogWithKiril5)
            {
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fifthDialogKiril[0],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fifthDialogRed[0],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _fifthDialogKiril[1],
                    guiText, guiFace, kirilFace));
            } else if (PlayerController.DialogWithKiril6)
            {
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[0],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[0],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[1],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[1],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[2],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[2],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[3],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[4],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[3],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[4],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[5],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[5],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[6],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[6],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[7],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[7],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[8],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[8],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[9],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogKiril[9],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[10],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _sixthDialogRed[11],
                    guiText));
                PlayerController.DialogWithBorov = true;
            } else if (PlayerController.DialogWithKiril7)
            {
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[0],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogKiril[0],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[1],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogKiril[1],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[2],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[3],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[4],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogKiril[2],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[5],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[6],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[7],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _seventhDialogRed[8],
                    guiText));
                PlayerController.PerspectiveWindow = true;
            } else if (PlayerController.DialogWithKiril8)
            {
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[0],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[1],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[0],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[1],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[2],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[2],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[3],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[3],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[4],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[4],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[5],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogKiril[5],
                    guiText, guiFace, kirilFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[6],
                    guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _eightDialogRed[7],
                    guiText));
                PlayerController.GamePassed = true;
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
}
