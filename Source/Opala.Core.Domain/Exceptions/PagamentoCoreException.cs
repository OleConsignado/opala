using Otc.DomainBase.Exceptions;

namespace Opala.Core.Domain.Exceptions
{
    public class PagamentoCoreException : CoreException<PagamentoCoreError>
    {
        public PagamentoCoreException() : base()
        {

        }

        public PagamentoCoreException(params PagamentoCoreError[] errors)
        {
            AddError(errors);
        }
    }
}
