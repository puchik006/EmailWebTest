using EmailWebTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmailWebTest.Services;
using Microsoft.EntityFrameworkCore;

namespace EmailWebTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailsController : Controller
    {
        EmailContext db;
        public MailsController(EmailContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailData>>> Get()
        {
            return await db.EmailTable.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EmailData>>> POST(EmailData email)
        {
            if (email == null)
            {
                return BadRequest();
            }

            EmailSender emailSender = new EmailSender();

            char[] separtors = " ,".ToCharArray();

            string[] recipientsArray = email.Recipients.Split(separtors);

            emailSender.SendMessage(email.Subject, email.Body, recipientsArray);

            //filing data for db
            email.CreateDate = DateTime.Now.ToString();

            email.FailedMessage = emailSender.Failedmessage;

            email.Result = emailSender.IsEmailSent ? "OK" : "Failed";

            //add email to db
            db.EmailTable.Add(email);

            await db.SaveChangesAsync();

            return Ok(email);
        }
    }
}
