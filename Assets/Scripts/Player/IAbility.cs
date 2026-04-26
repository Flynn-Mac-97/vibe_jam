// AI EXTENSION HINTS
// Stable (INFRA): interface contract — do not change method signatures.
// Extend (GAMEPLAY): implement on any MonoBehaviour on the Player to create new abilities.
// This script follows VibeJam conventions — see agents/AGENTS.md.

namespace VibeJam.Player
{
    /// <summary>
    /// Modular ability contract. Implement on a MonoBehaviour attached to the Player.
    /// PlayerAbilityController discovers implementations via GetComponents and drives them.
    /// </summary>
    public interface IAbility
    {
        /// <summary>Called once when the player enters a zone that grants this ability type.</summary>
        void OnActivate();

        /// <summary>Called once when the player leaves the zone or zones change.</summary>
        void OnDeactivate();

        /// <summary>
        /// Called every Update frame while the ability is active, inside PlayerController.Update
        /// just before CharacterController.Move — safe to call SetVerticalVelocity here.
        /// </summary>
        void Execute(PlayerController player);

        /// <summary>Identifies which zone type activates this ability.</summary>
        AbilityType AbilityType { get; }
    }

    /// <summary>Matches the AbilityType enum used by AbilityZone.</summary>
    public enum AbilityType
    {
        None,
        Jump,
        Fall
    }
}
