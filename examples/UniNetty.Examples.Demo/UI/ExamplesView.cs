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
        var size = ImGui.GetItemRectSize();
        if (32 >= size.X && 32 >= size.Y)
        {
            int width = 310;
            var posX = _canvas.Size.X - width;
            //ImGui.SetWindowPos(new Vector2(posX, 0));
            ImGui.SetWindowSize(new Vector2(width, _canvas.Size.Y - 60));
        }

        foreach (var example in _vm.Examples)
        {
            ImGui.Text(example.Example.Name);
            ImGui.Separator();
            if (ImGui.Button($"Run Server"))
            {
                example.RunServer();
            }

            ImGui.SameLine();
            if (ImGui.Button($"Run Client"))
            {
                example.RunClient();
            }

            // var width = ImGui.GetContentRegionAvail().X;
            // ImGui.SetNextItemWidth(width); // 입력 상자 너비 설정
            int port = example.Port;
            if (ImGui.InputInt(example.Example.Name + " Port", ref port)) ;
            {
                example.Port = port;
            }

            ImGui.NewLine();
        }

        ImGui.End();
    }
}