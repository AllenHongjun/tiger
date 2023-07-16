using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Volo.Abp;

namespace Tiger.Infrastructure.Location
{
    public class LocationResolveException : AbpException, IBusinessException
    {
        public LocationResolveException()
        {
        }

        public LocationResolveException(string message)
            : base(message)
        {
        }

        public LocationResolveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LocationResolveException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
