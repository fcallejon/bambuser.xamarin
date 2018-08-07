using System;

using UIKit;
using Foundation;
using CoreGraphics;
using ObjCRuntime;

namespace Bambuser.Xamarin.Broadcast
{
    /// <summary>
    /// This is the main class for using the broadcasting library.
    /// </summary>
    [BaseType(typeof(UIViewController))]
    public interface BambuserView
    {
        /// <summary>
        /// Used to initialise the bambuserView.
        /// 
        /// This method should be called with one of the SessionPreset video preset constants.
        /// 
        /// This method does not start capture immediately, but allows you to set other capture related parameters, before starting the capture.
        /// If initializing with this method, you must call <see cref="StartCapture"/> after setting all the capture related properties.
        /// </summary>
        /// <param name="preset">Preferred kSessionPreset-value</param>
        /// <returns>A <see cref="BambuserView"/></returns>
        [Export("initWithPreparePreset:")]
        IntPtr Constructor(string preset);

        /// <summary>
        /// This property should hold your application ID. Depending on serverside setup this ID can redirect you to a suitable
        /// ingest server, and automatically set your broadcasting credentials.
        /// 
        /// @property (nonatomic, retain, setter = setApplicationId:, getter = applicationId) NSString* applicationId;
        /// </summary>
        /// <remarks>This property should be set before calling <see cref="StartBroadcasting"/>.</remarks>
        [Export("applicationId", ArgumentSemantic.Retain)]
        string ApplicationId { get; [Bind("setApplicationId:")]set; }

        /// <summary>
        /// This property presents a scrollable view for displaying chat messages.
        /// 
        /// See documentation for UIView for styling the view with background color, alpha and frame.
        /// 
        /// @property (nonatomic, retain) UIView *chatView;
        /// </summary>
        [Export("chatView", ArgumentSemantic.Retain)]
        UIView ChatView { get; set; }

        /// <summary>
        /// This property contains a view with an user interface for configuring settings.
        /// 
        /// The settings shown in the view can be enabled with <see cref="BambuserView.EnableOption" />(<see cref="BambuserConstants" />).*Option.
        /// 
        /// @property (nonatomic, retain) UIViewController *settingsView;
        /// </summary>
        /// <value>By default, no options are enabled.</value>
        [Export("settingsView", ArgumentSemantic.Retain)]
        UIViewController SettingsView { get; set; }

        /// <summary>
        /// This property is True if the startBroadcasting method can be called.
        /// 
        /// //@property (nonatomic, readonly, getter = canStart) BOOL canStart;
        /// </summary>
        /// <remarks>If the library is connecting or already is connected this method will return NO.</remarks>
        [Export("canStart")]
        bool CanStart { get; }

        /// <summary>
        /// This property sets the audio quality preset for the upcoming bambuser broadcast.
        /// 
        /// Constants are defined in <see cref="AudioQuality"/> for the possible values of this property.
        /// 
        /// @property (nonatomic, setter = setAudioQualityPreset:, getter = audioQualityPreset) enum AudioQuality audioQualityPreset;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("audioQualityPreset")]
        AudioQuality AudioQualityPreset { get; [Bind("setAudioQualityPreset:")]set; }

        /// <summary>
        /// This property reflects the 'save on server'-option in the settings view, and can also be set and read independently of the settings view.
        /// 
        /// When this property is True, the next broadcast will signal the Bambuser video server that the broadcast
        /// should be available on demand when the live broadcast is over.
        /// @property (nonatomic, setter = setSaveOnServer:, getter = saveOnServer) BOOL saveOnServer;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("saveOnServer")]
        bool SaveOnServer { get; [Bind("setSaveOnServer:")]set; }

        /// <summary>
        /// This property reflects the 'save locally'-option in the settings view, and can also be set and read independently of the settings view.
        /// 
        /// When this property is True, the next broadcast will also be saved to a local file. If a path is set in <see cref="localFilename"/>, the file gets written in that path,
        /// otherwise the file gets a unique filename assigned and is stored within the sandbox's NSTemporaryDirectory().
        /// 
        /// <see cref="BambuserView.LocalFilename"/>.
        /// <see cref="BambuserViewDelegate.RecordingComplete"/>.
        /// @property (nonatomic, setter = setSaveLocally:, getter = saveLocally) BOOL saveLocally;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("saveLocally")]
        bool SaveLocally { get; [Bind("setSaveLocally:")]set; }

        /// <summary>
        /// This property reflects the 'talkback'-option in the settings view, and can also be set and read independently of the settings view.
        /// 
        /// When this property is True, the next broadcast will signal the Bambuser video server that the client is willing to accept requests for talkback.
        /// @property (nonatomic, setter = setTalkback:, getter = talkback) BOOL talkback;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("talkback")]
        bool Talkback { get; [Bind("setTalkback:")]set; }

