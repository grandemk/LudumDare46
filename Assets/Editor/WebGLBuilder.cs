using UnityEditor;

class WebGLBuilder {
    static void build()
    {
        string[] scenes = {"Assets/Scenes/SampleScene.unity"};
        string deployPath = "builds/WebGL/";

        BuildPipeline.BuildPlayer(scenes, deployPath, BuildTarget.WebGL, BuildOptions.None);
    }
}