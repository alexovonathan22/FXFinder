using FXFinder.API.Controllers;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Models;
using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FXFinder.Test
{
    public class CurrencyTests
    {
        readonly CurrencyController _sut;
        //readonly Cu\ _manager;
        public CurrencyTests()
        {
            //_manager = new ;
        }

        [Fact]
        public async Task Test_To_Change_CurrencyAsync()
        {
            //Arrange
            var model = new CurrencyChangeModel
            {
                Symbol = "EUR",
                UserId = 2
            };

            var cur = new CurrencyChange
            {
                FormerMainCurrencyTitle = "usd",
                NewMainCurrencySymbol = "EUR"

            };
            var currMgr = new Mock<ICurrencyManager>();
            currMgr.Setup(t => t.ChangeMainCurrency(model.Symbol, model.UserId)).ReturnsAsync((cur, "success"));
            var _sut = new CurrencyController(currMgr.Object);
            //Act
            var output = await _sut.ChangeUserCurrency(model);

            Assert.IsType<OkObjectResult>(output);
            Assert.Equal(model.Symbol, cur.NewMainCurrencySymbol);

        }

        [Fact]
        public async Task Test_To_Change_CurrencyAsync_BadResult()
        {
            //Arrange
            var model = new CurrencyChangeModel
            {
                Symbol = "USD",
                UserId = 2
            };

            var cur = new CurrencyChange
            {
                FormerMainCurrencyTitle = "eur",
                NewMainCurrencySymbol = "USD"

            };
            var currMgr = new Mock<ICurrencyManager>();
            currMgr.Setup(t => t.ChangeMainCurrency("kpb", model.UserId)).ReturnsAsync((cur, string.Empty));
            var _sut = new CurrencyController(currMgr.Object);
            //Act
            var output = await _sut.ChangeUserCurrency(model);

            Assert.IsType<BadRequestObjectResult>(output);
            Assert.Equal(model.Symbol, cur.NewMainCurrencySymbol);

        }
       
    }
}
