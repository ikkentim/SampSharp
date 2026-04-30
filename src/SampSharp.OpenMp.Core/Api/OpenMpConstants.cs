using System.Diagnostics.CodeAnalysis;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Contains constants provided by the open.mp SDK.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Mirrors open.mp SDK constant names")]
public static class OpenMpConstants
{
    /// <summary>
    /// The maximum number of seats in a vehicle.
    /// </summary>
    public const int MAX_SEATS = 4;

    /// <summary>
    /// The maximum number of players in the server.
    /// </summary>
    public const int PLAYER_POOL_SIZE = 1000;

    /// <summary>
    /// The maximum number of NPCs in the server.
    /// </summary>
    public const int MPC_POOL_SIZE = 1000;

    /// <summary>
    /// The maximum number of vehicles in the server.
    /// </summary>
    public const int VEHICLE_POOL_SIZE = 2000;

    /// <summary>
    /// The maximum number of classes in the server.
    /// </summary>
    public const int CLASS_POOL_SIZE = 320;

    /// <summary>
    /// The maximum number of objects in the server.
    /// </summary>
    public const int OBJECT_POOL_SIZE = 2000;

    /// <summary>
    /// The maximum number of objects clients with a version of 0.3.7 can see.
    /// </summary>
    public const int OBJECT_POOL_SIZE_037 = 1000;

    /// <summary>
    /// The number of weapon slots a player has.
    /// </summary>
    public const int MAX_WEAPON_SLOTS = 13;

    /// <summary>
    /// The maximum number of vehicle models.
    /// </summary>
    public const int MAX_VEHICLE_MODELS = 611 - 400 + 1;

    /// <summary>
    /// The highest weapon ID.
    /// </summary>
    public const int MAX_WEAPON_ID = 47;

    /// <summary>
    /// The number of skills a player has.
    /// </summary>
    public const int NUM_SKILL_LEVELS = 11;

    /// <summary>
    /// The slot ID which represents an invalid weapon slot.
    /// </summary>
    public const byte INVALID_WEAPON_SLOT = 0xFF;

    /// <summary>
    /// The minimum length of a player name.
    /// </summary>
    public const int MIN_PLAYER_NAME = 3;

    /// <summary>
    /// The maximum length of a player name.
    /// </summary>
    public const int MAX_PLAYER_NAME = 24;

    /// <summary>
    /// The highest animation ID.
    /// </summary>
    public const int MAX_ANIMATIONS = 1813;

    /// <summary>
    /// The maximum level a player can have in a skill.
    /// </summary>
    public const int MAX_SKILL_LEVEL = 999;

    /// <summary>
    /// The ID used to represent an invalid vehicle ID.
    /// </summary>
    public const int INVALID_VEHICLE_ID = 0xFFFF;

    /// <summary>
    /// The ID used to represent an invalid object ID.
    /// </summary>
    public const int INVALID_OBJECT_ID = 0xFFFF;

    /// <summary>
    /// The ID used to represent an invalid player ID.
    /// </summary>
    public const int INVALID_PLAYER_ID = 0xFFFF;

    /// <summary>
    /// The ID used to represent an invalid actor ID.
    /// </summary>
    public const int INVALID_ACTOR_ID = 0xFFFF;

    /// <summary>
    /// The distance at which objects are streamed in.
    /// </summary>
    public const float STREAM_DISTANCE = 200;

    /// <summary>
    /// The number of objects which can be attached to a player.
    /// </summary>
    public const int MAX_ATTACHED_OBJECT_SLOTS = 10;

    /// <summary>
    /// The number of materials an object can have.
    /// </summary>
    public const int MAX_OBJECT_MATERIAL_SLOTS = 16;

    /// <summary>
    /// The maximum number of text labels in the server
    /// </summary>
    public const int TEXT_LABEL_POOL_SIZE = 1024;

    /// <summary>
    /// The ID used to represent an invalid text label ID.
    /// </summary>
    public const int INVALID_TEXT_LABEL_ID = 0xFFFF;

    /// <summary>
    /// The maximum number of pickups in the server.
    /// </summary>
    public const int PICKUP_POOL_SIZE = 4096;

    /// <summary>
    /// The maximum number of global text draws in the server.
    /// </summary>
    public const int GLOBAL_TEXTDRAW_POOL_SIZE = 2048;

    /// <summary>
    /// The maximum number of player text draws in the server.
    /// </summary>
    public const int PLAYER_TEXTDRAW_POOL_SIZE = 256;

    /// <summary>
    /// The highest vehicle component ID.
    /// </summary>
    public const int MAX_VEHICLE_COMPONENTS = 194;

    /// <summary>
    /// The ID used to represent an invalid vehicle component ID.
    /// </summary>
    public const int INVALID_COMPONENT_ID = 0;

    /// <summary>
    /// The highest vehicle component slot.
    /// </summary>
    public const int MAX_VEHICLE_COMPONENT_SLOT = 16;

    /// <summary>
    /// The highest vehicle component slot in an RPC.
    /// </summary>
    public const int MAX_VEHICLE_COMPONENT_SLOT_IN_RPC = 14;

    /// <summary>
    /// The maximum number of text labels in the server.
    /// </summary>
    public const int MAX_TEXT_LABELS = 1024;

    /// <summary>
    /// The maximum number of global text draws which can be shown to a player.
    /// </summary>
    public const int MAX_GLOBAL_TEXTDRAWS = 2048;

