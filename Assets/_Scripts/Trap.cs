using MoreMountains.Feedbacks;
using System.Threading.Tasks;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public MMFeedbacks PickedMMFeedbacks;
    public Transform TrapSpikes;

    private void Start()
    {
        PickedMMFeedbacks?.Initialization(gameObject);
    }
    private async void OnTriggerEnter(Collider other)
    {
        if (enabled && other.gameObject.CompareTag("Player"))
        {
            await MoveSpikes();
            enabled = false;
        }
    }

    private async Task MoveSpikes()
    {
        await Task.Delay(500);
        PickedMMFeedbacks?.PlayFeedbacks();
        TrapSpikes.localPosition = new Vector3(0, 0, 0);
    }
}
