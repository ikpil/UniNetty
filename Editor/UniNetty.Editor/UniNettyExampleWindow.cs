using System;
using System.Collections.Generic;
using UniNetty.Examples.DemoSupports;
using UnityEditor;
using UnityEngine;

namespace UniNetty.Editor
{
    public class UniNettyExampleWindow : EditorWindow
    {
        private Dictionary<UniNettyExampleType, UniNettyExample> _examples;

        private void OnDisable()
        {
            foreach (var knv in _examples)
            {
                knv.Value?.Stop();
            }

            _examples.Clear();
            _examples = null;
        }

        private void OnEnable()
        {
            var ip = UniNettyExampleSupports.GetPrivateIp();
            _examples = UniNettyExampleSupports.CreateDefaultExamples(null, ip);
        }

        // GUI 그리기
        private void OnGUI()
        {
            if (GUILayout.Button("Discard Server"))
            {
                _examples[UniNettyExampleType.Discard].ToggleServer();
            }
            
            if (GUILayout.Button("Discard Client"))
            {
                _examples[UniNettyExampleType.Discard].ToggleClient();
            }

        }
    }
}