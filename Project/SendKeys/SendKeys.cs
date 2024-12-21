using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Rainmeter;

namespace PluginSendKeys
{
    public class Measure
    {
        private string keysToSend = "";
        private int delay = 0;
        private Dictionary<string, byte> keyMapping;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

        public Measure()
        {
            InitializeKeyMapping();
        }

        public void Reload(API api)
        {
            keysToSend = api.ReadString("Keys", "").Trim();
            delay = api.ReadInt("Delay", 0); // Delay parameter in milliseconds

            if (string.IsNullOrEmpty(keysToSend))
            {
                api.Log(API.LogType.Error, "SendKeys.dll: No Keys parameter provided.");
            }
            else
            {
                api.Log(API.LogType.Debug, $"SendKeys.dll: Keys to send - '{keysToSend}'");
                api.Log(API.LogType.Debug, $"SendKeys.dll: Delay set to {delay} milliseconds.");
            }
        }

        public void ExecuteCommand(string command, API api)
        {
            if (command.Equals("Send", StringComparison.OrdinalIgnoreCase))
            {
                SimulateKeys(api);
            }
            else
            {
                api.Log(API.LogType.Warning, $"SendKeys.dll: Unknown command '{command}'.");
            }
        }

        private void SimulateKeys(API api)
        {
            if (string.IsNullOrEmpty(keysToSend))
            {
                api.Log(API.LogType.Error, "SendKeys.dll: No keys defined to simulate.");
                return;
            }

            string[] keyGroups = keysToSend.Split(' ');

            foreach (string group in keyGroups)
            {
                Thread keyThread = new Thread(() => SimulateKeyGroup(group, api));
                keyThread.Start();
                keyThread.Join(); // Wait for each key group
                Thread.Sleep(delay); // Apply delay between key groups
            }
        }

        private void SimulateKeyGroup(string keyGroup, API api)
        {
            string[] keys = keyGroup.Split('+');
            List<byte> keyCodes = new List<byte>();

            // Press each key
            foreach (string key in keys)
            {
                if (keyMapping.TryGetValue(key.Trim(), out byte keyCode))
                {
                    keyCodes.Add(keyCode);
                    keybd_event(keyCode, 0, 0, UIntPtr.Zero); // Key Down
                }
                else
                {
                    api.Log(API.LogType.Error, $"SendKeys.dll: Unknown key '{key}'.");
                }
            }

            // Release keys in reverse order
            for (int i = keyCodes.Count - 1; i >= 0; i--)
            {
                keybd_event(keyCodes[i], 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key Up
            }
        }

        private void InitializeKeyMapping()
        {
            keyMapping = new Dictionary<string, byte>
    {
        // Modifier Keys
        { "Ctrl", 0x11 }, { "Shift", 0x10 }, { "Alt", 0x12 },
        { "LWin", 0x5B }, { "RWin", 0x5C },

        // Special Keys
        { "Tab", 0x09 }, { "Enter", 0x0D }, { "Escape", 0x1B },
        { "Space", 0x20 }, { "Backspace", 0x08 },
        { "Delete", 0x2E }, { "PrintScreen", 0x2C },
        { "NumLock", 0x90 }, { "CapsLock", 0x14 },
        { "Insert", 0x2D }, { "Home", 0x24 }, { "End", 0x23 },
        { "PageUp", 0x21 }, { "PageDown", 0x22 },

        // Arrow Keys
        { "Left", 0x25 }, { "Up", 0x26 }, { "Right", 0x27 }, { "Down", 0x28 },

        // Function Keys
        { "F1", 0x70 }, { "F2", 0x71 }, { "F3", 0x72 }, { "F4", 0x73 },
        { "F5", 0x74 }, { "F6", 0x75 }, { "F7", 0x76 }, { "F8", 0x77 },
        { "F9", 0x78 }, { "F10", 0x79 }, { "F11", 0x7A }, { "F12", 0x7B },

        // Letters and Numbers
        { "A", 0x41 }, { "B", 0x42 }, { "C", 0x43 }, { "D", 0x44 },
        { "E", 0x45 }, { "F", 0x46 }, { "G", 0x47 }, { "H", 0x48 },
        { "I", 0x49 }, { "J", 0x4A }, { "K", 0x4B }, { "L", 0x4C },
        { "M", 0x4D }, { "N", 0x4E }, { "O", 0x4F }, { "P", 0x50 },
        { "Q", 0x51 }, { "R", 0x52 }, { "S", 0x53 }, { "T", 0x54 },
        { "U", 0x55 }, { "V", 0x56 }, { "W", 0x57 }, { "X", 0x58 },
        { "Y", 0x59 }, { "Z", 0x5A },
        { "0", 0x30 }, { "1", 0x31 }, { "2", 0x32 }, { "3", 0x33 },
        { "4", 0x34 }, { "5", 0x35 }, { "6", 0x36 }, { "7", 0x37 },
        { "8", 0x38 }, { "9", 0x39 },

        // Numpad Keys
        { "Num0", 0x60 }, { "Num1", 0x61 }, { "Num2", 0x62 }, { "Num3", 0x63 },
        { "Num4", 0x64 }, { "Num5", 0x65 }, { "Num6", 0x66 }, { "Num7", 0x67 },
        { "Num8", 0x68 }, { "Num9", 0x69 }, { "NumMultiply", 0x6A },
        { "NumAdd", 0x6B }, { "NumSeparator", 0x6C }, { "NumSubtract", 0x6D },
        { "NumDecimal", 0x6E }, { "NumDivide", 0x6F },

        // Multimedia Keys
        { "VolumeMute", 0xAD }, { "VolumeDown", 0xAE }, { "VolumeUp", 0xAF },
        { "NextTrack", 0xB0 }, { "PreviousTrack", 0xB1 }, { "Stop", 0xB2 },
        { "PlayPause", 0xB3 },

        // OEM Keys (Symbols and Punctuation)
        { "Semicolon", 0xBA }, { "Equal", 0xBB }, { "Comma", 0xBC },
        { "Dash", 0xBD }, { "Period", 0xBE }, { "Slash", 0xBF },
        { "Backtick", 0xC0 }, { "LeftBracket", 0xDB },
        { "Backslash", 0xDC }, { "RightBracket", 0xDD }, { "Quote", 0xDE },

        // Browser Keys
        { "BrowserBack", 0xA6 }, { "BrowserForward", 0xA7 },
        { "BrowserRefresh", 0xA8 }, { "BrowserStop", 0xA9 },
        { "BrowserSearch", 0xAA }, { "BrowserFavorites", 0xAB },
        { "BrowserHome", 0xAC },

        // Miscellaneous Keys
        { "Pause", 0x13 }, { "ScrollLock", 0x91 }, { "Apps", 0x5D },
        { "Sleep", 0x5F }, { "Clear", 0x0C }
    };
    }
 }


        public static class Plugin
    {
        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new API(rm));
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            return 0.0;
        }

        [DllExport]
        public static void ExecuteBang(IntPtr data, [MarshalAs(UnmanagedType.LPWStr)] string args, IntPtr rm)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.ExecuteCommand(args, new API(rm));
        }
    }
}
