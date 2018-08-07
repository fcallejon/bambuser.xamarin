namespace Bambuser.Xamarin.Broadcast
{
    public static class BambuserViewExtensions
    {
        public static void EnableAudioQualityOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.AudioQualityOption, enabled);
        }

        public static void EnableSaveLocallyOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.SaveLocallyOption, enabled);
        }

        public static void EnableTalkbackOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.TalkbackOption, enabled);
        }

        public static void EnableArchiveOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.ArchiveOption, enabled);
        }

        public static void EnablePositionOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.PositionOption, enabled);
        }

        public static void EnablePrivateModeOption(this BambuserView view, bool enabled = true)
        {
            view.EnableOption(BambuserConstants.PrivateModeOption, enabled);
        }
    }
}
