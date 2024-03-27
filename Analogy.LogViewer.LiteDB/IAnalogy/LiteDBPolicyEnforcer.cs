namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class LiteDBPolicyEnforcer : Template.AnalogyPolicyEnforcer
    {
        public override bool DisableUpdates { get; set; }
    }
}