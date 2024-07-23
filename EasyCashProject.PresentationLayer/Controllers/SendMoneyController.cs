using EasyCashProject.BusinessLayer.Abstract;
using EasyCashProject.DataAccessLayer.Concrete;
using EasyCashProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyCashProject.PresentationLayer.Controllers
{
    public class SendMoneyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountProcessService _customerAccountProcessService;
        private readonly ICustomerAccountService _customerAccountService;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService, ICustomerAccountService customerAccountService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
            _customerAccountService = customerAccountService;
        }

        [HttpGet]
        public IActionResult Index(string mycurrency)
        {
            ViewBag.currency = mycurrency;
            return View();
        }

        // TL Transferi icin gecerli para gonderme islemleri
        [HttpPost]
        public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDto sendMoneyForCustomerAccountProcessDto)
        {
            // Bu metodda acilan sayfada para transferi yapilacak para turunun ne oldugu kontrol ediliyor
            sendMoneyForCustomerAccountProcessDto.CustomerAccountCurrency = ConvertAccountCurrency(sendMoneyForCustomerAccountProcessDto.CustomerAccountCurrency);

            var context = new Context();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // Gelen alici hesap numarasina gore CustomerAccounts tablosundan o hesaba iliskin CustomerAccountID bilgisini cekecek olan sorgu
            var receiverAccountNumberID = context.CustomerAccounts.Where(x => x.CustomerAccountNumber == sendMoneyForCustomerAccountProcessDto.ReceiverAccountNumber)
                .Select(y => y.CustomerAccountID).FirstOrDefault();

            // Sisteme login olan kullaniciya gore CustomerAccount tablosundan ilgili kullanicidaki tanimli Türk Lirası hesabininin CustomerAccountID bilgisini cekiyoruz.
            var senderAccountNumberID = context.CustomerAccounts.Where(x => x.AppUserID == user.Id)
                .Where(y => y.CustomerAccountCurrency == sendMoneyForCustomerAccountProcessDto.CustomerAccountCurrency)
                .Select(z => z.CustomerAccountID).FirstOrDefault();

            if (senderAccountNumberID != 0 & receiverAccountNumberID != 0)
            {
                var values = new CustomerAccountProcess();
                values.ProcessDate = Convert.ToDateTime(DateTime.Now);
                values.SenderID = senderAccountNumberID;
                values.ProcessType = "Havale";
                values.ReceiverID = receiverAccountNumberID;
                values.Amount = sendMoneyForCustomerAccountProcessDto.Amount;
                values.Description = sendMoneyForCustomerAccountProcessDto.Description;

                // Para transferi sonrasi hesaplardaki paralarda guncelleme islemi
                CustomerAccount receiver = context.CustomerAccounts.Find(receiverAccountNumberID); // Para gonderilecek kisinin ID'si
                receiver.CustomerAccountBalance += values.Amount; // Para gonderilen kisinin hesabini, gonderilen miktar kadar arttirma islemi

                CustomerAccount sender = context.CustomerAccounts.Find(senderAccountNumberID); // Para gonderen kisinin ID'si
                sender.CustomerAccountBalance -= values.Amount; // Para gonderen kisinin hesabindan, gonderilen miktar kadar azaltma islemi

                // Eger para gonderecek kisinin hesap bakiyesi 0 ise ya da para gondermek istedigi miktardan daha az parasi varsa hata donecek
                if (sender.CustomerAccountBalance == 0 || sender.CustomerAccountBalance < values.Amount)
                {
                    ModelState.AddModelError(string.Empty, "Hesabınızda yeteri kadar para bulunmamaktadır!");
                    return View(sendMoneyForCustomerAccountProcessDto);
                }

                // Bakiyesinde yeterli miktarda para varsa bakiye guncelleme ve para transferi islemleri gerceklestiriliyor.
                else
                {
                    _customerAccountService.TUpdate(receiver);
                    _customerAccountService.TUpdate(sender);

                    _customerAccountProcessService.TInsert(values); // Hesap islemleri tablosuna bilgileri kaydetme islemi
                }
            }
            else if (senderAccountNumberID == 0)
            {
                ModelState.AddModelError(string.Empty, sendMoneyForCustomerAccountProcessDto.CustomerAccountCurrency + " hesabınız bulunmamaktadır.");
                return View(sendMoneyForCustomerAccountProcessDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Böyle bir hesap bulunamadı. Lütfen tekrar deneyin.");
                return View(sendMoneyForCustomerAccountProcessDto);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        // Bu metodda acilan sayfada para transferi yapilacak para turunun ne oldugu kontrol ediliyor ve ilgili deger donduruluyor
        private string ConvertAccountCurrency(string accountCurrency)
        {
            switch (accountCurrency)
            {
                case "TL":
                    accountCurrency = "Türk Lirası";
                    break;

                case "USD":
                    accountCurrency = "Dolar";
                    break;

                case "EUR":
                    accountCurrency = "Euro";
                    break;
            }

            return accountCurrency;
        }
    }
}
