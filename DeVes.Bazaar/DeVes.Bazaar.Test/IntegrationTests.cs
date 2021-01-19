using System;
using DeVes.Bazaar.Controllers;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;
using DeVes.Bazaar.Logic;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Test
{
    public class IntegrationTests
    {
        private ISellerRepository _sellerRepository;
        private IArticleRepository _articleRepository;

        private IArticleLogic _articleLogic;
        private ISaleLogic _saleLogic;

        private SaleController _saleController;


        [SetUp]
        public void SetUp()
        {
            _sellerRepository = Substitute.For<ISellerRepository>();
            _articleRepository = Substitute.For<IArticleRepository>();

            _articleLogic = new ArticleLogic(_articleRepository, _sellerRepository);
            _saleLogic = new SaleLogic(_articleLogic);

            _saleController = new SaleController(_saleLogic);
        }


        #region - Sale -

        [Test]
        public void Sale_InsertValidPositions01_ExpectUpdatesInRepo()
        {
            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            _sellerRepository.GetItem(1).Returns(_ => new SellerModel());
            _articleRepository.GetItem(1).Returns(_ => new ArticleModel { Number = 1, Title = "x", Category = "x", Manufacturer = "x", Price = 10, SellerNumber = 1, OnSaleSince = DateTime.Now });

            _saleController.Sale(salePositions);

            _articleRepository.Received(1).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertValidPositions02_ExpectUpdatesInRepo()
        {
            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            _sellerRepository.GetItem(1).Returns(_ => new SellerModel());
            _articleRepository.GetItem(1).Returns(_ => new ArticleModel { Number = 1, Title = "x", Category = "x", Manufacturer = "x", Price = 10, SellerNumber = 1, OnSaleSince = DateTime.Now });
            _articleRepository.GetItem(2).Returns(_ => new ArticleModel { Number = 2, Title = "x", Category = "x", Manufacturer = "x", Price = 10, SellerNumber = 1, OnSaleSince = DateTime.Now });

            _saleController.Sale(salePositions);

            _articleRepository.Received(2).Update(Arg.Any<ArticleModel>());
        }

        #endregion - Sale -
    }
}