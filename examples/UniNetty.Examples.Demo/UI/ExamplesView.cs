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

    public ExamplesView(Canvas canvas)
    {
        _canvas = canvas;
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

        foreach (var example in DemoType.Values)
        {
            int port = example.Port;
            ImGui.Text(example.Name);
            ImGui.Separator();
            if (ImGui.Button($"Run Server"))
            {
            }

            ImGui.SameLine();
            if (ImGui.Button($"Run Client"))
            {
            }

            ImGui.SameLine();
            // var width = ImGui.GetContentRegionAvail().X;
            // ImGui.SetNextItemWidth(width); // 입력 상자 너비 설정
            ImGui.InputInt("", ref port, 0, 0, ImGuiInputTextFlags.CallbackCharFilter);

            ImGui.NewLine();
        }

        ImGui.End();
    }
}