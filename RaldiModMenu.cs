using BepInEx;
using UnityEngine;

[BepInPlugin("com.kristarune.raldimm", "Raldi Mod Menu", "1.0.0")]
public class RaldiModMenu : BaseUnityPlugin
{
    private bool showMenu = false;
    private bool infStamina = false;
    private bool noclip = false;
    private bool godMode = false;
    private float speedMult = 2f;

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

            player.walkSpeed = player.normSpeed * speedMult;
        }
    }

    private void OnGUI()
    {
        if (!showMenu) return;

        GUILayout.BeginArea(new Rect(30, 30, 400, 550), GUI.skin.window);
        GUILayout.Label("<b>Raldi's Crackhouse Mod Menu</b>\nPress Tab to toggle");

        infStamina = GUILayout.Toggle(infStamina, "Infinite Stamina");
        noclip = GUILayout.Toggle(noclip, "Noclip");
        godMode = GUILayout.Toggle(godMode, "God Mode");

        GUILayout.Label($"Speed: {speedMult:F1}x");
        speedMult = GUILayout.HorizontalSlider(speedMult, 0.5f, 30f);

        if (GUILayout.Button("Give All Items")) Debug.Log("Give All Items");
        if (GUILayout.Button("Teleport to Exit")) Debug.Log("Teleport");
        if (GUILayout.Button("Freeze NPCs")) Debug.Log("Freeze NPCs");

        GUILayout.EndArea();
    }
}
