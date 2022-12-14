using Persons.Player;
using TMPro;
using UI;
using UnityEngine;

namespace Staff
{
    public class CupboardWithDummyController : MonoBehaviour
    {
        // actions
        private bool _playerDetect;
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private LayerMask whatIsPlayer;
        private bool _isOpened;
        [SerializeField] private Transform groundPosition;
        private bool _firstOpen = true;
    
        private Animator _animator;
        private static readonly int OpenWithDummy = Animator.StringToHash("OpenWithDummy");
        private static readonly int Opened = Animator.StringToHash("Opened");
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");
    
        [SerializeField] private GameObject dialogWindow;
        [SerializeField] private TextMeshProUGUI guiText;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
                new Vector2(width, height), 0, whatIsPlayer);

            if (!_playerDetect) return;
            if (!Input.GetKeyDown(KeyCode.E)) return;
            if(!_isOpened)
            {
                if (_firstOpen)
                {
                    if (!PlayerInventory.HasFirstDummy)
                    {
                        _animator.SetTrigger(OpenWithDummy);
                        PlayerInventory.HasFirstDummy = true;
                        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
                            "*Нашлась. Пора отдать ее Кириллу и с моим рабством покончено.*", guiText));
                        PlayerController.DialogWithKiril5 = true;
                    }
                    else
                    {
                        _animator.ResetTrigger(Open);
                        _animator.SetTrigger(Opened);
                        _isOpened = true;
                    }
                } 
                else
                {
                    _animator.ResetTrigger(Close);
                    _animator.SetTrigger(Open);
                    _isOpened = true;
                }
            }
            else
            {
                if (_firstOpen)
                {
                    _firstOpen = false;
                    _animator.ResetTrigger(Opened);
                }
                else _animator.ResetTrigger(Open);
                _animator.SetTrigger(Close);
                _isOpened = false;
            }
        }
    
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.magenta;
            var position = groundPosition.position;
            Gizmos.DrawWireCube(position, new Vector2(width, height));  
        }
    }
}
