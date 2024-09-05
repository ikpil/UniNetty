using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;
using Serilog;
using UniNetty.Examples.Demo.Logging.Sinks;

namespace UniNetty.Examples.Demo.UI;

public class LogView : IView
{
    private static readonly ILogger Logger = Log.ForContext<LogView>();

    private Canvas _canvas;

    private readonly List<LogViewItem> _lines;
    private readonly ConcurrentQueue<LogViewItem> _queues;


    public LogView(Canvas canvas)
    {
        _canvas = canvas;
        _lines = new();
        _queues = new();

        LogMessageBrokerSink.OnEmitted += OnOut;
    }

    private void OnOut(int level, string message)
    {
        var lines = message
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new LogViewItem { Level = level, Message = x });

        foreach (var line in lines)
        {
            _queues.Enqueue(line);
        }
    }

    public void Clear()
    {
        _lines.Clear();
    }

    public void Update(double dt)
    {
        while (_queues.TryDequeue(out var item))
            _lines.Add(item);

        // buffer
        if (10240 < _lines.Count)
        {
            _lines.RemoveRange(0, _lines.Count - 8196);
        }
    }


    public void Draw(double dt)
    {
        if (!ImGui.Begin("Log"))
        {
            ImGui.End();
            return;
        }

        // size reset
        var size = ImGui.GetItemRectSize();
        if (32 >= size.X && 32 >= size.Y)
        {
            int otherWidth = 310;
            int height = 234;
            var width = _canvas.Size.X - (otherWidth * 2);
            //var posX = _canvas.Size.X - width;
            // ImGui.SetNextWindowPos(new Vector2(otherWidth1, _canvas.Size.Y - height));
            ImGui.SetWindowSize(new Vector2(width, height));
        }


        ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarSize, 16.0f);
        
        if (ImGui.BeginChild("scrolling", Vector2.Zero, false, ImGuiWindowFlags.HorizontalScrollbar))
        {
            //_isHovered = ImGui.IsWindowHovered(ImGuiHoveredFlags.RectOnly | ImGuiHoveredFlags.RootAndChildWindows);

            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, Vector2.Zero);

            unsafe
            {
                var clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
                clipper.Begin(_lines.Count);
                while (clipper.Step())
                {
                    for (int lineNo = clipper.DisplayStart; lineNo < clipper.DisplayEnd; lineNo++)
                    {
                        ImGui.TextUnformatted(_lines[lineNo].Message);
                    }
                }

                clipper.End();
                clipper.Destroy();
            }

            ImGui.PopStyleVar();

            // if (ImGui.GetScrollY() >= ImGui.GetScrollMaxY())
            // {
            //     ImGui.SetScrollHereY(1.0f);
            // }
            // 현재 스크롤 위치를 저장합니다.
            float scrollY = ImGui.GetScrollY();
            float scrollMaxY = ImGui.GetScrollMaxY();
    
            // 스크롤이 끝에 도달한 상태에서만 자동 스크롤을 수행하도록 합니다.
            if (scrollY >= scrollMaxY - ImGui.GetWindowHeight() * 0.1f)
            {
                ImGui.SetScrollHereY(1.0f);
            }
        }

        ImGui.EndChild();
        ImGui.PopStyleVar();
        
        ImGui.End();
    }
}