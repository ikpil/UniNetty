using System;
using System.IO;
using System.Numerics;
using ImGuiNET;
using Serilog;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class ExamplesView : IView
{
    private static readonly ILogger Logger = Log.ForContext<UniNettyDemo>();

    private readonly Canvas _canvas;
    private readonly ExamplesViewModel _vm;

    public ExamplesView(Canvas canvas)
    {
        _canvas = canvas;
        _vm = new ExamplesViewModel(canvas.Context);
    }

    public void Draw(double dt)
    {
        ImGui.Begin("Examples");

        // size reset
        var rectSize = ImGui.GetItemRectSize();
        if (32 >= rectSize.X && 32 >= rectSize.Y)
        {
            int width = 310;
            var posX = _canvas.Size.X - width;
            //ImGui.SetWindowPos(new Vector2(posX, 0));
            ImGui.SetWindowSize(new Vector2(width, _canvas.Size.Y - 60));
        }

        foreach (var example in _vm.Examples)
        {
            if (null == example)
                continue;

            var showSettings = ImGui.TreeNode(example.Example.Name);
            if (showSettings)
            {
                ImGui.Text(example.Example.Name);
                ImGui.Separator();

                var btnServer = example.IsRunningServer ? "Stop Server" : "Run Server";
                if (ImGui.Button(example.Example.Name + " " + btnServer))
                {
                    example.ToggleServer();
                }

                var btnClient = example.IsRunningClient ? "Stop Client" : "Run Client";
                if (ImGui.Button(example.Example.Name + " " + btnClient))
                {
                    example.ToggleClient();
                }

                int port = example.Port;
                if (ImGui.InputInt(example.Example.Name + " Port", ref port)) ;
                {
                    example.SetPort(port);
                }

                // use size
                if (0 != example.Size)
                {
                    int size = example.Size;
                    if (ImGui.InputInt(example.Example.Name + " Size", ref size))
                    {
                        example.SetSize(size);
                    }
                }

                ImGui.TreePop();
            }

            ImGui.Separator(); // 구분선
        }

        ImGui.End();
    }
}