        /// <summary>
        /// This property indicates whether the talkback audio should be mixed into the signal that gets recorded into the local copy and broadcasted.
        /// 
        /// @property (nonatomic, setter = setTalkbackMix:, getter = talkbackMix) BOOL talkbackMix;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("talkbackMix")]
        bool TalkbackMix { get; [Bind("setTalkbackMix:")]set; }

        /// <summary>
        /// This property can be set if you want to set a specific file path when saving a local copy of your broadcast.
        /// 
        /// 
        /// If this value is nil the local copy will be saved with a unique filename in the sandbox's NSTemporaryDirectory().
        /// 
        /// <see cref="BambuserView.SaveLocally"/>.
        /// <see cref="BambuserViewDelegate.RecordingComplete"/>.
        /// @property (nonatomic, retain, setter = setLocalFilename:, getter = localFilename) NSString *localFilename;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("localFilename", ArgumentSemantic.Retain)]
        string LocalFilename { get; [Bind("setLocalFilename:")]set; }

        /// <summary>
        /// This property reflects the 'send position'-option in the settings view, and can also be set and read independently of the settings view.
        /// 
        /// When this property is True, the location of the device will be sent continuously during the next broadcast.
        /// 
        /// @property (nonatomic, setter = setSendPosition:, getter = sendPosition) BOOL sendPosition;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("sendPosition")]
        bool SendPosition { get; [Bind("setSendPosition:")]set; }

        /// <summary>
        /// This property reflects the 'private mode'-option in the settings view, and can also be set and read independently of the settings view.
        /// 
        /// When this property is True, the next broadcast will signal the Bambuser video server that the broadcast should be listed as private.
        /// 
        /// @property (nonatomic, setter = setPrivateMode:, getter = privateMode) BOOL privateMode;
        /// </summary>
        /// <remarks>This property can not be altered during an ongoing broadcast.</remarks>
        [Export("privateMode")]
        bool PrivateMode { get; [Bind("setPrivateMode:")]set; }

        /// <summary>
        /// Contains the author field which will be associated with the broadcast.
        /// 
        /// This can be any arbitrary string that should be associated with the broadcast.
        /// @property (nonatomic, retain, setter = setAuthor:, getter = author) NSString *author;
        /// </summary>
        /// <remarks>This property should be set before calling <see cref="BambuserView.StartBroadcasting"/>.</remarks>
        [Export("author", ArgumentSemantic.Retain)]
        string Author { get; [Bind("setAuthor:")]set; }

        /// <summary>
        /// Sets the title for an upcoming broadcast, or updates the title for an ongoing broadcast.
        /// This property can be altered at any time.
        /// 
        /// @property (nonatomic, retain, setter = setBroadcastTitle:, getter = broadcastTitle) NSString *broadcastTitle;
        /// </summary>
        [Export("broadcastTitle", ArgumentSemantic.Retain)]
        string BroadcastTitle { get; [Bind("setBroadcastTitle:")]set; }

        /// <summary>
        /// Can be set to an arbitrary string that will be associated with the broadcast.
        /// 
        /// This property can be set before broadcasting and updated during the broadcast.
        /// @property (nonatomic, retain, setter = setCustomData:, getter = customData) NSString *customData;
        /// </summary>
        /// <remarks>This field is currently limited to 10000 bytes serverside..</remarks>
        [Export("customData", ArgumentSemantic.Retain)]
        string CustomData { get; [Bind("setCustomData:")]set; }

        /// <summary>
        /// This property determines the orientation the video will be recorded in.
        /// 
        /// This property can be set to <see cref="UIInterfaceOrientation" />.
        /// 
        /// @property (nonatomic, setter = setOrientation:, getter = orientation) UIInterfaceOrientation orientation;
        /// </summary>
        /// <remarks>During a broadcast, it can only be set to flipped versions of the same orientation (i.e. switching from
        /// <see cref="UIInterfaceOrientation.LandscapeRight" /> to <see cref="UIInterfaceOrientation.LandscapeLeft"/>).</remarks>
        /// <value>The default value is <see cref="UIInterfaceOrientation.LandscapeRight"/>.</value>
        [Export("orientation")]
        UIInterfaceOrientation Orientation { get; [Bind("setOrientation:")]set; }

        /// <summary>
        /// This property determines the orientation of the application UI, making sure that the camera picture on the screen is rotated correctly.
        /// 
        /// This property is read-only - setting the orientation property sets this to the same value.
        /// 
        /// See <see cref="SetOrientation"/> in order to change it.
        /// @property (nonatomic, readonly, getter = previewOrientation) UIInterfaceOrientation previewOrientation;
        /// </summary>
        /// <returns></returns>
        [Export("previewOrientation")]
        UIInterfaceOrientation PreviewOrientation { get; }

