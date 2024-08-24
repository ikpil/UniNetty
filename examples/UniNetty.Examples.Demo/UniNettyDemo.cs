using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime;
using System.Runtime.InteropServices;
using ImGuiNET;
using Serilog;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using UniNetty.Common;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo;

public class UniNettyDemo
{
    private static readonly ILogger Logger = Log.ForContext<UniNettyDemo>();

    private IWindow _window;
    private GL _gl;
    private IInputContext _input;
    private ImGuiController _imgui;

    private int[] _viewport;
    private Vector2D<int> _resolution;
    private int _width = 1000;
    private int _height = 900;

    public void Start(DemoContext context)
    {
        var monitor = Window.Platforms.First().GetMainMonitor();
        var resolution = monitor.VideoMode.Resolution.Value;

        float aspect = 16.0f / 9.0f;
        _width = Math.Min(resolution.X, (int)(resolution.Y * aspect)) - 100;
        _height = resolution.Y - 100;
        _viewport = new int[] { 0, 0, _width, _height };

        var options = WindowOptions.Default;
        options.Title = "UniNetty Demo";
        options.Size = new Vector2D<int>(_width, _height);
        options.Position = new Vector2D<int>((resolution.X - _width) / 2, (resolution.Y - _height) / 2);
        options.VSync = true;
        options.ShouldSwapAutomatically = false;
        options.PreferredDepthBufferBits = 24;
        _window = Window.Create(options);

        if (_window == null)
        {
            throw new Exception("Failed to create the GLFW window");
        }

        ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;

        _window.Closing += OnWindowClosing;
        _window.Load += OnWindowOnLoad;
        _window.Resize += OnWindowResize;
        _window.FramebufferResize += OnWindowFramebufferSizeChanged;
        _window.Update += OnWindowUpdate;
        _window.Render += OnWindowRender;

        _window.Run();

        // context.RunWebSocketServer();
        // context.RunTelnetServer();
        // context.RunSecureChatServer();
        // context.QuoteOfTheMomentServer();
        // context.RunHelloHttpServer();
        // context.RunFactorialServer();
        // context.RunEchoServer();
        // context.RunDiscardServer();
        //
        // context.RunWebSocketClient();
        // context.RunTelentClient();
        // context.RunSecureChatClient();
        // context.RunQuoteOfTheMomentClient();
        // context.RunFactorialClient();
        // context.RunEchoClient();
    }

    private void OnWindowClosing()
    {
    }

    private void OnWindowResize(Vector2D<int> size)
    {
        _width = size.X;
        _height = size.Y;
    }

    private void OnWindowFramebufferSizeChanged(Vector2D<int> size)
    {
        _gl.Viewport(size);
        _viewport = new int[] { 0, 0, _width, _height };
    }

    private void OnWindowOnLoad()
    {
        _input = _window.CreateInput();
        _gl = _window.CreateOpenGL();

        var scale = (float)_resolution.X / 1920;
        int fontSize = Math.Max(10, (int)(16 * scale));

        // for windows : Microsoft Visual C++ Redistributable Package
        // link - https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist
        _imgui = new ImGuiController(_gl, _window, _input);

        ImGui.GetStyle().ScaleAllSizes(scale);
        //ImGui.GetIO().FontGlobalScale = 2.0f;

        var vendor = _gl.GetStringS(GLEnum.Vendor);
        var version = _gl.GetStringS(GLEnum.Version);
        var rendererGl = _gl.GetStringS(GLEnum.Renderer);
        var glslString = _gl.GetStringS(GLEnum.ShadingLanguageVersion);
        var currentCulture = CultureInfo.CurrentCulture;
        string bitness = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";

        var workingDirectory = Directory.GetCurrentDirectory();


        Logger.Information($"{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}");
        Logger.Information($"{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}");
        Logger.Information($"Processor Count : {Environment.ProcessorCount}");
        Logger.Information("Transport type : Socket");

        Logger.Information($"Server garbage collection : {(GCSettings.IsServerGC ? "Enabled" : "Disabled")}");
        Logger.Information($"Current latency mode for garbage collection: {GCSettings.LatencyMode}");
        Logger.Information("");

        Logger.Information($"Working directory - {workingDirectory}");
        Logger.Information($"ImGui.Net - version({ImGui.GetVersion()}) UI scale({scale}) fontSize({fontSize})");
        Logger.Information($"Dotnet - {Environment.Version.ToString()} culture({currentCulture.Name})");
        Logger.Information($"OS Version - {Environment.OSVersion} {bitness}");
        Logger.Information($"{vendor} {rendererGl}");
        Logger.Information($"gl version({version}) lang version({glslString})");
    }

    private void OnWindowUpdate(double dt)
    {
        var io = ImGui.GetIO();

        io.DisplaySize = new Vector2(_width, _height);
        io.DisplayFramebufferScale = Vector2.One;
        io.DeltaTime = (float)dt;

        //_canvas.Update(dt);
        _imgui.Update((float)dt);
    }

    private void OnWindowRender(double dt)
    {
        _imgui.Render();
        _window.SwapBuffers();
    }
}