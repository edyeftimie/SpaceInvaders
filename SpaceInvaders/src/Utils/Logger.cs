using System;
using System.IO;
using System.Diagnostics;

public static class Logger
{
    private static string _logFilePath = "game.log"; // Default log file name
    private static bool _enableFileLogging = true;
    private static bool _enableConsoleLogging = true;
    private static bool _enableDebugOutput = true; // Output to Debug window in IDE

    public static void Initialize(string? logFilePath = null, bool enableFile = true, bool enableConsole = true, bool enableDebug = true)
    {
        _logFilePath = logFilePath ?? _logFilePath;
        _enableFileLogging = enableFile;
        _enableConsoleLogging = enableConsole;
        _enableDebugOutput = enableDebug;

        if (_enableFileLogging)
        {
            try
            {
                // Create or clear the log file on initialization
                File.WriteAllText(_logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Logger Initialized.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing file logger: {ex.Message}");
                _enableFileLogging = false; // Disable if there's an error
            }
        }
    }

    public static void Log(string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [INFO] {message}";
        WriteLogEntry(logEntry);
    }

    public static void Warning(string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [WARN] {message}";
        WriteLogEntry(logEntry);
    }

    public static void Error(string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [ERROR] {message}";
        WriteLogEntry(logEntry);
    }

    private static void WriteLogEntry(string logEntry)
    {
        if (_enableConsoleLogging)
        {
            Console.WriteLine(logEntry);
        }

        if (_enableDebugOutput)
        {
            Debug.WriteLine(logEntry);
        }

        if (_enableFileLogging)
        {
            try
            {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
                _enableFileLogging = false; // Disable file logging if errors occur
            }
        }
    }
}