        /// <summary>
        /// This property turns the LED torch on and off, if the device has one. This property can be altered at any time.        /// 
        /// 
        /// @property (nonatomic, setter = setTorch:, getter = torch) BOOL torch;
        /// </summary>
        [Export("torch")]
        bool Torch { get; [Bind("setTorch:")]set; }

        /// <summary>
        /// This readonly property will be TRUE if the device has an LED torch.
        /// 
        /// 
        /// @property (nonatomic, readonly, getter = hasLedTorch) BOOL hasLedTorch;
        /// </summary>
        [Export("hasLedTorch")]
        bool HasLedTorch { get; }

        /// <summary>
        /// This readonly property will be True if the device has a front-facing camera.
        /// 
        /// @property (nonatomic, readonly, getter = hasFrontCamera) BOOL hasFrontCamera;
        /// </summary>
        [Export("hasFrontCamera")]
        bool HasFrontCamera { get; }

        /// <summary>
        /// This readonly property returns a value between 0 and 100, indicating the current stream health.
        /// 
        /// A value of 0 indicates that the connection is unable to keep up with transferring all data currently being generated.
        /// A value of 100 indicates that the connection is able to send all the data currently being generated.
        /// 
        /// 
        /// @property (nonatomic, readonly, getter = health) int health;
        /// </summary>
        /// <remarks>This method will return 0 when not broadcasting.</remarks>
        [Export("health")]
        int Health { get; }

        /// <summary>
        /// This readonly property contains the current state of talkback.
        /// 
        /// <see cref="TalkbackState" /> for possible values.
        /// @property (nonatomic, readonly, getter = talkbackState) enum TalkbackState talkbackState;
        /// </summary>
        [Export("talkbackState")]
        TalkbackState TalkbackState { get; }

        /// <summary>
        /// This property controls the current zoom level. Accepted values are in the range [1.0, <see cref="MaxZoom"/>].
        /// 
        /// @property (nonatomic, setter = setZoom:, getter = zoom) float zoom;
        /// </summary>
        [Export("zoom")]
        float Zoom { get; [Bind("setZoom:")]set; }


        /// <summary>
        /// This readonly property returns the highest value accepted for the zoom property.
        /// 
        /// 
        /// @property (nonatomic, readonly, getter = maxZoom) float maxZoom;
        /// </summary>
        /// <remarks>A negative value indicates that the device does not support zooming.</remarks>
        [Export("maxZoom")]
        float MaxZoom { get; }

        /// <summary>
        /// This property allows setting the maximum capture framerate, within a range from 24 to 30 fps.
        /// 
        /// @property (nonatomic, setter = setFramerate:, getter = framerate) float framerate;
        /// </summary>
        /// <value>Default framerate is 30.</value>
        [Export("framerate")]
        float Framerate { get; [Bind("setFramerate:")]set; }

        /// <summary>
        ///  property indicates the minimum framerate.
        /// 
        /// @property (nonatomic, readonly, getter = minFramerate) float minFramerate;
        /// </summary>
        /// <remarks>This property is read-only, it can be set via <see cref="SetFramerate" />.
        [Export("minFramerate")]
        float MinFramerate { get; }

        /// <summary>
        /// This property returns the current speed in bytes per second of an ongoing or previously
        /// performed uplink test.
        /// 
        /// 
        /// @property (nonatomic, readonly, getter = uplinkSpeed) float uplinkSpeed;
        /// </summary>
        /// <value>If an uplink test has not yet been started, 0 will be returned.</value>
        [Export("uplinkSpeed")]
        float UplinkSpeed { get; }

        /// <summary>
        /// This property returns the current recommendation whether to broadcast or not, during an
        /// ongoing or previously performed uplink test.
        /// 
        /// 
        /// @property (nonatomic, readonly, getter = uplinkRecommendation) BOOL uplinkRecommendation;
        /// </summary>
        /// <value>If an uplink test has not yet been started, FALSE will be returned.</value>
        [Export("uplinkRecommendation")]
        bool UplinkRecommendation { get; }

        /// <summary>
        /// Sets the max size for any dimension when using the auto quality preset.
        /// 
        /// For instance, if <see cref="MaxBroadcastDimension" /> is set to 640, the broadcast is limited to 640x360
        /// in landscape mode, and to 360x640 in portrait mode at the default aspect ratio.
        /// 
        /// Set to 0 for no limiting of maximum broadcast resolution.
        /// 
        /// 
        /// @property (nonatomic, setter=setMaxBroadcastDimension:, getter=maxBroadcastDimension) int maxBroadcastDimension;
        /// </summary>
        /// <remarks>This property can not be altered during a broadcast.</remarks>
        [Export("maxBroadcastDimension")]
        float MaxBroadcastDimension { get; [Bind("setMaxBroadcastDimension:")]set; }

