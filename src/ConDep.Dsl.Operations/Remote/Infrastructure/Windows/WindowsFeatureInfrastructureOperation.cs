using System.Collections.Generic;
using ConDep.Dsl.Validation;

namespace ConDep.Dsl.Operations.Infrastructure.Windows
{
    public class WindowsFeatureInfrastructureOperation : RemoteCompositeOperation
    {
        private readonly List<string> _featuresToAdd = new List<string>();
        private readonly List<string> _featuresToRemove = new List<string>();

        public override void Configure(IOfferRemoteComposition server)
        {
            var removeFeatures = _featuresToRemove.Count > 0 ? string.Join(",", _featuresToRemove) : "$null";
            var addFeatures = string.Join(",", _featuresToAdd);
            server.Execute.PowerShell(string.Format("Set-ConDepWindowsFeatures {0} {1}", addFeatures, removeFeatures));
        }

        public override string Name
        {
            get { return "Windows Feature Installer"; }
        }

        public override bool IsValid(Notification notification)
        {
            return true;
        }

        public void AddWindowsFeature(string roleService)
        {
            _featuresToAdd.Add(roleService);            
        }

        public void RemoveWindowsFeature(string roleService)
        {
            _featuresToRemove.Add(roleService);
        }
    }
}