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

    /// <summary>Enumeration of possible errorcodes.</summary>
    public enum BambuserError
    {
        ///<summary></summary>
        BambuserErrorServerFull = -1,
        /// <summary>Server rejected client because of incorrect credentials</summary>
        BambuserErrorIncorrectCredentials = -2,
        /// <summary>Server disconnected</summary>
        BambuserErrorServerDisconnected = -3,
        /// <summary>No camera available</summary>
        BambuserErrorNoCamera = -4,
        /// <summary>Location disabled by user</summary>
        BambuserErrorLocationDisabled = -5,
        /// <summary>Connection to server lost</summary>
        BambuserErrorConnectionLost = -6,
        /// <summary>Connection could not be established</summary>
        BambuserErrorUnableToConnect = -7,
        /// <summary>Unable to start broadcasting because a broadcast is already ongoing.</summary>
        BambuserErrorAlreadyBroadcasting = -8,
        /// <summary>User privacy settings prohibit video or audio capture</summary>
        BambuserErrorPrivacy = -9,
        /// <summary>Not enough free space to continue local recording</summary>
        BambuserErrorNoFreeSpace = -10,
        /// <summary>Specified filename for local recording is not writable</summary>
        BambuserErrorWriteError = -11,
        /// <summary>Failed to retrieve ingest server or credentials.</summary>
        BambuserErrorBroadcastTicketFailed = -12,
        /// <summary>Encoder failed</summary>
        BambuserErrorEncoderFailed = -13,
        /// <summary>Server rejected client for another, unclassified reason - the accompanying error message should be shown to the user.</summary>
        BambuserErrorServerRejected = -14,
    };

    /// <summary>
    /// Possible audio presets to be set by calling setAudioQualityPreset:
    /// </summary>
    public enum AudioQuality
    {
        ///<summary>Audio off</summary>
        AudioOff = -1,
        /// <summary>Low quality audio, 11kHz mono AAC.</summary>
        AudioLow = 0,
        ///<summary>High quality audio, 22kHz mono AAC.</summary>
        AudioHigh = 1
    };

    /// <summary>
    /// The different states of talkback
    /// </summary>
    public enum TalkbackState
    {
        ///<summary>This is the default state. It signals that no request is pending and no talkback session is ongoing.</summary>
        TalkbackIdle = 0,
        ///<summary>At least one talkback request is pending, but has not yet been accepted.</summary>
        TalkbackNeedsAccept = 1,
        ///<summary>A talkback request has been accepted, but playback has not yet started.</summary>
        TalkbackAccepted = 2,
        ///<summary>A talkback request has been accepted and playback is ongoing.</summary>
        TalkbackPlaying = 3
    }

    ///<summary>Possible values of #BambuserPlayer.status.</summary>
	public enum BambuserPlayerState
    {
        ///<summary>Playback is stopped</summary>
        kBambuserPlayerStateStopped = 0,
        ///<summary>Playback of the stream has been requested but not yet started</summary>
        kBambuserPlayerStateLoading = 1,
        ///<summary>Playback is in progress</summary>
        kBambuserPlayerStatePlaying = 2,
        ///<summary>Playback is paused</summary>
        kBambuserPlayerStatePaused = 3
    };

    ///<summary>Possible values for #BambuserPlayer.requiredBroadcastState</summary>
	public enum BroadcastState
    {
        ///<summary>Any broadcast state</summary>
        kBambuserBroadcastStateAny = 0,
        ///<summary>Only live broadcasts</summary>
        kBambuserBroadcastStateLive = 1,
        ///<summary>Only archived broadcasts</summary>
        kBambuserBroadcastStateArchived = 2
    };

    ///<summary>Possible values for #BambuserPlayer.videoScaleMode </summary>
    public enum VideoScaleMode
    {
        ///<summary>Specifies that the player should preserve the video's aspect ratio and fit the video within the view's bounds.</summary>
        VideoScaleAspectFit = 0,
        ///<summary>Specifies that the player should preserve the video's aspect ratio and fill the view's bounds.</summary>
        VideoScaleAspectFill = 1,
        ///<summary>Specifies that the video should be stretched to fill the view's bounds.</summary>
        VideoScaleToFill = 2
    };
}