using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private TopDownController3D _controller;
    public MMFeedbacks FinishMMFeedbacks;

    private void Awake()
    {
        FinishMMFeedbacks?.Initialization(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FinishMMFeedbacks.PlayFeedbacks(transform.position);
            other.gameObject.GetComponent<TopDownController3D>().enabled = false;
            GameObject.FindGameObjectWithTag("PoisonBar").GetComponent<PoisonBar>().enabled = false;
            GUIManager.Instance.SetDeathScreen(true);
            TopDownEngineEvent.Trigger(TopDownEngineEventTypes.PauseNoMenu, null);
        }
    }
}
