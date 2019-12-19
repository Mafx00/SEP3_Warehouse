using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP3_warehouseAPI.Controllers;
using SEP3_warehouseAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest_Warehouse
{
    [TestClass]
    public class WarehouseControllerTest
    {
        private readonly TestFixture<SEP3_warehouseAPI.Data.WarehouseContext> fixture = new TestFixture<SEP3_warehouseAPI.Data.WarehouseContext>(m => new SEP3_warehouseAPI.Data.WarehouseContext(m));

        [Fact]
        public async Task GetAllItemsTest()
        {
           

            await fixture.RunWithDatabaseAsync(
                arrange: db => db.Stock,
                act: async (db, count) => new WarehouseController(db).ShowAllItems(),
                assert: (result, count) => Count(result).Equals(count));
        }

        private int Count(IList<WarehouseItem> l)
        {
            return l.Count;
        }

        [Fact]
        public async Task CheckOrderTest()
        {
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
