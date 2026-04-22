// ========================================
// AI EXTENSION HINTS
// Stable: the existing enum values are the contract with trixie's audio deliveries.
// Extend: add new keys at the END of the enum, never reorder or remove existing values.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

namespace VibeJam.Audio
{
    public enum SfxKey
    {
        None = 0,
        Jump,
        Pickup,
        Hit,
        Fail,
        UIClick,
        UIBack,
        Win,
        Tick,
    }

    public enum BgmKey
    {
        None = 0,
        Main,
    }
}
