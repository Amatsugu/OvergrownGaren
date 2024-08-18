using Resources;
using TMPro;
using UnityEngine;

public class AddCoinsTestButton : MonoBehaviour
{
    public TMP_Text _textTitle;
    public int _amountToAdd = 100;

    private void Start()
    {
        _textTitle.text = "Add Coins (+ " + _amountToAdd + ")";
    }

    public void OnClick()
    {
        GameManager.ResourcesService.AddResource((ResourceType.Coins, _amountToAdd));
    }
}
