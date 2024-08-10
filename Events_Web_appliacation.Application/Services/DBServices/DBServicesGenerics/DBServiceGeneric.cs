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
                if (record == null) throw new ArgumentNullException(nameof(record));
                return _context.Set<T>().Any(c => c.Equals(record));
            }
            catch (Exception ex) 
            {
                throw new ServiceException(nameof(IsRecordExist), record, ex.Message);
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
                throw new ServiceException(nameof(IsRecordValid), _validator.Validate(record).IsValid, ex.Message);
            }
        }

        public async Task<bool> IsRecordDublicate(T record, CancellationToken cancellationToken)
        {
            try
            {
                if (record == null) throw new ArgumentNullException(nameof(record));
                return _context.Set<T>().Any(c =>c.Equals(record));
            }
            catch (Exception ex) 
            {
                return false;
                throw new ServiceException(nameof(IsRecordDublicate), record, ex.Message);
            }
        }
    }
}