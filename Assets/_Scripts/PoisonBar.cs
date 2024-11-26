using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class PoisonBar : MonoBehaviour
{
    public MMProgressBar progressBar;

    public float maxTime = 300f;
    public float elapsedTime = 0f;
    private Health healthComponent;
    private bool deathTriggered = false;

    private void Start()
    {
        healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        if (healthComponent == null)
        {
            healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        elapsedTime += Time.deltaTime;

        // Clamp the elapsed time to the maximum time
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, maxTime);

        // Calculate the progress as a percentage
        float progress = elapsedTime / maxTime;

        // Update the progress bar's value
        progressBar.SetBar01(progress);

        if (progress >= 1f && !deathTriggered)
        {
            deathTriggered = true;
            healthComponent.Kill();
        }
    }

    public void AddSeconds(float amountOfSecondsToAdd)
    {
        elapsedTime -= amountOfSecondsToAdd;
        if (elapsedTime < 0f)
        {
            elapsedTime = 0f;
        }
    }
}
