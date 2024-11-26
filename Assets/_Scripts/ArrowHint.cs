using DunGen;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuntimeDungeon))]
public class ArrowHint : MonoBehaviour
{
    private RuntimeDungeon runtimeDungeon;
    private List<GameObject> arrows = new List<GameObject>();
    public MMFeedbacks ArrowHintMMFeedbacks;

    private void Awake()
    {
        runtimeDungeon = GetComponent<RuntimeDungeon>();
        runtimeDungeon.Generator.OnGenerationStatusChanged += OnDungeonGenerationStatusChanged;
        ArrowHintMMFeedbacks?.Initialization(gameObject);
    }

    private void OnDestroy()
    {
        runtimeDungeon.Generator.OnGenerationStatusChanged -= OnDungeonGenerationStatusChanged;
    }

    private void OnDungeonGenerationStatusChanged(DungeonGenerator generator, GenerationStatus status)
    {
        if (status == GenerationStatus.Complete)
        {
            for (int i = 2; i < generator.CurrentDungeon.MainPathTiles.Count; i++)
            {
                var previousTile = generator.CurrentDungeon.MainPathTiles[i - 1].gameObject;
                var currentTarget = generator.CurrentDungeon.MainPathTiles[i].gameObject;

                var previousTarget = previousTile.GetComponentInChildren<DoNothing>(true);

                if (previousTarget == null)
                {
                    continue;
                }

                // Calculate the direction vector from the previous target to the current target
                Vector3 direction = currentTarget.transform.position - previousTarget.transform.position;

                // Create a rotation that aligns the previous target's Z-axis with the direction vector
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

                // Apply the rotation to the previoustarget
                previousTarget.transform.rotation = rotation;

                arrows.Add(previousTarget.gameObject);
            }
        }
    }

    public void TriggerArrowHint()
    {
        foreach (var arrow in arrows)
        {
            arrow.SetActive(true);
        }

        ArrowHintMMFeedbacks.PlayFeedbacks(transform.position);
    }

    private void Update()
    {
        if (Input.GetKey("h") && 
            Input.GetKey("e") && 
            Input.GetKey("l") && 
            Input.GetKey("p"))
        {
            TriggerArrowHint();
        }
    }
}
