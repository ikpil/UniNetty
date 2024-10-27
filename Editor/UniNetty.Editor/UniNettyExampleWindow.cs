using UniNetty.Examples.Discard.Client;
using UniNetty.Examples.Discard.Server;
using UnityEditor;
using UnityEngine;

namespace UniNetty.Editor
{
    public class UniNettyExampleWindow : EditorWindow
    {
        public void ExampleDiscard()
        {
            var server = new DiscardServer();
            var client = new DiscardClient();
        }
        
        // GUI 그리기
        private void OnGUI()
        {
            if (GUILayout.Button("Discard"))
            {
                ExampleDiscard();
            }
        }
    }
}