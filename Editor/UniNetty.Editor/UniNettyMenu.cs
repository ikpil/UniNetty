using UnityEditor;
using UnityEngine;

namespace UniNetty.Editor
{
    public class UniNettyMenu
    {
        [MenuItem("UniNetty/Examples")]
        public static void MyMenuItem()
        {
            var window = EditorWindow.GetWindow<UniNettyExampleWindow>();
            window.titleContent = new GUIContent("UniNetty Examples");
            window.Show();
        }
    }
}