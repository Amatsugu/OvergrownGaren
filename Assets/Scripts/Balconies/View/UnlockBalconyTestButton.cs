using System.Linq;
using UnityEngine;

public class UnlockBalconyTestButton : MonoBehaviour
{

    public void OnClick()
    {
        var balconiesService = GameManager.BalconiesService;
        var allLockedBalconies = balconiesService.AllBalconies.Where(b => !b.IsUnlocked).ToArray();
        if (allLockedBalconies.Length == 0)
        {
            return;
        }
        
        var rIndex = Random.Range(0, allLockedBalconies.Length);
        var rLockedBalcony = allLockedBalconies[rIndex];
        balconiesService.UnlockBalcony(rLockedBalcony.Id);
    }
}
