using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class BorovOfficeDoorScript : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask whatIsPlayer;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (PlayerController.PerspectiveExitFromBorovOffice)
        {
            StartCoroutine(Says());
            PlayerController.PerspectiveExitFromBorovOffice = false;
        }
    }

    private IEnumerator Says()
    {
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "*Откуда же донос идет? В институте про меня никто ничего не знает и знать не может.", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "А если бумаги из полиции, то что они там могут знать, кроме моих старых дел?", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "Последний раз ночью я в Зону ходил три месяца назад, хабар почти весь уже сбыл и деньги потратил.", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "С поличным не поймали, а теперь меня не возьмешь, я скользкий.", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "...", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "Так , стоп. Получается, в Зону то мне нельзя идти. " +
            "Никакой сталкер не подойдет к Зоне, когда за ним следят.", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "Мне надо в самый темный угол залезть. Какая, мол, Зона? " +
            "Что вы, понимаешь, привязались к честному лаборанту?", guiText));
        yield return StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "Только как бы теперь это все поделикатнее сообщить Кириллу?..*", guiText));
        PlayerController.DialogWithKiril7 = true;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
