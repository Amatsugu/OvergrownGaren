using System;
using System.Collections.Generic;

using DG.Tweening;

using Resources;

using TMPro;
using TMPro.EditorUtilities;

using UnityEngine;
using UnityEngine.UI;

public class QuestNotifications : MonoBehaviour
{
	public RectTransform _root;
	public TMP_Text _textQuestTitle;
	public TMP_Text _textNotifTitle;
	public Button _btn;

	[HideInInspector]
	public QuestTracker _questTracker;

	[HideInInspector]
	public ShopUI _shopUI;

	private Vector3 _hiddenPosition;
	private Vector3 _shownPosition;

	private NotifType _notifType;

	private List<Notif> _notifQueue = new();

	private class Notif
	{
		public NotifType type;
		public string title;
		public string subtitle;

		public Notif()
		{
			type = NotifType.NewQuests;
			title = "New Quests Availble";
			subtitle = "Click here to choose";
		}

		public Notif(QuestDefination quest)
		{
			type = NotifType.Quest;
			title = "Quest Complete";
			subtitle = quest.displayName;
		}

		public Notif(ResourceType resource)
		{
			type = NotifType.Shop;
			title = "New Item in Shop";
			subtitle = $"{resource.GetSprite()} {resource.ToDisplayString()}";
		}

		public Notif(string title, string subtitle)
		{
			type = NotifType.Info;
			this.title = title;
			this.subtitle = subtitle;
		}
	}

	private enum NotifType
	{
		None,
		Quest,
		Shop,
		NewQuests,
		Info
	}

	// private TweenerCore<Vector3, Vector3, VectorOptions> _tweener;
	private Sequence _sequence;

	private void Awake()
	{
		_root.gameObject.SetActive(false);
		_hiddenPosition = new Vector3(0, 100, 0);
		_shownPosition = new Vector3(0, 0, 0);
		_root.anchoredPosition = _hiddenPosition;
		_questTracker = FindAnyObjectByType<QuestTracker>();
		_shopUI = FindAnyObjectByType<ShopUI>(FindObjectsInactive.Include);
	}

	private void Start()
	{
		if (_shopUI == null)
			Debug.LogError("Shop UI is null");
		if (_questTracker == null)
			Debug.LogError("Quest Tracker is null");

		GameManager.Events.OnQuestBecomeReadyToComplete += OnQuestBecomeReadyToComplete;
		GameManager.Events.OnDayStart += OnDayStart;
		GameManager.Events.OnResourceTypeUnlocked += OnItemUnlocked;
		_btn.onClick.AddListener(OnClick);
	}

	private void OnDestroy()
	{
		_btn.onClick.RemoveListener(OnClick);
	}

	private void OnQuestBecomeReadyToComplete(QuestDefination quest)
	{
		_notifQueue.Add(new Notif(quest));
	}

	private void OnDayStart(int _)
	{
		if(_questTracker.HasQuests())
			_notifQueue.Add(new Notif());
	}

	private void OnItemUnlocked(ResourceType resource)
	{
		_notifQueue.Add(new(resource));
	}

	private void OnClick()
	{
		Hide();
		switch(_notifType){
			case NotifType.Quest:
				_questTracker.ShowQuests();
				_notifQueue.RemoveAll(n => n.type == NotifType.Quest);
				break;

			case NotifType.NewQuests:
				_questTracker.ShowNewQuests();
				_notifQueue.RemoveAll(n => n.type == NotifType.NewQuests);
				break;

			case NotifType.Shop:
				_shopUI.Show();
				_notifQueue.RemoveAll(n => n.type == NotifType.Shop);
				break;

		}
	}

	private void Update()
	{
		if (_notifType != NotifType.None)
			return;
		if (_notifQueue.Count == 0)
			return;
		var notif = _notifQueue[0];
		_notifQueue.RemoveAt(0);

		ShowNotif(notif);

	}

	private void ShowNotif(Notif notif)
	{
		_textNotifTitle.text = notif.title;
		_textQuestTitle.text = notif.subtitle;
		_notifType = notif.type;
		Show();
	}

	private void Show()
	{
		_sequence?.Kill();

		_root.gameObject.SetActive(true);
		_root.anchoredPosition = _hiddenPosition;

		_sequence = DOTween.Sequence();
		_sequence
			.Append(_root.DOAnchorPos(_shownPosition, 0.4f).SetEase(Ease.OutElastic))
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
				_notifType = NotifType.None;
			})
			.Play();
	}

	public void ShowNotif(string title, string subtitle)
	{
		_notifQueue.Add(new Notif(title, subtitle));
	}
}