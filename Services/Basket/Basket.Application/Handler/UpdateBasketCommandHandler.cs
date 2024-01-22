using Basket.Application.Command;
using Basket.Application.Mapper;
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
    public class UpdateBasketCommandHandler: IRequestHandler<UpdateBasketCommand, ShoppingCartResponce>
    {
        IBasketRepository basketRepository;
        public UpdateBasketCommandHandler(IBasketRepository _basketRepository)
        {
            basketRepository = _basketRepository;
        }

        public async Task<ShoppingCartResponce> Handle(UpdateBasketCommand updateBasketCommand, CancellationToken cancellationToken)
        {
            var ShopingCartEnt = LazyMapper.MapperLazy.Map<ShoppingCart>(updateBasketCommand);
            var result = await basketRepository.UpdateBasket(ShopingCartEnt);
            var responce =  LazyMapper.MapperLazy.Map<ShoppingCartResponce>(result);
            return responce;
        }

        

    }
}