        /// <summary>
        /// Property to set a custom rectangle with the desired location and dimensions for the preview view.
        /// 
        /// @property (nonatomic, setter=setPreviewFrame:, getter=previewFrame) CGRect previewFrame;
        /// </summary>
        [Export("previewFrame")]
        CGRect PreviewFrame { get; [Bind("setPreviewFrame:")]set; }

        /// <summary>
        /// Property for setting delegate to receive BambuserViewDelegate callbacks
        /// 
        /// @property (weak) id delegate;
        /// </summary>
        [Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        /// <summary>
        /// Connects to the Bambuser video server and starts a new broadcast.
        /// 
        /// - (void) startBroadcasting;
        /// </summary>
        /// <remarks>
        /// This method should not be called if the library is trying to connect or is already connected.
        /// It is advisable to check the canStart property before calling this method.
        /// </remarks>
        [Export("startBroadcasting")]
        void StartBroadcasting();

        /// <summary>
        /// Stops the broadcast and disconnects from the Bambuser video server.
        /// 
        /// - (void) stopBroadcasting;
        /// </summary>
        /// <remarks> This method can be called at any time.</remarks>
        [Export("stopBroadcasting")]
        void StopBroadcasting();

        /// <summary>
        /// Start capturing with the properties set.
        /// 
        /// This method should be called after all properties have been set.
        /// This must be called before calling <see cref="StartBroadcasting"/>.
        /// - (void) startCapture;
        /// </summary>
        /// <remarks>This method should only be called once.</remarks>
        [Export("startCapture")]
        void StartCapture();

        /// <summary>
        /// Sets the preview orientation independently from the capture orientation.
        /// 
        /// The <pararef name="previewOrientation" /> parameter should be the orientation mode used for the application UI where the preview is shown.
        /// This allows e.g. keeping the UI locked in one orientation while capturing in an orientation depending on how the device is held.
        /// 
        /// - (void) setOrientation: (UIInterfaceOrientation) orientation previewOrientation: (UIInterfaceOrientation) previewOrientation;
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="previewOrientation">should be the orientation mode used for the application UI where the preview is shown.</param>
        [Export("setOrientation:previewOrientation:")]
        void SetOrientation(UIInterfaceOrientation orientation, UIInterfaceOrientation previewOrientation);

        /// <summary>
        /// Sets the preview orientation independently from the capture orientation.
        /// 
        /// The <pararef name="previewOrientation" /> parameter should be the orientation mode used for the application UI where the preview is shown.
        /// This allows e.g. keeping the UI locked in one orientation while capturing in an orientation depending on how the device is held.
        /// 
        /// This method additionally takes integers for width and height, to set aspect ratio for the captured video. Note that the captured
        /// video is cropped to fit this ratio, so edges of your preview might not be in the broadcast video. Vice versa, parts not visible
        /// in the preview might appear in the broadcast video. To avoid this, make sure to set the preview frame to the same aspect ratio using
        /// the <see cref="PreviewFrame"/> property.
        /// 
        /// - (void) setOrientation: (UIInterfaceOrientation) orientation previewOrientation: (UIInterfaceOrientation) previewOrientation withAspect: (int) w by: (int) h;
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="previewOrientation">should be the orientation mode used for the application UI where the preview is shown.</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        [Export("setOrientation:previewOrientation:withAspect:by:")]
        void SetOrientation(UIInterfaceOrientation orientation, UIInterfaceOrientation previewOrientation, int w, int h);

        /// <summary>
        /// This method allows setting the minimum framerate of the capture, in addition to the maximum framerate.
        /// 
        /// The minimum framerate only affects the capture from the camera itself; the actual streamed framerate may be lower.
        /// 
        /// This restricts the camera, not allowing it to lower the framerate even if it would be needed to get better exposure (e.g. in low light conditions).
        /// Default framerate is 30 and minFramerate is 15.
        /// 
        /// - (void) setFramerate:(float)framerate minFramerate:(float)minFramerate;
        /// </summary>
        /// <param name="framerate"></param>
        /// <param name="minFramerate"></param>
        [Export("setFramerate:minFramerate:")]
        void SetFramerate(float framerate, float minFramerate);

        /// <summary>
        /// For devices with more than one camera this method can be used to toggle between these cameras. This method can be called at any time.
        /// 
        /// - (void) swapCamera;
        /// </summary>
        [Export("swapCamera")]
        void SwapCamera();

        /// <summary>
        /// Request a snapshot be returned from the camera.
        /// 
        /// The resulting UIImage will be returned through the <see cref="BambuserViewDelegate.SnapshotTaken"/> method.
        /// 
        /// - (void) takeSnapshot;
        /// </summary>
        [Export("takeSnapshot")]
        void TakeSnapshot();

        /// <summary>
        /// This method is used to accept a pending talkback request.
        /// 
        /// The talkbackID should previously have been supplied by the delegate protocol method <see cref="BambuserViewDelegate.TalkbackRequest" />.
        /// 
        /// - (void) acceptTalkbackRequest: (int) talkbackID;
        /// </summary>
        /// <param name="talkbackID">The talkbackID supplied in a previous talkback request during ongoing broadcast.</param>
        [Export("acceptTalkbackRequest:")]
        void AcceptTalkbackRequest(int talkbackID);

        /// <summary>
        /// This method is used to reject a pending talkback request.
        /// 
        /// The talkbackID should previously have been supplied by the delegate protocol method <see cref="BambuserViewDelegate.TalkbackRequest" />.
        /// 
        /// - (void) declineTalkbackRequest: (int) talkbackID;
        /// </summary>
        /// <param name="talkbackID">The talkbackID supplied in a previous talkback request during ongoing broadcast.</param>
        [Export("declineTalkbackRequest:")]
        void DeclineTalkbackRequest(int talkbackID);

        /// <summary>
        /// This method is used to end an ongoing talkback session.
        /// 
        /// - (void) endTalkback;
        /// </summary>
        [Export("endTalkback")]
        void EndTalkback();

        /// <summary>
        /// This method enables or disables the visibility of options in the settings view. This method can be called at any time.
        /// 
        /// - (void) enableOption: (NSString*) optionName enabled: (BOOL) enabled;
        /// </summary>
        /// <param name="optionName">The name of the property being enabled/disabled. The name of all properties are defined in <see cref="BambuserConstants"/>.</param>
        /// <param name="enabled">Set whether or not to display the option</param>
        /// <remarks> This method can be called at any time.</remarks>
        [Export("enableOption:enabled:")]
        void EnableOption(string optionName, bool enabled);

        /// <summary>
        /// This method is used to manually start a linktest.
        /// 
        /// A valid applicationId must be set before linktests can be performed.
        /// 
        /// The result will be returned through the <see cref="BambuserViewDelegate.UplinkTestComplete" /> method.
        /// - (void) startLinktest;
        /// </summary>
        [Export("startLinktest")]
        void StartLinktest();

        /// <summary>
        /// Display the supplied string message in <see cref="ChatView"/>.
        /// 
        /// - (void) displayMessage: (NSString*) message;
        /// </summary>
        /// <param name="message">A string containing the message to be displayed in <see cref="ChatView"/>.</param>
        [Export("displayMessage:")]
        void DisplayMessage(string message);

        /// <summary>
        /// Sets a video quality preset for the upcoming Bambuser session. This method should not be called during a broadcast.
        /// 
        /// The only valid value for this parameter is <see cref="BambuserConstants.SessionPresetAuto"/>.
        /// The preset <see cref="BambuserConstants.SessionPresetAuto"/> will allow the video quality to dynamically be adjusted depending on the connection quality.
        /// The default value is <see cref="BambuserConstants.SessionPresetAuto"/>.
        /// 
        /// - (BOOL) setVideoQualityPreset: (NSString*) preset;
        /// </summary>
        /// <param name="preset">The only valid value for this parameter is <see cref="BambuserConstants.SessionPresetAuto"/></param>
        /// <returns>true if the session could apply the preset, otherwise False.</returns>
        [Export("setVideoQualityPreset:")]
        bool SetVideoQualityPreset(string preset);
    }

