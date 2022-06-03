using System;
using UnityEngine;
using Tymski;

public class CustomReadme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public Scenes[] scenes;
    public bool loadedLayout;
   
    

    [Serializable]
    public class Section
    {
        public string heading, linkText, url;
        [TextArea(3,20)]
        public string text;
        public string objectText;
        public UnityEngine.Object objectToOpen;
    }

    [Serializable]
    public class Scenes
    {
        public SceneReference scene;
        public string name;
    }
}
