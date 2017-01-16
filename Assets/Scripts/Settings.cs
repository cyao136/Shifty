using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    // Primes used in the modification of seed
    public int[] primes;

    // Default pattern
    static private Color default_top = new Color32(2, 193, 20, 255);
    static private Color default_right = new Color32(221, 28, 26, 255);
    static private Color default_left = new Color32(240, 200, 8, 255);
    static private Color default_bottom = new Color32(7, 160, 195, 255);

    // High Contrast pattern
    static private Color hc_top = new Color32(255, 128, 0, 255);
    static private Color hc_right = new Color32(255, 0, 255, 255);
    static private Color hc_left = new Color32(240, 200, 8, 255);
    static private Color hc_bottom = new Color32(0, 255, 255, 255);

    // Special Colors
    static private Color grey = new Color32(150, 150, 150, 255);

    public Color[] colors;

    // Use this for initialization
    void Start ()
    {
        colors = new Color[8];

        // Default colors
        ChangeColorPattern("default");

        colors[4] = grey;

        primes = new int[7] { -3, 7, 13, 17, 23, -43, -53 };
    }

    void ChangeColorPattern(string pat)
    {
        switch (pat) {
            case "default":
                colors[0] = default_top;
                colors[1] = default_right;
                colors[2] = default_left;
                colors[3] = default_bottom;
                break;

            case "hc":
                colors[0] = hc_top;
                colors[1] = hc_right;
                colors[2] = hc_left;
                colors[3] = hc_bottom;
                break;

            default:
                break;
        }
    }
}
