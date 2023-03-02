using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InterviewScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interviewerSpeech;
    private readonly string[] _interviewerQuotes =
    {
        "Что вы подумали, когда ваш родной город оказался объектом нашествия инопланетной сверхцивилизации?...",
        "Однако в конце концов вам пришлось поверить.",
        "И что же?",
        "По вашему мнению, это является самым важным открытием за все эти тринадцать лет?",
        "А что же?",
        "Простите?",
        "Это страшно интересно, доктор Пильман, но я имел в виду открытия технологического порядка. " +
        "Ведь целый ряд очень видных учёных полагает, " +
        "что находки в Зонах Посещения способны изменить весь ход нашей истории.",
        "А разве на эти чудеса посягает ещё кто-нибудь?",
        "Вы, вероятно, имеете в виду сталкеров?",
        "Так у нас в Хармонте называют отчаянных парней, " +
        "которые на свой страх и риск проникают в Зону и тащат оттуда все, что им удаётся найти. " +
        "Это настоящая новая профессия."
    };
    [SerializeField] private TextMeshProUGUI researcherSpeech;
    private readonly string[] _researcherQuotes =
    {
        "Честно говоря, сначала я подумал, что это шутка. Трудно было себе представить, " +
        "что в нашем старом маленьком Хармонте может случиться что-нибудь подобное.",
        "В конце концов да.",
        "Мне вдруг пришло в голову, что Хармонт и остальные пять Зон Посещения ложатся на очень гладкую кривую. " +
        "Так и был открыт радиант Пильмана.",
        "Нет",
        "Сам факт Посещения.",
        "Не так уж важно, кто были эти пришельцы. Неважно, откуда они прибыли, зачем прибыли, " +
        "почему так недолго пробыли и куда девались потом. Важно то, что теперь человечество твердо знает: " +
        "оно не одиноко во Вселенной.",
        "Н-ну, я не имею никакого отношения к изучению внеземных культур. В КОПРОПО я и мои коллеги следим, " +
        "чтобы инопланетными чудесами, добытыми в зонах, распоряжался только Международный институт.",
        "Да.",
        "Я не знаю, что это такое.",
        "Понимаю. Нет, это вне нашей компетенции…"
    };
        
    [SerializeField] private float delay = 0.01f;
        
    [SerializeField] private GameObject fullDarkening;
    private Animator _fullDarkeningAnimator;
        
    private static readonly int DarkenUp = Animator.StringToHash("DarkenUp");
    private void Awake() => _fullDarkeningAnimator = fullDarkening.GetComponent<Animator>();
    
    private void Start() => StartCoroutine(Process());

    private void LateUpdate()
    {
        if (!Input.GetKey(KeyCode.N)) return;
        _fullDarkeningAnimator.SetTrigger(DarkenUp);
        StopCoroutine(Process());
    }

    private IEnumerator QuestionWithAnswer(string question, string answer, 
        TMP_Text guiTextAnswer, TMP_Text guiTextQuestion)
    {
        foreach (var symbol in question)
        {
            guiTextQuestion.text += symbol;
            yield return new WaitForSeconds(delay);
        }
    
        foreach (var symbol in answer)
        {
            guiTextAnswer.text += symbol;
            yield return new WaitForSeconds(delay);
        }
    }
    
    private IEnumerator Process()
    {
        for (var i = 0; i < 10; i++)
        {
            interviewerSpeech.text = researcherSpeech.text = string.Empty;
            yield return StartCoroutine(QuestionWithAnswer(_interviewerQuotes[i], 
                _researcherQuotes[i], researcherSpeech, interviewerSpeech));
            yield return new WaitForSeconds(1f);
        }
        _fullDarkeningAnimator.SetTrigger(DarkenUp);
    }
}