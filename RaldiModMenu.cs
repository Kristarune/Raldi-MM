using BepInEx;
using UnityEngine;

[BepInPlugin("com.kristarune.raldimm", "Raldi Big Mod Menu", "1.0.0")]
public class RaldiModMenu : BaseUnityPlugin
{
    private bool showMenu = false;

    private bool infStamina = false;
    private bool noclip = false;
    private bool godMode = false;
    private bool infNotebooks = false;
    private bool freezeNPCs = false;

    private float speedMult = 3f;

    private PlayerScript player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            showMenu = !showMenu;

        if (player == null)
            player = FindObjectOfType<PlayerScript>();

        if (player != null)
        {
            if (infStamina)
                player.stamina = player.maxStamina;

            if (player.normSpeed > 0)
                player.walkSpeed = player.normSpeed * speedMult;
        }
    }

    private void OnGUI()
    {
        if (!showMenu) return;

        GUILayout.BeginArea(new Rect(20, 20, 500, 700), GUI.skin.window);
        GUILayout.Label("<b>🔥 Raldi's Crackhouse BIG Mod Menu</b>\nPress Tab to Open/Close");

        GUILayout.Space(15);
        GUILayout.Label("=== Player Cheats ===");
        infStamina = GUILayout.Toggle(infStamina, "Infinite Stamina");
        godMode = GUILayout.Toggle(godMode, "God Mode");
        noclip = GUILayout.Toggle(noclip, "Noclip");

        GUILayout.Label($"Player Speed: {speedMult:F1}x");
        speedMult = GUILayout.HorizontalSlider(speedMult, 0.5f, 50f);

        GUILayout.Space(15);
        GUILayout.Label("=== Game Cheats ===");
        infNotebooks = GUILayout.Toggle(infNotebooks, "Infinite Notebooks");
        freezeNPCs = GUILayout.Toggle(freezeNPCs, "Freeze All NPCs");

        GUILayout.Space(15);
        if (GUILayout.Button("Give All Items")) Debug.Log("[Mod] Give All Items");
        if (GUILayout.Button("Teleport to Exit")) Debug.Log("[Mod] Teleport to Exit");
        if (GUILayout.Button("Complete Level")) Debug.Log("[Mod] Complete Level");
        if (GUILayout.Button("Spawn Item")) Debug.Log("[Mod] Spawn Item");

        GUILayout.EndArea();
    }
}
