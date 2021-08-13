using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        public ActionResult InvoiceToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            TempData[handle] = GetPdfContent(invoice);

            return Json(new
            {
                FileGuid = handle,
                FileName = $@"Faktura_{invoice.Id}.pdf"
            });
        }

        private byte[] GetPdfContent(Invoice invoice)
        {
            var pdfResult = new ViewAsPdf(@"InvoiceTemplate", invoice)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait
            };

            return pdfResult.BuildFile(ControllerContext);
        }

        public ActionResult DownloadInvoicePdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
                throw new Exception("Błąd przy próbie eksportu faktury do PDF.");

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        public ActionResult PrintPdf(int id)
        {
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            return View("InvoiceTemplate", invoice);
        }
    }


}


