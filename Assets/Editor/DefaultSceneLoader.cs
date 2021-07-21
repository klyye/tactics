#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
///     https://stackoverflow.com/questions/35586103/unity3d-load-a-specific-scene-on-play-mode
///     https://stackoverflow.com/questions/40577412/clear-editor-console-logs-from-script
/// </summary>
[InitializeOnLoadAttribute]
public static class DefaultSceneLoader
{
    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    private static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode) EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            SceneManager.LoadScene(0);
            ClearLog();
        }
    }

    private static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
#endif