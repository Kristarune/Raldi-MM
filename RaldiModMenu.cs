using BepInEx;
using UnityEngine;

[BepInPlugin("com.kristarune.raldimm", "Raldi Big Mod Menu", "1.1.0")]
public class RaldiModMenu : BaseUnityPlugin
{
    private bool showMenu;
    private bool infStamina;
    private bool noclip;
    private bool godMode;
    private float speedMult = 1f;
    private PlayerScript player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            showMenu = !showMenu;

        if (player == null)
            player = FindObjectOfType<PlayerScript>();

        if (player == null)
            return;

        if (infStamina)
            player.stamina = player.maxStamina;

        player.walkSpeed = 5f * speedMult;

        if (noclip)
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
        }
        else
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc != null && !cc.enabled) cc.enabled = true;
        }
    }

    private void OnGUI()
    {
        if (!showMenu) return;

        GUILayout.BeginArea(new Rect(20,20,500,650), GUI.skin.window);

        GUILayout.Label("Raldi Big Mod Menu");
        GUILayout.Label(player != null ? "Player Found" : "Player Not Found");

        infStamina = GUILayout.Toggle(infStamina, "Infinite Stamina");
        godMode = GUILayout.Toggle(godMode, "God Mode (WIP)");
        noclip = GUILayout.Toggle(noclip, "Noclip");

        GUILayout.Label($"Speed: {speedMult:F1}x");
        speedMult = GUILayout.HorizontalSlider(speedMult, 0.5f, 20f);

        if (GUILayout.Button("Give All Items"))
            Debug.Log("Not implemented yet");

        if (GUILayout.Button("Teleport To Exit"))
            Debug.Log("Not implemented yet");

        if (GUILayout.Button("Complete Level"))
        {
            try
            {
                GameControllerScript.GetGameController().OnGameWin();
            }
            catch { }
        }

        GUILayout.EndArea();
    }
}
