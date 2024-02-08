using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SchemaStyles
{
    static SchemaStyles()
    {

        //Schema inspector dividers
        Divider = new GUIStyle(GUI.skin.textField);

        //Schema Header bar
        HeaderLabel = new GUIStyle();
        HeaderLabel.stretchWidth = true;
        HeaderLabel.alignment = TextAnchor.MiddleLeft;

        //Centralized fixed button
        CenterButton = new GUIStyle(GUI.skin.button);
        CenterButton.fontSize = 12;
        CenterButton.fixedWidth = 225;
        CenterButton.fixedHeight = 22;

        CornerIcon = new GUIStyle(GUIStyle.none);
        CornerIcon.alignment = TextAnchor.MiddleRight;
        //CornerIcon.padding.bottom += 1;
    }


    public static GUIStyle Divider;

    public static GUIStyle HeaderLabel;

    public static GUIStyle CenterButton;

    public static GUIStyle CornerIcon;
}
