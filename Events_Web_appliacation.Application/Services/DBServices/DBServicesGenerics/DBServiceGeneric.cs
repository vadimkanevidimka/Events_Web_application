using Events_Web_application.Application.Services.Exceptions;
using Events_Web_application.Infrastructure.DBContext;
using FluentValidation;

namespace Events_Web_application.Application.Services.DBServices.DBServicesGenerics
{
    public class DBServiceGeneric<T> : IDBService<T> where T : class
    {
        protected readonly EWADBContext _context;

        protected IValidator<T> _validator;

        public DBServiceGeneric(EWADBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsRecordExist(T record, CancellationToken cancellationToken)
        {
            try
            {
                if (record != null) throw new ArgumentNullException(nameof(record));
                var objects = await _context.FindAsync(typeof(T), record, cancellationToken);
                return objects == null ? false : true;
            }
            catch (Exception ex) 
            {
                throw new ServiceException(nameof(IsRecordExist), record);
            }
        }

        public async Task<bool> IsRecordValid(T record, CancellationToken cancellationToken)
        {
            try
            {
                if (record == null) throw new ArgumentNullException(nameof(record));
                if (_validator.Validate(record).IsValid) return true;
                else return false;
            }
            catch (Exception ex) 
            {
                throw new ServiceException(nameof(IsRecordValid), _validator.Validate(record).IsValid);
            }
        }

        public async Task<bool> IsRecordDublicate(T record, CancellationToken cancellationToken)
        {
            try
            {
                if (record != null) throw new ArgumentNullException(nameof(record));
                var objects = await _context.FindAsync(typeof(T), record, cancellationToken);
                return objects == null ? false : true;
            }
            catch (Exception ex) 
            {
                throw new ServiceException(nameof(IsRecordDublicate), record);
            }
        }
    }
}