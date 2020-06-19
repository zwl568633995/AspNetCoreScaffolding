using Kay.Framework.Exceptions.Common;
using System;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Kay.Framework.Exceptions
{
    public class NotFoundException : BaseException
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public Type EntityType { get; set; }

        public object Id { get; set; }

        public NotFoundException(string errorMessage, int errorNumber)
            : base(errorMessage, errorNumber)
        {

        }

        public NotFoundException(Type entityType)
            : this(entityType, null, null)
        {

        }

        public NotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        public NotFoundException(
            Type entityType,
            object id,
            Exception innerException)
            : base($"Entity is not found! entityType: {entityType.FullName}, entity id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        public NotFoundException(string errorMessage)
            : base(errorMessage)
        {

        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
