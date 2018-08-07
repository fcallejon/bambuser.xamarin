using System;
namespace Bambuser.Xamarin.Broadcast
{

    /// <summary>
    /// Preset for automatic video quality
    /// </summary>
    public static class BambuserConstants
    {
        public const string SessionPresetAuto = "auto";

        /// <summary>
        /// Used to enable/disable the setting to adjust the option to save video locally
        /// </summary>
        public const string SaveLocallyOption = "saveLocally";

        /// <summary>
        /// Used to enable/disable the setting to enable the option to signal talkback capability to server
        /// </summary>
        public const string TalkbackOption = "talkback";

        /// <summary>
        /// Used to enable/disable the audio quality setting-selector
        /// </summary>
        public const string AudioQualityOption = "audioQuality";

        /// <summary>
        /// Used to enable/disable the 'save on server' settings-toggle
        /// </summary>
        public const string ArchiveOption = "archive";

        /// <summary>
        /// Used to enable/disable the location settings-toggle
        /// </summary>
        public const string PositionOption = "position";

        /// <summary>
        /// Used to enable/disable the 'private mode' settings-toggle
        /// </summary>
        public const string PrivateModeOption = "privateMode";
    }
}
