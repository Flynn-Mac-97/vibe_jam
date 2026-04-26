// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): HUD text/image binding, ability polling, phase cooldown display.
// Extend (GAMEPLAY): call SetHealth, SetKeyCount, ShowDeath, and ShowVictory from gameplay triggers.
// Requires: Canvas with TextMeshProUGUI labels and optional UnityEngine.UI.Image fills.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VibeJam.Player;

namespace VibeJam.UI
{
    public class PlayerHudController : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Header("Player")]
        [SerializeField] private PlayerAbilityController abilityController;

        [Header("Health")]
        [SerializeField] private Image healthFill;
        [SerializeField] private Image[] healthHearts;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int currentHealth = 3;
        [SerializeField] private Color filledHeartColor = new Color(0.68f, 0.08f, 0.07f, 1f);
        [SerializeField] private Color emptyHeartColor = new Color(0.18f, 0.12f, 0.11f, 0.55f);

        [Header("Ability")]
        [SerializeField] private TMP_Text zoneAbilityText;
        [SerializeField] private Image phaseCooldownFill;
        [SerializeField] private TMP_Text phaseCooldownText;

        [Header("Goal")]
        [SerializeField] private TMP_Text objectiveText;
        [SerializeField] private TMP_Text keyGateText;
        [SerializeField] private TMP_Text controlsText;
        [SerializeField] private int requiredKeys = 1;
        [SerializeField] private int collectedKeys;

        [Header("Overlays")]
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private TMP_Text deathText;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private TMP_Text victoryText;

        private void Awake()
        {
            if (abilityController == null)
                abilityController = FindObjectOfType<PlayerAbilityController>();

            EnsureHealthHearts();

            if (zoneAbilityText != null)
                zoneAbilityText.gameObject.SetActive(false);

            if (objectiveText != null)
                objectiveText.gameObject.SetActive(false);

            if (controlsText != null)
                controlsText.gameObject.SetActive(false);

            if (phaseCooldownText != null)
                phaseCooldownText.gameObject.SetActive(false);

            if (phaseCooldownFill != null && phaseCooldownFill.transform.parent != null)
                phaseCooldownFill.transform.parent.gameObject.SetActive(false);

            if (healthFill != null)
                healthFill.gameObject.SetActive(false);

            if (healthText != null)
                healthText.gameObject.SetActive(false);

            SetHealth(currentHealth, maxHealth);
            SetKeyCount(collectedKeys, requiredKeys);
            ShowDeath(false);
            ShowVictory(false);
        }

        private void Update()
        {
            UpdatePhaseCooldown();
        }

        public void SetHealth(int value, int maximum)
        {
            EnsureHealthHearts();

            maxHealth = Mathf.Max(1, maximum);
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

            if (healthFill != null)
                healthFill.fillAmount = (float)currentHealth / maxHealth;

            if (healthHearts != null)
            {
                for (int i = 0; i < healthHearts.Length; i++)
                {
                    if (healthHearts[i] == null)
                        continue;

                    bool inRange = i < maxHealth;
                    healthHearts[i].gameObject.SetActive(inRange);
                    if (healthHearts[i].transform.parent != null)
                        healthHearts[i].transform.parent.gameObject.SetActive(true);

                    healthHearts[i].color = i < currentHealth ? filledHeartColor : emptyHeartColor;
                }
            }

            if (healthText != null)
                healthText.gameObject.SetActive(false);
        }

        private void EnsureHealthHearts()
        {
            if (healthHearts != null && healthHearts.Length >= maxHealth && healthHearts[0] != null)
                return;

            Image[] existingImages = GetComponentsInChildren<Image>(true);
            Image[] discoveredHearts = new Image[maxHealth];
            int discoveredCount = 0;

            for (int i = 0; i < existingImages.Length; i++)
            {
                if (!existingImages[i].gameObject.name.StartsWith("HealthHeart"))
                    continue;

                if (discoveredCount >= discoveredHearts.Length)
                    break;

                discoveredHearts[discoveredCount] = existingImages[i];
                discoveredCount++;
            }

            if (discoveredCount < maxHealth)
            {
            Transform parent = healthText != null && healthText.transform.parent != null
                ? healthText.transform.parent
                : transform;

            parent.gameObject.SetActive(true);

            for (int i = discoveredCount; i < maxHealth; i++)
                discoveredHearts[i] = CreateHealthHeart(parent, i);
            }

            healthHearts = discoveredHearts;
        }

        private Image CreateHealthHeart(Transform parent, int index)
        {
            GameObject heart = new GameObject($"HealthHeart{index + 1}", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            heart.transform.SetParent(parent, false);

            RectTransform rect = heart.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = new Vector2(34f + index * 40f, -22f);
            rect.sizeDelta = new Vector2(36f, 36f);

            Image image = heart.GetComponent<Image>();
            image.sprite = CreateHeartSprite();
            image.raycastTarget = false;
            image.transform.SetAsLastSibling();
            return image;
        }

        private static Sprite CreateHeartSprite()
        {
            const int size = 64;
            Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float px = (x + 0.5f) / size;
                    float py = (y + 0.5f) / size;
                    bool leftLobe = (px - 0.34f) * (px - 0.34f) + (py - 0.66f) * (py - 0.66f) < 0.20f * 0.20f;
                    bool rightLobe = (px - 0.66f) * (px - 0.66f) + (py - 0.66f) * (py - 0.66f) < 0.20f * 0.20f;
                    bool body = py <= 0.68f && py >= 0.12f && Mathf.Abs(px - 0.50f) < (py - 0.12f) * 0.72f;
                    bool shoulder = py <= 0.64f && py >= 0.38f && Mathf.Abs(px - 0.50f) < 0.33f;
                    bool inside = leftLobe || rightLobe || body || shoulder;

                    texture.SetPixel(x, y, inside ? Color.white : Color.clear);
                }
            }

            texture.Apply();
            return Sprite.Create(texture, new Rect(0f, 0f, size, size), new Vector2(0.5f, 0.5f), size);
        }

        public void SetKeyStatus(bool hasKey, bool gateOpen)
        {
            SetKeyCount(hasKey ? 1 : 0, requiredKeys);
        }

        public void SetKeyCount(int collected, int required)
        {
            requiredKeys = Mathf.Max(1, required);
            collectedKeys = Mathf.Clamp(collected, 0, requiredKeys);

            if (keyGateText != null)
                keyGateText.text = $"KEY {collectedKeys}/{requiredKeys}";
        }

        public void ShowDeath(bool visible)
        {
            if (deathPanel != null)
                deathPanel.SetActive(visible);

            if (deathText != null)
                deathText.text = "YOU FELL\nPress R to restart";
        }

        public void ShowVictory(bool visible)
        {
            if (victoryPanel != null)
                victoryPanel.SetActive(visible);

            if (victoryText != null)
                victoryText.text = "GATE REACHED\nPress R to play again";
        }

        private void UpdatePhaseCooldown()
        {
            if (phaseCooldownFill != null && !phaseCooldownFill.gameObject.activeInHierarchy)
                return;

            if (phaseCooldownText != null && !phaseCooldownText.gameObject.activeInHierarchy)
                return;

            float ready = abilityController != null ? abilityController.PhaseReady01 : 1f;

            if (phaseCooldownFill != null)
                phaseCooldownFill.fillAmount = ready;

            if (phaseCooldownText != null)
                phaseCooldownText.text = ready >= 0.99f ? "PHASE READY" : "PHASE CHARGING";
        }

    }
}
