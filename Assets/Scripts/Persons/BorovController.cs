using System.Collections;
using Persons.Player;
using TMPro;
using UI;
using UnityEngine;

namespace Persons
{
    public class BorovController : MonoBehaviour
    {
        private bool _playerDetect;
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform groundPosition;
    
        [SerializeField] private GameObject dialogWindow;
        [SerializeField] private TextMeshProUGUI guiText;
        [SerializeField] private GameObject guiFace;
        [SerializeField] private GameObject borovFace;
    
        private Animator _animator;
        private static readonly int SawAPlayer = Animator.StringToHash("SawAPlayer");

        private readonly string[] _redDialog =
        {
            "Здравствуйте. Вызывали?",
            "Он самый.",
            "За какое такое старое?",
            "И откуда материал?",
            "Понимаю. Это я понимаю. Не понимаю только, какая же это сволочь на меня донесла…"
        };
        private readonly string[] _borovDialog =
        {
            "Рэдрик Шухарт?",
            "Опять за старое взялся?",
            "Сам знаешь, за какое. Опять на тебя материал пришёл.",
            "Это тебя не касается. Я тебя по старой дружбе предупреждаю: брось это дело, брось навсегда.", // #
            "Ведь во второй раз сцапают, шестью месяцами не отделаешься, из института вышибут навсегда, понимаешь?",
            "Свободен, Шухарт."
        };
    
        private void Awake() => _animator = GetComponent<Animator>();

        private void Update()
        {
            _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
                new Vector2(width, height), 0, whatIsPlayer);

            _animator.SetBool(SawAPlayer, _playerDetect);

            if (!_playerDetect) return;
            if (PlayerController.DialogWithBorov)
            {
                StartCoroutine(Says());
                PlayerController.DialogWithBorov = false;
            }
        }

        private IEnumerator Says()
        {
            if (PlayerController.DialogWithBorov)
            {
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _redDialog[0], guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[0], guiText,
                    guiFace, borovFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _redDialog[1], guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[1], guiText,
                    guiFace, borovFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _redDialog[2], guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[2], guiText,
                    guiFace, borovFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _redDialog[3], guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[3], guiText,
                    guiFace, borovFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[4], guiText,
                    guiFace, borovFace));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _redDialog[4], guiText));
                yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, _borovDialog[5], guiText,
                    guiFace, borovFace));
                PlayerController.PerspectiveExitFromBorovOffice = true;
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            var position = groundPosition.position;
            Gizmos.DrawWireCube(position, new Vector2(width, height));  
        }
    }
}
