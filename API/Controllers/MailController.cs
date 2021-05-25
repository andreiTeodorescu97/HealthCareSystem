using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Recipes;
using API.Email;
using API.Entities;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MailController : BaseApiController
    {
        private readonly IMailService mailService;
        private readonly DataContext _context;
        public MailController(DataContext context, IMailService mailService)
        {
            _context = context;
            this.mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("readMeds")]
        public async Task<ActionResult> ReadMedicinesAsync()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Files\\nomenclator.xlsx";
            var medicines = new ExcelMapper(filePath).Fetch<MedicineMapperDto>();
            List<MedicineMapperDto> listOfMeds = medicines.ToList();
            List<MedicineMapperDto> listOfMedsToInsert = new List<MedicineMapperDto>();

            //1400

            for (int i = 1; i < 466; i++)
            {
                listOfMedsToInsert.Add(listOfMeds[i*3]);
            }

            var orderedList = listOfMedsToInsert.OrderBy(c => c.Id);

            int index = 1;

            foreach (var item in orderedList)
            {
                var med = new Medicine
                {
                    Id = index,
                    CimCode = item.CodCIM,
                    CommercialName = item.DenumireComerciala,
                    Name = item.DCI,
                    PharmaceuticalForm = item.FormaFarmaceutica,
                    Concentration = item.Concentratie,
                    Producer = item.FirmaProducatoare,
                    TerapeuticalAction = item.ActiuneTerapeutica,
                    Valability = item.Valabilitate
                };

                _context.Medicines.Add(med);
                index++;
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok(orderedList);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}