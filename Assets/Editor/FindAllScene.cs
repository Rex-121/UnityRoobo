//using System.Collections;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.SceneManagement;
//using UnityEngine.SceneManagement;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class FindAllSingltonSO : OdinMenuEditorWindow
{

    [MenuItem("Roobo/单例")]
    static void OpenWindow()
    {
        GetWindow<FindAllSingltonSO>("单例").Show();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.AddAllAssetsAtPath(null, "Assets/Resources/Singleton", typeof(ScriptableObject));
        return tree;
    }
}


//public class FindAllScene : EditorWindow
//{

//    string myString = "Hello World";
//    bool groupEnabled;
//    bool myBool = true;
//    float myFloat = 1.23f;


//    public Rect windowRect = new Rect(0, 0, 200, 200);//子窗口的大小和位置

//    // Add menu named "My Window" to the Window menu
//    [MenuItem("Roobo/所有场景")]
//    static void Init()
//    {
//        // Get existing open window or if none, make a new one:
//        FindAllScene window = (FindAllScene)EditorWindow.GetWindow(typeof(FindAllScene), false, "所有场景", true);
//        window.Show();
//    }

//    void OnGUI()
//    {
//        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
//        myString = EditorGUILayout.TextField("Text Field", myString);

//        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
//        myBool = EditorGUILayout.Toggle("Toggle", myBool);
//        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);



//        //foreach (var s in UnityEditor.EditorBuildSettings.scenes)
//        //{

//        //}
//        //var d = EditorSceneManager.GetAllScenes();


//        var d = Resources.LoadAll<ScriptableObject>("Singleton");

//        //var kk = AssetDatabase..FindAssets("t:Scene");

//        //foreach (var da in kk)
//        //{
//        //    var a = EditorGUILayout.TextField("fadf", da);
//        //}


//        foreach (var da in d)
//        {
//            var a = EditorGUILayout.ObjectField(da.name, da, typeof(ScriptableObject) ,allowSceneObjects: true);
//            var k = Editor.CreateEditor(da);
//            k.OnPreviewGUI(GUILayoutUtility.GetRect(200, 200), EditorStyles.label);
//        }



//        EditorGUILayout.EndToggleGroup();
//        //BeginWindows();//标记开始区域所有弹出式窗口
//        //windowRect = GUILayout.Window(1, windowRect, DoWindow, "子窗口");//创建内联窗口,参数分别为id,大小位置，创建子窗口的组件的函数，标题
//        //EndWindows();//

//    }


//    void DoWindow(int unusedWindowID)
//    {
//        GUILayout.Button("按钮");//创建button
//        GUI.DragWindow();//画出子窗口
//    }
//}