    /// <summary>
    /// The maximum number of player text draws which can be shown to a player.
    /// </summary>
    public const int MAX_PLAYER_TEXTDRAWS = 256;

    /// <summary>
    /// The ID used to represent an invalid text draw ID.
    /// </summary>
    public const int INVALID_TEXTDRAW = 0xFFFF;

    /// <summary>
    /// The maximum number of actors in the server.
    /// </summary>
    public const int ACTOR_POOL_SIZE = 1000;

    /// <summary>
    /// The maximum number of menus in the server.
    /// </summary>
    public const int MENU_POOL_SIZE = 128;

    /// <summary>
    /// The maximum number of items in a menu.
    /// </summary>
    public const int MAX_MENU_ITEMS = 12;

    /// <summary>
    /// The maximum length of text in a menu.
    /// </summary>
    public const int MAX_MENU_TEXT_LENGTH = 32;

    /// <summary>
    /// The ID used to represent an invalid menu ID.
    /// </summary>
    public const int INVALID_MENU_ID = 0xFF;

    /// <summary>
    /// The ID used to represent an invalid dialog ID.
    /// </summary>
    public const int INVALID_DIALOG_ID = -1;

    /// <summary>
    /// The maximum ID a dialog can have.
    /// </summary>
    public const int MAX_DIALOG = 32768;

    /// <summary>
    /// The ID used to represent an invalid gang zone ID.
    /// </summary>
    public const int INVALID_GANG_ZONE_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid pickup ID.
    /// </summary>
    public const int INVALID_PICKUP_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid object model ID.
    /// </summary>
    public const int INVALID_OBJECT_MODEL_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid menu item ID.
    /// </summary>
    public const int INVALID_MENU_ITEM_ID = -1;

    /// <summary>
    /// The maximum number of gang zones in the server.
    /// </summary>
    public const int GANG_ZONE_POOL_SIZE = 1024;

    /// <summary>
    /// The maximum number of players which can be streamed in for a player.
    /// </summary>
    public const int MAX_STREAMED_PLAYERS = 200;

    /// <summary>
    /// The maximum number of actors which can be streamed in for a player.
    /// </summary>
    public const int MAX_STREAMED_ACTORS = 50;

    /// <summary>
    /// The maximum number of vehicles which can be streamed in for a player.
    /// </summary>
    public const int MAX_STREAMED_VEHICLES = 700;

    /// <summary>
    /// The team ID used to represent no team.
    /// </summary>
    public const int TEAM_NONE = 255;

    /// <summary>
    /// The seat ID used to represent no seat.
    /// </summary>
    public const int SEAT_NONE = -1;

    /// <summary>
    /// The default upper X/Y bounds of the world
    /// </summary>
    public const float MAX_WORLD_BOUNDS = 20000.0f;

    /// <summary>
    ///   The default lower X/Y bounds of the world.
    /// </summary>
    public const float MIN_WORLD_BOUNDS = -20000.0f;

    /// <summary>
    /// The maximum length of a text draw string.
    /// </summary>
    public const int MAX_TEXTDRAW_STR_LENGTH = 800;

    /// <summary>
    /// The maximum number of connected vehicle carriages.
    /// </summary>
    public const int MAX_VEHICLE_CARRIAGES = 3;

    /// <summary>
    /// The highest game text style ID.
    /// </summary>
    public const int MAX_GAMETEXT_STYLES = 16;

    /// <summary>
    /// The minimum ID for custom skins.
    /// </summary>
    public const int MIN_CUSTOM_SKIN_ID = 20001;

    /// <summary>
    /// The maximum ID for custom skins.
    /// </summary>
    public const int MAX_CUSTOM_SKIN_ID = 30000;

    /// <summary>
    /// The minimum ID for custom object models.
    /// </summary>
    public const int MIN_CUSTOM_OBJECT_ID = -30000;

    /// <summary>
    /// The maximum ID for custom object models.
    /// </summary>
    public const int MAX_CUSTOM_OBJECT_ID = -1000;

    /// <summary>
    /// The ID used to represent an invalid path ID.
    /// </summary>
    public const int INVALID_PATH_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid node ID.
    /// </summary>
    public const int INVALID_NODE_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid record ID.
    /// </summary>
    public const int INVALID_RECORD_ID = -1;

    /// <summary>
    /// The ID used to represent an invalid model ID.
    /// </summary>
    public const ushort INVALID_MODEL_ID = 65535;

    /// <summary>
    /// The ID of the question mark object model.
    /// </summary>
    public const int QUESTION_MARK_MODEL_ID = 18631;

    /// <summary>
    /// The speed at which NPCs will move when using the "auto" movement type.
    /// </summary>
    public const float NPC_MOVE_SPEED_AUTO = -1.0f;

    /// <summary>
    /// The speed at which NPCs will move when using the "walk" movement type.
    /// </summary>
    public const float NPC_MOVE_SPEED_WALK = 0.1552086f;

    /// <summary>
    /// The speed at which NPCs will move when using the "jog" movement type.
    /// </summary>
    public const float NPC_MOVE_SPEED_JOG = 0.56444f;

    /// <summary>
    /// The speed at which NPCs will move when using the "sprint" movement type.
    /// </summary>
    public const float NPC_MOVE_SPEED_SPRINT = 0.926784f;
}