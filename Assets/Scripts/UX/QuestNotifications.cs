using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestNotifications : MonoBehaviour
{
    public RectTransform _root;
    public TMP_Text _textQuestTitle;
    public Button _btn;
    public QuestTracker _questTracker;
    public ShopUI _shopUI;

    private Vector3 _hiddenPosition;
    private Vector3 _shownPosition;
    // private TweenerCore<Vector3, Vector3, VectorOptions> _tweener;
    private Sequence _sequence;
    
    private void Awake()
    {
        _root.gameObject.SetActive(false);
        _hiddenPosition = new Vector3(0, 100, 0);
        _shownPosition = new Vector3(0, 0, 0);
        _root.anchoredPosition = _hiddenPosition;
    }

    private void Start()
    {
        GameManager.Events.OnQuestBecomeReadyToComplete += OnQuestBecomeReadyToComplete;
        
        _btn.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveListener(OnClick);
    }

    private void OnQuestBecomeReadyToComplete(QuestDefination quest)
    {
        if (_root.gameObject.activeInHierarchy)
        {
            Hide(() =>
            {
                _textQuestTitle.text = quest.displayName;
                Show();
            });
        }
        else
        {
            _textQuestTitle.text = quest.displayName;
            Show();
        }
    }

    private void OnClick()
    {
        Hide();
        _questTracker.ShowQuests();
    }

    private void Show()
    {
        if (_shopUI.gameObject.activeInHierarchy)
        {
            _shopUI.Hide();
        }
        
        _sequence?.Kill();

        _root.gameObject.SetActive(true);
        _root.anchoredPosition = _hiddenPosition;

        _sequence = DOTween.Sequence();
        _sequence
            .Append(_root.DOAnchorPos(_shownPosition, 0.4f).SetEase(Ease.OutElastic))
            .AppendInterval(3f)
            .OnComplete(() =>
            {
                Hide();
            })
            .Play();
    }

    private void Hide(Action onComplete = null)
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();
        _sequence
            .Append(_root.DOAnchorPos(_hiddenPosition, 0.4f).SetEase(Ease.InElastic))
            .OnComplete(() =>
            {
                _root.gameObject.SetActive(false);
                _sequence = null;
                onComplete?.Invoke();
            })
            .Play();
    }
}
