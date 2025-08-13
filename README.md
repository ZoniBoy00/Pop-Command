# Pop Command Plugin

**Plugin Name:** Pop Command  
**Author:** ZoniBoy00  
**Version:** 1.0.0  
**Description:** Displays server population in chat with cooldown.

---

## Features

- Displays server population using `/pop`
- Shows **online** and **sleeping** players
- Cooldown prevents spamming
- Configurable colors and message format
- Lightweight and optimized

---

## Commands

### `/pop`

Displays server population.

**Example output:**
```
Server Pop:
25/100 online
5 sleeping
```

**Cooldown message example:**
```
Wait 5.0s before using /pop again.
```

---

## Configuration

`PopCommand.json` (in `oxide/config/`) is auto-generated on first load.

**Default configuration:**

```json
{
  "CooldownMs": 10000,
  "TitleColor": "yellow",
  "OnlineColor": "green",
  "SleepingColor": "orange",
  "MessageFormat": "<color={TitleColor}>Server Pop:</color>\n<color={OnlineColor}>{online}/{maxPlayers}</color> online\n<color={SleepingColor}>{sleeping}</color> sleeping"
}
```

### Configuration Options

| Option | Description |
|--------|-------------|
| `CooldownMs` | Cooldown in milliseconds |
| `TitleColor` | Color of the title |
| `OnlineColor` | Color of online players count |
| `SleepingColor` | Color of sleeping players count |
| `MessageFormat` | Format of the message with placeholders |

### Available Placeholders

- `{TitleColor}`, `{OnlineColor}`, `{SleepingColor}` - Color placeholders
- `{online}`, `{maxPlayers}`, `{sleeping}` - Player count placeholders

---

## Installation

1. Place `PopCommand.cs` in `oxide/plugins/`
2. Start or reload server
3. Default config is auto-generated; customize `PopCommand.json` if needed

---

## Notes

- Only players can use `/pop`
- Cooldown prevents spam
- Shows only online and sleeping players
- Uses standard Rust chat color codes

---

## Support

For issues or feature requests, contact **ZoniBoy00**.
