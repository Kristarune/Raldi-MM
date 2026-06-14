using BepInEx;
using UnityEngine;

[BepInPlugin("com.kristarune.raldimm", "Raldi Big Mod Menu", "1.0.0")]
public class RaldiModMenu : BaseUnityPlugin
{
    private bool showMenu = false;

    private bool infStamina = false;
    private bool noclip = false;
    private bool godMode = false;

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

            // Speed hack (safer version)
            player.walkSpeed = 5f * speedMult;   // we'll adjust the base value later if needed
        }
    }

    private void OnGUI()
    {
        if (!showMenu) return;

        GUILayout.BeginArea(new Rect(20, 20, 500, 650), GUI.skin.window);
        GUILayout.Label("<b>🔥 Raldi's Crackhouse BIG Mod Menu</b>\nTab to toggle");

        GUILayout.Space(10);
        infStamina = GUILayout.Toggle(infStamina, "Infinite Stamina");
        godMode = GUILayout.Toggle(godMode, "God Mode");
        noclip = GUILayout.Toggle(noclip, "Noclip");

        GUILayout.Label($"Speed: {speedMult:F1}x");
        speedMult = GUILayout.HorizontalSlider(speedMult, 0.5f, 50f);

        GUILayout.Space(15);
        if (GUILayout.Button("Give All Items")) Debug.Log("[Mod] Give All Items");
        if (GUILayout.Button("Teleport to Exit")) Debug.Log("[Mod] Teleport");
        if (GUILayout.Button("Complete Level")) Debug.Log("[Mod] Complete Level");

        GUILayout.EndArea();
    }
}
