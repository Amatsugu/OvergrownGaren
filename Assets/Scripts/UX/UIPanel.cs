using TMPro;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIPanel : UIHover
{
	public RectTransform PanelBase { get; private set; }
	public Button closeButton;
	public TMP_Text titleText;
	public bool hideOnStart = true;
	public bool hideOnBlur = true;
	public bool hideInEditor = false;
	public AudioClip openSound;
	public AudioClip closeSound;

	public event System.Action OnShow;

	public event System.Action OnHide;


	protected AudioSource audioSource;


	private bool _showThisFrame;

	public bool IsOpen
	{
		get
		{
			return gameObject?.activeInHierarchy ?? false;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
			audioSource = gameObject.AddComponent<AudioSource>();
	}

	protected override void Start()
	{
		PanelBase = GetComponent<RectTransform>();
		if (!_showThisFrame)
		{
			if (hideOnStart || (hideInEditor && Application.isEditor))
				Hide();
			else
				OnShow?.Invoke();
		}
		if (closeButton != null)
		{
			var click = new Button.ButtonClickedEvent();
			click.AddListener(Hide);
			closeButton.onClick = click;
		}
	}

	protected override void Update()
	{
		base.Update();
		_showThisFrame = false;
	}

	protected override void LateUpdate()
	{
		if (!hideOnBlur)
			return;
		if (_showThisFrame)
			return;
		if (Input.GetKeyUp(KeyCode.Mouse0) && !isHovered)
		{
			Hide();
		}
	}

	public virtual void Show()
	{
		_showThisFrame = true;
		SetActive(true);
		OnShow?.Invoke();
		if (openSound != null)
			audioSource.PlayOneShot(openSound);
	}

	public virtual void Hide()
	{
		if (closeSound != null)
			audioSource.PlayOneShot(closeSound);
		SetActive(false);
		OnHide?.Invoke();
	}

	public static void DestroyChildren(Transform transform)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	public static Vector3 ContrainToScreen(Vector3 pos, Rect rect)
	{
		if (pos.x < 0)
			pos.x = 0;
		if (pos.x + rect.width > Screen.width)
			pos.x = Screen.width - rect.width;
		if (pos.y - rect.height < 0)
			pos.y = rect.height;
		if (pos.y > Screen.height)
			pos.y = Screen.height;
		return pos;
	}
}