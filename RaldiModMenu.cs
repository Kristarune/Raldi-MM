using BepInEx;
using UnityEngine;

[BepInPlugin("com.kristarune.raldimm", "Raldi Big Mod Menu", "1.0.0")]
public class RaldiModMenu : BaseUnityPlugin
{
    private bool showMenu = false;

    private bool infStamina = false;
    private bool godMode = false;
    private bool noclip = false;

    private float speedMult = 3f;

    private PlayerScript player;
    private CharacterController cc;
    private GameControllerScript gc;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            showMenu = !showMenu;

        if (player == null) player = FindObjectOfType<PlayerScript>();
        if (gc == null) gc = FindObjectOfType<GameControllerScript>();

        if (player != null)
        {
            if (cc == null)
                cc = player.GetComponent<CharacterController>();

            // Infinite Stamina
            if (infStamina)
                player.stamina = player.maxStamina;

            // Speed
            player.walkSpeed = 5f * speedMult;

            // Noclip
            if (cc != null)
                cc.enabled = !noclip;

            if (noclip)
            {
                float speed = 25f * speedMult;
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                move = player.transform.TransformDirection(move);
                move.y = Input.GetKey(KeyCode.Space) ? 1f : Input.GetKey(KeyCode.LeftControl) ? -1f : 0f;
                player.transform.position += move * speed * Time.deltaTime;
            }
        }
    }

    private void OnGUI()
    {
        if (!showMenu) return;

        GUILayout.BeginArea(new Rect(20, 20, 520, 750), GUI.skin.window);
        GUILayout.Label("<b>🔥 Raldi's Crackhouse BIG Mod Menu</b>\nTab to toggle");

        GUILayout.Space(15);
        GUILayout.Label("=== Player Cheats ===");
        infStamina = GUILayout.Toggle(infStamina, "Infinite Stamina");
        godMode = GUILayout.Toggle(godMode, "God Mode");
        noclip = GUILayout.Toggle(noclip, "Noclip (WASD + Space/Ctrl)");

        GUILayout.Label($"Speed: {speedMult:F1}x");
        speedMult = GUILayout.HorizontalSlider(speedMult, 0.5f, 50f);

        GUILayout.Space(15);
        GUILayout.Label("=== Actions ===");
        if (GUILayout.Button("Give All Items")) GiveAllItems();
        if (GUILayout.Button("Teleport to Exit")) TeleportToExit();
        if (GUILayout.Button("Complete Level")) CompleteLevel();
        if (GUILayout.Button("Freeze All NPCs")) Debug.Log("Freeze NPCs - coming soon");

        GUILayout.EndArea();
    }

    private void GiveAllItems()
    {
        Debug.Log("[Mod] Giving items...");
        // This is basic for now, can be improved later
    }

    private void TeleportToExit()
    {
        if (player != null)
        {
            Debug.Log("[Mod] Teleporting to nearest exit...");
            // Simple teleport forward for now
            player.transform.position += player.transform.forward * 20f;
        }
    }

    private void CompleteLevel()
    {
        if (gc != null)
        {
            Debug.Log("[Mod] Completing level...");
            // Try to force win condition
            gc.notebooks = 999;
        }
    }
}
