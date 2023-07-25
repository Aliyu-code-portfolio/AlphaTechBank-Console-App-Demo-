using AlphaTechBank.Models;

namespace AlphaBankTest
{
    [TestClass]
    public class BankFunctionTest
    {
        [TestMethod]
        public void DepositFundsTest()
        {
            AccountService service = new(new List<Account>());
            decimal balance = service.DepositFunds("1234567890");
            Assert.AreEqual(60000, balance);
        }
        [TestMethod]
        public void WithrawFundsTest()
        {
            AccountService service = new(new List<Account>());
            decimal balance = service.WithdrawFunds("1234567890");
            Assert.AreEqual(50000, balance);
        }
        [TestMethod]
        public void TransferFundsTest()
        {
            AccountService service = new(new List<Account>());
            decimal balance = service.TransferFunds("1234567890");
            Assert.AreEqual(46000, balance);
        }
    }
}