    /// <summary>
    /// Protocol for a delegate to handle updates and exceptions when using BambuserView.
    /// 
    /// The delegate of a BambuserView must adopt the BambuserViewDelegate
    /// protocol. Optional methods of the protocol allow the delegate to receive
    /// signals about the state of broadcasts.
    /// </summary>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    public partial interface BambuserViewDelegate
    {
        /// <summary>
        /// This method will be called when the BambuserView has successfully connected to a Bambuser video server and a broadcast has been started.
        /// -(void) broadcastStarted;
        /// </summary>
        [Export("broadcastStarted")]
        [Abstract]
        void BroadcastStarted();

        /// <summary>
        /// This method will be called when the BambuserView has been disconnected from a Bambuser video server and broadcasting has stopped.
        /// -(void) broadcastStopped;
        /// </summary>
        [Export("broadcastStopped")]
        [Abstract]
        void BroadcastStopped();

        /// <summary>
        /// This method will be called to relay any error to the delegate.
        /// -(void) bambuserError: (enum BambuserError)errorCode message:(NSString*) errorMessage
        /// </summary>
        /// <param name="errorCode">Defined in <see cref="BambuserError"/></param>
        /// <param name="errorMessage">User readable error message, where available.</param>
        [Export("bambuserError:message:")]
        [Abstract]
        void BambuserError(BambuserError errorCode, string errorMessage);

        /// <summary>
        /// This method is called when an uplink test has been completed. Uplink speed will be tested
        /// automatically when applicationId has been set and a valid broadcast ticket has been retrieved.
        /// 
        /// 
        /// The supplied speed is bytes per second and the shouldBroadcast is TRUE if attempting to broadcast
        /// is advisable.
        /// 
        /// 
        /// BambuserView will allow broadcasting regardless of the speed test results. The speed test results
        /// are only offered as guidance.
        /// 
        /// 
        /// -(void) uplinkTestComplete: (float) speed recommendation: (BOOL) shouldBroadcast;
        /// </summary>
        /// <param name="speedRecomendation"></param>
        /// <param name="shouldBroadcast"></param>
        [Export("uplinkTestComplete:recommendation:")]
        [Abstract]
        void UplinkTestComplete(float speedRecomendation, bool shouldBroadcast);

        /// <summary>
        /// This method will be called when the back-button has been pressed in the settings view.
        /// 
        /// It is the delegates responsibility to take appropriate action to dismiss the settings view.
        /// -(void) hideSettingsView;
        /// </summary>
        [Export("hideSettingsView")]
        [Abstract]
        void HideSettingsView();

        /// <summary>
        /// This method is called every time a chat message is received from the server. 
        /// -(void) chatMessageReceived: (NSString*) message;
        /// </summary>
        /// <param name="message">Contains the message.</param>
        [Export("hideSettingsView:")]
        [Abstract]
        void ChatMessageReceived(string message);

        /// <summary>
        /// * This method will be called when broadcasting has been stopped and a local copy has been saved.
        /// By default the file is located in the sandbox's NSTemporaryDirectory() and may be removed by the system when your
        /// application is not running. It is your responsibility to copy, move, remove or export this file to camera roll.
        /// -(void) recordingComplete: (NSString*) filename;
        /// 
        /// <see cref="BambuserView.SaveLocally" />
        /// <see cref="BambuserView.LocalFilename" />
        /// </summary>
        /// /// <param name="filename">Contains the filename of the recorded file</param>
        [Export("recordingComplete:")]
        [Abstract]
        void RecordingComplete(string filename);

        /// <summary>
        /// During an ongoing broadcast, this method is called whenever the BambuserView's health property is updated.
        /// -(void) healthUpdated: (int) health;
        /// </summary>
        /// <param name="health">Contains the stream health value in percents</param>
        [Export("healthUpdated:")]
        [Abstract]
        void HealthUpdated(int health);

        /// <summary>
        /// Called when the number of current viewers is updated.
        /// -(void) currentViewerCountUpdated: (int) viewers;
        /// </summary>
        /// <param name="viewers">Number of current viewers of the broadcast. This is generally the most interesting number during a live broadcast.</param>
        [Export("currentViewerCountUpdated:")]
        [Abstract]
        void CurrentViewerCountUpdated(int viewers);

        /// <summary>
        /// Called when the total number of viewers is updated.
        /// 
        /// -(void) totalViewerCountUpdated: (int) viewers;
        /// </summary>
        /// <param name="viewers">Total number of viewers of the broadcast. This accumulates over time.</param>
        /// <remarks>The counted viewers are not guaranteed to be unique, but there are measures in place to exclude obvious duplicates, eg. replays from a viewer.</remarks>
        [Export("totalViewerCountUpdated:")]
        [Abstract]
        void TotalViewerCountUpdated(int viewers);

        /// <summary>
        /// This method is called when an incoming talkback request is received.
        /// 
        /// The strings caller and request are set by the caller. The integer talkbackID is unique for this broadcast and request,
        /// and is used when accepting a talkback request.
        /// 
        /// -(void) talkbackRequest: (NSString*) request caller: (NSString*) caller talkbackID: (int) talkbackID;
        /// </summary>
        /// <param name="request">A free form string set by the caller</param>
        /// <param name="caller">A string containing the callers name</param>
        /// <param name="talkbackID">A unique number associated with this talkback request</param>
        [Export("talkbackRequest:request:caller:")]
        [Abstract]
        void TalkbackRequest(string request, string caller, int talkbackID);
        /**
         * \anchor talkbackStateChanged
         * 
         *
         * @param state 
         */
        // -(void) talkbackStateChanged: (enum TalkbackState) state;
        /// <summary>
        /// This method is called when talkback status changes.
        /// </summary>
        /// <param name="state">Defined in <see cref="TalkbackState" />.</param>
        [Export("talkbackStateChanged:")]
        [Abstract]
        void TalkbackStateChanged(TalkbackState state);

        /// <summary>
        /// This method is called when the server has returned the unique id given to the broadcast.
        /// 
        /// -(void) broadcastIdReceived: (NSString*) broadcastId;
        /// </summary>
        /// <param name="broadcastId">The Broadcast ID.</param>
        [Export("broadcastIdReceived:")]
        [Abstract]
        void BroadcastIdReceived(string broadcastId);

        /// <summary>
        /// When calling the takeSnapshot method, this method will return the result if successful.
        /// 
        /// The actual dimensions of the snapshot are limited by the active camera resolution, and can vary depending on device.
        /// 
        /// If cropping has been requested using the <code>BambuserView.SetOrientation:previewOrientation:withAspect:by: (!!NOT IMPLEMENTED!!)</code>
        /// method, the snapshot will be cropped to the desired aspect ratio.
        /// 
        /// -(void) :(UIImage*)image;
        /// </summary>
        /// <param name="image">The image.</param>
        [Export("snapshotTaken:")]
        [Abstract]
        void SnapshotTaken(UIImage image);
    }

