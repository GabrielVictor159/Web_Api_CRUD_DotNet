using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;

namespace gcsb.ecommerce.application.UseCases.Client.GetClients
{
    public class GetClientsRequest
    {
       public Expression<Func<domain.Client.Client, bool>> func {get;private set;}
       public int page {get;private set; }
       public int pageSize {get;private set;}
       public List<GetClientOutput>? clientResult {get;private set;}
       public GetClientsRequest(Expression<Func<domain.Client.Client, bool>> func,
        int page,
        int pageSize)
       {
        this.func = func;
        this.page = page;
        this.pageSize = pageSize;
       }
       public void SetOutput(List<GetClientOutput> clientResult)
        =>this.clientResult = clientResult;
    }
}