using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP3_warehouseAPI.Controllers;
using SEP3_warehouseAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace UnitTest_Warehouse
{
    public class WarehouseControllerTest
    {
        private readonly TestFixture<SEP3_warehouseAPI.Data.WarehouseContext> fixture = new TestFixture<SEP3_warehouseAPI.Data.WarehouseContext>(m => new SEP3_warehouseAPI.Data.WarehouseContext(m));

        [Fact]
        public async Task GetAllItemsTest()
        {
            await fixture.RunWithDatabaseAsync(
                arrange: db => new WarehouseController(db),
                act: (db, controller) => controller.ShowAllItems(),
                assert: (result, _, db) =>
                {
                    result.Should().BeOfType<OkObjectResult>();
                    result.As<OkObjectResult>().Value.Should().BeOfType<List<WarehouseItem>>();
                    result.As<OkObjectResult>().Value.As<IList<WarehouseItem>>().Should().HaveCount(db.Stock.Count());
                });
        }

        [Fact]
        public async Task CheckOrderTest()
        {
            //this test is useless at the moment ;)
            Order o = new Order()
            {
                account = "tim",
                orderedItems = null,
                orderId = 8,
                orderInfo = null,
            };

           /*   WarehouseController wc = new WarehouseController();

            //act 
            await fixture.RunWithDatabaseAsync(
                arrange: db => db.,
                act: async (db, count) => new WarehouseController(db).ShowAllItems(),
                assert: (result, count) => Count(result).Equals(count));

            //assert
            result.Should().BeTrue();
                 */
        }
    }
}