    /// <summary>
    /// This is the main class for using the player library.
    /// </summary>
    [BaseType(typeof(UIView))]
    public interface BambuserPlayer
    {
        /// <summary>
        /// Set this to the object that conforms to the BambuserPlayerDelegate protocol, to receive updates about playback.
        /// 
        /// @property (nonatomic, weak) id <BambuserPlayerDelegate> delegate;
        /// </summary>
        [Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        /// <summary>
        /// Contains the resourceUri for the currently loaded broadcast.
        /// 
        /// @property (nonatomic) NSString *resourceUri;
        /// </summary>
        [Export("resourceUri")]
        string ResourceUri { get; set; }

        /// <summary>
        /// Contains the Bambuser Application ID necessary to make authorized requests.
        /// 
        /// @property(nonatomic, retain, setter = setApplicationId:, getter = applicationId) NSString* applicationId;
        /// </summary>
        [Export("applicationId", ArgumentSemantic.Retain)]
        string ApplicationId { get; [Bind("setApplicationId:")]set; }

        /// <summary>
        /// Setting this requires the broadcast to be in a specific state for playback.
        /// 
        /// @property(nonatomic) enum BroadcastState requiredBroadcastState;
        /// </summary>
        [Export("requiredBroadcastState")]
        BroadcastState RequiredBroadcastState { get; set; }

        /// <summary>
        /// This property reflects the current state of playback.
        /// 
        /// @property(nonatomic, readonly) enum BambuserPlayerState status;
        /// </summary>
        [Export("status")]
        BambuserPlayerState Status { get; }

        /// <summary>
        /// This property reflects the current playback position.
        /// 
        /// @property(nonatomic, readonly, getter = playbackPosition) double playbackPosition;
        /// </summary>
        [Export("playbackPosition")]
        double PlaybackPosition { get; set; }

        /// <summary>
        /// This boolean property indicates whether the broadcast loaded for playback is currently live or not.
        /// Live broadcasts cannot be paused nor seeked in.
        /// 
        /// @property(nonatomic, readonly) BOOL live;
        /// </summary>
        [Export("live")]
        bool Live { get; }

        /// <summary>
        /// This boolean property indicates whether or not system controls should be displayed when doing playback of archived broadcasts.
        /// </summary>
        [Export("VODControlsEnabled")]
        bool VODControlsEnabled { get; [Bind("setVODControlsEnabled:")]set; }

        /// <summary>
        ///  An enum that specifies how the video is displayed within the bounds of the BambuserPlayer's view.
        ///  The default value is #VideoScaleAspectFit.
        /// 
        /// @property(nonatomic) enum VideoScaleMode videoScaleMode;
        /// </summary>
        [Export("videoScaleMode")]
        VideoScaleMode VideoScaleMode { get; set; }

        /// <summary>
        /// This boolean property is used to request that seeking is enabled during a live broadcast.
        /// 
        /// The value default is NO, as the timeshift mode has trade-offs: it adds additional latency, and
        /// is mainly suited for broadcasts with reasonable duration. This should only be enabled if
        /// seeking in live content is actually required.
        /// 
        /// @property(nonatomic, setter = setTimeShiftModeEnabled:) BOOL timeShiftModeEnabled;
        /// </summary>
        /// <remarks>This property can only be changed before calling playVideo:</remarks>
        [Export("timeShiftModeEnabled")]
        double TimeShiftModeEnabled { get; [Bind("setTimeShiftModeEnabled:")]set; }

        /// <summary>
        /// This property holds the earliest possible position to seek to in timeshift mode.
        /// 
        /// @property(nonatomic, readonly) double seekableStart;
        /// </summary>
        /// <value>This property will return a negative value if not available.</value>
        [Export("seekableStart")]
        double SeekableStart { get; set; }

        /// <summary>
        /// This property holds the latest possible position to seek to in timeshift mode.
        /// 
        /// @property(nonatomic, readonly) double seekableEnd;
        /// </summary>
        /// <value>This property will return a negative value if not available.</value>
        [Export("seekableEnd")]
        double SeekableEnd { get; set; }

        /// <summary>
        /// This float adjusts the volume of playback. Set in the range 0.0 to 1.0.
        /// 
        /// @property(nonatomic, setter = setVolume:) float volume;
        /// </summary>
        [Export("volume")]
        float Volume { get; [Bind("setVolume:")]set; }

        /// <summary>
        /// Request the BambuserPlayer to start playing a broadcast with the supplied resource uri,
        /// which is a signed URI received from the Bambuser Metadata API.
        /// 
        /// - (void) playVideo: (NSString*) resourceUri;
        /// </summary>
        /// <param name="resourceUri">The resource uri associated with the broadcast to be loaded.</param>
        [Export("playVideo:")]
        void PlayVideo(string resourceUri);

        /// <summary>
        /// Stops playback of broadcast.
        /// 
        /// - (void) stopVideo;
        /// </summary>
        [Export("stopVideo")]
        void StopVideo();

        /// <summary>
        /// Pauses playback of broadcast.
        /// 
        /// - (void) pauseVideo;
        /// </summary>
        /// <remarks>For live broadcasts, this will behave in the same manner as stopVideo.</remarks>
        [Export("pauseVideo")]
        void PauseVideo();

        /// <summary>
        /// Resume playback of a video that has been paused. This only works
        /// for non-live videos; for live videos, playback must be requested
        /// via the <see cref="PlayVideo" />
        /// 
        /// - (void) playVideo;
        /// </summary>
        [Export("playVideo")]
        void PlayVideo();

        /// <summary>
        /// Seek archived broadcast to supplied time (in seconds).
        /// 
        /// - (void) seekTo: (double) time;
        /// </summary>
        /// <param name="time"></param>
        [Export("seekTo:")]
        void SeekTo(double time);
    }

