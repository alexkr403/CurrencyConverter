using System;
using System.ServiceModel.Configuration;

namespace Common.EndpointBehavior
{
    public class CultureEndpointBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(CultureEndpointBehavior);

        protected override object CreateBehavior()
        {
            return new CultureEndpointBehavior(new CultureMessageInspector());
        }
    }
}
