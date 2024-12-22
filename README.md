
# SenKeys - Rainmeter Plugin  

**SenKeys** is a Rainmeter plugin that enables users to simulate keypresses directly from their Rainmeter skins. This plugin provides a flexible and powerful way to trigger keyboard inputs, automate workflows, and enhance interactivity in your Rainmeter setups.

---

## Features  

- **Simulate Keypresses:** Send single keys, key combinations, or sequences of keys using customizable configurations.  
- **Custom Delays:** Configure delays between simulated key sequences to fine-tune the timing of automated inputs.  
- **Wide Range of Supported Keys:** Support for standard keys, function keys, media keys, and more.  
- **Flexible Commands:** Trigger keypress simulations dynamically using Rainmeter bangs.  
- **Error Logging:** Debugging logs to ensure proper configuration and usage.  

---

## Installation  

1. Download the compiled `SenKeys.dll` plugin file.  
2. Place the `SenKeys.dll` file into the `Plugins` folder of your Rainmeter installation (typically located in `C:\Program Files\Rainmeter\Plugins`).  
3. Restart Rainmeter to load the plugin.  

---

## Usage  

### Plugin Configuration  

In your Rainmeter skin, define a measure using the `SenKeys` plugin and configure the following options:  

```ini
[MeasureSendKeys]
Measure=Plugin
Plugin=SenKeys
Keys=Ctrl+Alt+Delete   ; Define the keys to simulate
Delay=500              ; Delay (in milliseconds) between key groups
```

### Sending Keypresses  

Use the `!CommandMeasure` bang to trigger key simulations. Example:  

```ini
[SendKeyAction]
LeftMouseUpAction=[!CommandMeasure "MeasureSendKeys" "Send"]
```

---

## Parameters  

| Parameter | Description | Example |  
|-----------|-------------|---------|  
| **`Keys`** | Specifies the keys to simulate. Supports combinations (e.g., `Ctrl+Alt+Delete`) and sequences (e.g., `A B C`). | `Ctrl+Shift+T` |  
| **`Delay`** | Time delay (in milliseconds) between each key sequence. Defaults to `0`. | `500` |  

---

## Supported Keys  

Here is the full list of supported keys:  

### Modifiers  
- `Ctrl`, `Shift`, `Alt`  
- `LWin`, `RWin`  

### Function Keys  
- `F1` to `F12`  

### Special Keys  
- `Tab`, `Enter`, `Escape`, `Space`, `Backspace`, `Delete`, `Insert`  
- `Home`, `End`, `PageUp`, `PageDown`  

### Arrow Keys  
- `Left`, `Up`, `Right`, `Down`  

### Alphanumeric Keys  
- `A` to `Z`  
- `0` to `9`  

### Numeric Keypad  
- `Num0` to `Num9`  
- `NumMultiply`, `NumAdd`, `NumSeparator`, `NumSubtract`, `NumDecimal`, `NumDivide`  

### Media Keys  
- `VolumeMute`, `VolumeDown`, `VolumeUp`  
- `NextTrack`, `PreviousTrack`, `Stop`, `PlayPause`  

### Punctuation and Symbols  
- `Semicolon`, `Equal`, `Comma`, `Dash`, `Period`, `Slash`, `Backtick`  
- `LeftBracket`, `Backslash`, `RightBracket`, `Quote`  

### Browser Keys  
- `BrowserBack`, `BrowserForward`, `BrowserRefresh`, `BrowserStop`  
- `BrowserSearch`, `BrowserFavorites`, `BrowserHome`  

### Miscellaneous Keys  
- `PrintScreen`, `Pause`, `ScrollLock`, `Apps`, `Sleep`, `Clear`  

---

## Example  

Here is an example Rainmeter skin using SenKeys:  

```ini
[Rainmeter]
Update=1000
DynamicWindowSize=1

[MeasureSendKeys]
Measure=Plugin
Plugin=SenKeys
Keys=Ctrl+Shift+N
Delay=100

[SendKeyAction]
LeftMouseUpAction=[!CommandMeasure "MeasureSendKeys" "Send"]

[Button]
Meter=String
Text="Send Keys"
SolidColor=100,100,100,255
FontColor=255,255,255
Padding=10,10,10,10
LeftMouseUpAction=[!CommandMeasure "MeasureSendKeys" "Send"]
```

---

## Logging and Debugging  

- If no keys are defined, a log entry will be created:  
  *"SendKeys.dll: No Keys parameter provided."*  
- If an unknown key is specified, the plugin will log:  
  *"SendKeys.dll: Unknown key '<key>'."*  

Logs can be reviewed in the Rainmeter log window (`Ctrl + L`).  

---

## Known Issues  

- This plugin uses the `user32.dll` API, so it might require elevated permissions for certain keypress simulations.  
- Simulating certain reserved keys or combinations (like `Ctrl+Alt+Delete`) might not work depending on system restrictions.  

---

## Contribution  

Contributions are welcome! To contribute:  

1. Fork this repository.  
2. Create a feature branch.  
3. Submit a pull request with detailed notes about your changes.  

---

## License  

This project is licensed under the Apache License. See the `LICENSE` file for more details.  

---

## Credits  

- Developed by Nasir Shahbaz.  
- Powered by the Rainmeter platform.  

---

If you have any questions or need help, feel free to open an issue or reach out!  