    /// <summary>
    /// The delegate of a BambuserPlayer must adopt the BambuserPlayerDelegate
    /// protocol. Optional methods of the protocol allow the delegate to receive
    /// signals about the state of playback.
    /// </summary>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    public partial interface BambuserPlayerDelegate
    {
        /// <summary>
        /// This method will be called when the BambuserPlayer failed to load or prime after a BambuserPlayer.playVideo: call.
        /// - (void) videoLoadFail;
        /// </summary>
        [Export("videoLoadFail")]
        [Abstract]
        void VideoLoadFail();

        /// <summary>
        /// This method will be called when playback of a broadcast starts.
        /// 
        /// - (void) playbackStarted;
        /// </summary>
        [Export("playbackStarted")]
        [Abstract]
        void PlaybackStarted();

        /// <summary>
        /// This method will be called when playback of a broadcast is paused.
        /// 
        /// - (void) playbackPaused;
        /// </summary>
        [Export("playbackPaused")]
        [Abstract]
        void PlaybackPaused();

        /// <summary>
        /// This method will be called when playback of a broadcast is stopped.
        /// 
        /// - (void) playbackStopped;
        /// </summary>
        [Export("playbackStopped")]
        [Abstract]
        void PlaybackStopped();

        /// <summary>
        /// This method will be called when a broadcast, archived or live, has reached the end.
        /// This method is called regardless of whether the player enters the stopped or paused state at the end.
        /// 
        /// - (void) playbackCompleted;
        /// </summary>
        [Export("playbackCompleted")]
        [Abstract]
        void PlaybackCompleted();

        /// <summary>
        /// This method will be called when the duration of an archived broadcast is known.
        /// 
        /// - (void) durationKnown: (double) duration;
        /// </summary>
        [Export("durationKnown:")]
        [Abstract]
        void DurationKnown(double duration);

        /// <summary>
        /// Called when the number of current viewers is updated.
        /// 
        /// -(void) currentViewerCountUpdated: (int) viewers;
        /// </summary>
        /// <summary>
        /// <param name="viewers">Number of current viewers of the broadcast. This is generally an interesting number during a live broadcast.</param>
        [Export("currentViewerCountUpdated:")]
        [Abstract]
        void CurrentViewerCountUpdated(int viewers);

        /// <summary>
        /// Called when the total number of viewers is updated.
        /// 
        /// -(void) totalViewerCountUpdated: (int) viewers;
        /// </summary>
        /// <summary>
        /// <param name="viewers">Total number of viewers of the broadcast. This accumulates over time and is generally a nice number to show for an old broadcast.</param>
        /// <remarks>The counted viewers are not guaranteed to be unique, but there are measures in place to exclude obvious duplicates, eg. replays from a viewer.</remarks>
        [Export("totalViewerCountUpdated:")]
        [Abstract]
        void TotalViewerCountUpdated(int viewers);
    }
}