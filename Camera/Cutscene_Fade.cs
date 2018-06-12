using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_Fade : MonoBehaviour {

    public Texture2D fadeTexture;  // Texture that overlays the screen.
    public float fadeSpeed = 0.8f; // Fade out speed
    private int fadeDir;           // Direction to fade: in = -1 or out = 1

    private int drawDepth = -1000;  // Texture's order in the draw hierarchy, a low # means it renders on top.
    private float alpha = 1.0f;    // texture's alpha value between 0 and 1.

    void OnGUI()
    {
        // Fade out/in the alpha value using a direction, a speed, and Time.deltaTime to conver the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        // Force (clamp) the # btw 0 & 1 because GUI.color uses alpha values between 0 & 1
        alpha = Mathf.Clamp01(alpha);

        // Sets color of our texture. All color values remain the same & the Alpha is set to the alpha variable
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);        // Sets the alpha value
        GUI.depth = drawDepth;                                                      // Make the black texture render on top (drawn last)
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);  // Draw the texture to fit the entire screen
    }

    // Sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int dir)
    {
        fadeDir = dir;
        return (fadeSpeed);  // Return the fadeSpeed variable so that it's easy to time the Application.LoadLevel();
    }

    void SceneLoaded()
    {
        BeginFade(-1);  // Call the fade in function;
    }
}
