using System;
//using Demandtec.DealManagement.Contracts.Services;
using IDeals = Demandtec.DealManagement.Services.Client.Reference.IDeals;


namespace Demandtec.DealManagement.Services.Client.Proxy
{
    
     // This new interface is required because we can't use DealsClient directly with Using, 
    // as it does not IDeals does not implement IDisposable.
    public interface IDealsClient : IDeals, IDisposable
    {

    }

}