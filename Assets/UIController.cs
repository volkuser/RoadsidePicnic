using Newtonsoft.Json.Serialization;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject helpWindow;
    /*private Transform _player;
    
    private bool _helpWindowIsShowed = false;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void LateUpdate()
    {
        if (!_helpWindowIsShowed) return;
        var direction = helpWindow.transform.position;
        var position = _player.position;
        direction.x = position.x;
        direction.y = position.y;
        helpWindow.transform.position = direction;
    }*/
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            /*var direction = helpWindow.transform.position;
            var position = _player.position;
            direction.x = position.x;
            direction.y = position.y;
            helpWindow.transform.position = direction;*/
            
            //_helpWindowIsShowed = true;
            helpWindow.SetActive(true);
        }
        if (helpWindow.activeSelf && Input.GetKeyDown(KeyCode.N)) helpWindow.SetActive(false);
    }
}
