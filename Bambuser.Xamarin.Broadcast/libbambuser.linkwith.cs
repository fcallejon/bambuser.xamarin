// WARNING: This feature is deprecated. Use the "Native References" folder instead.
// Right-click on the "Native References" folder, select "Add Native Reference",
// and then select the static library or framework that you'd like to bind.
//
// Once you've added your static library or framework, right-click the library or
// framework and select "Properties" to change the LinkWith values.

using ObjCRuntime;

[assembly: LinkWith("libbambuser.a", 
                    SmartLink = true,
                    LinkTarget = LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64,
                    ForceLoad = true,
                    IsCxx = true,
                    LinkerFlags = "-lz -lc++",
                    Frameworks = "AssetsLibrary MobileCoreServices AudioToolbox AVFoundation CoreGraphics CoreLocation CoreMedia CoreVideo Foundation Photos QuartzCore SystemConfiguration UIKit VideoToolbox")]
