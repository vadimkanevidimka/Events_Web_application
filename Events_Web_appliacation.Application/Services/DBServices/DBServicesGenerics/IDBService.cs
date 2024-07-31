using System;
namespace Events_Web_application.Application.Services.DBServices.DBServicesGenerics
{
    public interface IDBService<T>
    {
        public Task<bool> IsRecordExist(T record, CancellationToken cancellationToken);
        public Task<bool> IsRecordValid(T record, CancellationToken cancellationToken);
        public Task<bool> IsRecordDublicate(T record, CancellationToken cancellationToken);
    }
}
