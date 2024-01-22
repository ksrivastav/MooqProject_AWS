using Basket.Application.Mapper;
using Basket.Application.Querry;
using Basket.Application.Responce;
using Basket.Core.Domain.Entities;
using Basket.Core.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler
{
    public class GetBasketByUsernameHandler: IRequestHandler<GetBasketByUserNameQuerry, ShoppingCartResponce>
    {
        IBasketRepository basketRepository;
        public GetBasketByUsernameHandler(IBasketRepository _basketRepository)
        {
            basketRepository= _basketRepository;

        }

        public async Task<ShoppingCartResponce>  Handle(GetBasketByUserNameQuerry getBasketByUserNameQuerry, CancellationToken cancellationToken)
        {
            var result = await basketRepository.GetBasket(getBasketByUserNameQuerry.UserName);
            var returnresult = LazyMapper.MapperLazy.Map<ShoppingCart, ShoppingCartResponce>(result);
            return returnresult;
        }



    }
}
