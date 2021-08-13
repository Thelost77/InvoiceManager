using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();
        private AddressRepositiory _addressRepositiory = new AddressRepositiory();
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var invoices = _invoiceRepository.GetAllInvoices(userId);


            return View(invoices);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = User.Identity.GetUserId();

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Invoice(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoice = id == 0 ?
                GetNewInvoice(userId) :
                _invoiceRepository.GetInvoice(id, userId);

            var vm = PrepareInvoiceVm(invoice, userId);

            return View(vm);
        }
        public ActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoicePosition = invoicePositionId == 0 ?
                GetNewPosition(invoiceId, invoicePositionId) :
                _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);

            var vm = PrepareInvoicePositionVm(invoicePosition);
            return View(vm);
        }
        public ActionResult Clients()
        {
            var userId = User.Identity.GetUserId();

            var clients = _clientRepository.GetClients(userId);

            foreach (var client in clients)
            {
                client.Address = _addressRepositiory.GetAddress(client.AddressId);
            }

            return View(clients);
        }

        private EditInvoicePositionViewModel PrepareInvoicePositionVm(InvoicePosition invoicePosition)
        {
            return new EditInvoicePositionViewModel
            {
                InvoicePosition = invoicePosition,
                Heading = invoicePosition.Id == 0 ?
                "Dodawanie nowej pozycji" : "Pozycja",
                Products = _productRepository.GetProducts()
            };
        }

        private InvoicePosition GetNewPosition(int invoiceId, int invoicePositionId)
        {
            return new InvoicePosition
            {
                InvoiceId = invoiceId,
                Id = invoicePositionId
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invoice(Invoice invoice)
        {

            var userId = User.Identity.GetUserId();
            invoice.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoiceVm(invoice, userId);
                return View("Invoice", vm);
            }

            if (invoice.Id == 0)
                _invoiceRepository.Add(invoice);
            else
                _invoiceRepository.Update(invoice);



            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoicePosition(InvoicePosition invoicePosition)
        {
            var userId = User.Identity.GetUserId();

            var product = _productRepository.GetProduct(invoicePosition.ProductId);

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoicePositionVm(invoicePosition);
                return View("InvoicePosition", vm);
            }

            invoicePosition.Value = invoicePosition.Quantity * product.Value;

            if (invoicePosition.Id == 0)            
                _invoiceRepository.AddPosition(invoicePosition, userId);            
            else            
                _invoiceRepository.UpdatePosition(invoicePosition, userId);

            _invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);

            return RedirectToAction("Invoice", new { id = invoicePosition.InvoiceId});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.Delete(id, userId);
            }
            catch (Exception e)
            {
                // logowanie do pliku
                return Json(new { Success = true , Message = e.Message});

            }
            return Json(new { Success = true});

        }

        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _clientRepository.Delete(id, userId);
            }
            catch (Exception e)
            {
                // logowanie do pliku
                return Json(new { Success = true, Message = e.Message });

            }
            return Json(new { Success = true });

        }
        [HttpPost]
        public ActionResult DeletePosition(int id, int invoiceId)
        {
            var invoiceValue = 0m;
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.DeletePosition(id, userId);
                invoiceValue = _invoiceRepository.UpdateInvoiceValue(invoiceId, userId);
            }
            catch (Exception e)
            {
                // logowanie do pliku
                return Json(new { Success = true, Message = e.Message });

            }
            return Json(new { Success = true , InvoiceValue = invoiceValue});

        }

        private EditInoviceViewModel PrepareInvoiceVm(Invoice invoice, string userId)
        {
            return new EditInoviceViewModel
            {
                Invoice = invoice,
                Heading = invoice.Id == 0 ? "Dodawanie nowej faktury" : "Faktura",
                Clients = _clientRepository.GetClients(userId),
                MethodOfPayments = _invoiceRepository.GetMethodsOfPayment()
            };
        }

        private Invoice GetNewInvoice(string userId)
        {
            return new Invoice
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentDate = DateTime.Now.AddDays(7)
            };
        }
    }
}