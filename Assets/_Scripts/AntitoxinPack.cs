using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

[AddComponentMenu("TopDown Engine/Items/AntitoxinPack")]
public class AntitoxinPack : PickableItem
{
    [Header("AntitoxinPack")]
    public float TimeToAdd = 30f;
    /// if this is true, only player characters can pick this up
    [Tooltip("if this is true, only player characters can pick this up")]
    public bool OnlyForPlayerCharacter = true;
    private PoisonBar poisonbar;

    protected override void Start()
    {
        base.Start();
        poisonbar = GameObject.FindGameObjectWithTag("PoisonBar").GetComponent<PoisonBar>();
    }
  
    /// <summary>
    /// Triggered when something collides with the stimpack
    /// </summary>
    /// <param name="collider">Other.</param>
    protected override void Pick(GameObject picker)
    {
        Character character = picker.gameObject.MMGetComponentNoAlloc<Character>();
        if (OnlyForPlayerCharacter && (character != null) && (_character.CharacterType != Character.CharacterTypes.Player))
        {
            return;
        }

        if (poisonbar == null)
        {
            poisonbar = GameObject.FindGameObjectWithTag("PoisonBar").GetComponent<PoisonBar>();
        }

        // else, we give health to the player
        if (poisonbar != null)
        {
            poisonbar.AddSeconds(TimeToAdd);
        }
    }
}
