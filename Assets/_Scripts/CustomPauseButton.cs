using MoreMountains.TopDownEngine;
using System.Collections;
using UnityEngine;

public class CustomPauseButton : PauseButton
{
    public GameObject Intro;

    private void Start()
    {
        TopDownEngineEvent.Trigger(TopDownEngineEventTypes.PauseNoMenu, null);
    }

    protected override IEnumerator PauseButtonCo()
    {
        yield return null;
        // we trigger a Pause event for the GameManager and other classes that could be listening to it too
        TopDownEngineEvent.Trigger(TopDownEngineEventTypes.UnPause, null);
        Intro.SetActive(false);
    }
}
