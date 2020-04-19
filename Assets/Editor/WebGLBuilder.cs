using UnityEditor;

class WebGLBuilder {
    static void build()
    {
        string[] scenes = {"Assets/Scenes/MainMenu.unity", "Assets/Scenes/Game.unity"};
        string deployPath = "builds/WebGL/";

        BuildPipeline.BuildPlayer(scenes, deployPath, BuildTarget.WebGL, BuildOptions.None);
    }
}
